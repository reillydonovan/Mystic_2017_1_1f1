using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleFlock : MonoBehaviour
{
    public enum FlockType { BIRD, INSECT, TADPOLE };
    public enum NpcState { PATROL, PURSUE, ATTACK };
    public enum FlockState { PATROL, PURSUE, ATTACK };
    [Header("Flock Manager")]
    public GameObject flockPrefab;
    public float tankSize = 5;
    public float tankScale = 1;
    public int numFlock = 20;
    public GameObject[] allFlock;
    public static Vector3 goalPos = Vector3.zero;
    public float minSpeed;
    public float maxSpeed;
    public float gSpeedChange;
    public float minScale;
    public float maxScale;
    public float agentSpeed;
    public float agentRotation;
    public float agentNeighborDistance;
    public FlockType activeFlockType = FlockType.BIRD;
    public FlockState activeFlockState = FlockState.PATROL;

    [Header("NPC Target")]
    public GameObject goalPrefab;
    public Transform player;
    public Transform head;
    public NpcState activeNpcState = NpcState.PATROL;
    Animator anim;
    public float rotSpeed = 0.2f;
    public float npcSpeed = 1.5f;
    public float npcViewRange;
    public float npcViewAngle;
    public float npcEngageMagnitude;

    Vector3 npcDirection;
    float angle;
    // public Material material;
     public float vertexAnimOffset;


    // Use this for initialization
    void Start()
    {
        tankSize = tankSize * tankScale;
        anim = goalPrefab.GetComponent<Animator>();
        allFlock = new GameObject[numFlock];
        for (int i = 0; i < numFlock; i++)
        {
            Vector3 pos = new Vector3(Random.Range(transform.position.x - tankSize, transform.position.x + tankSize), 
                Random.Range(transform.position.y - tankSize, transform.position.y + tankSize), 
                Random.Range(transform.position.z - tankSize, transform.position.z + tankSize));
            allFlock[i] = (GameObject)Instantiate(flockPrefab, pos, Quaternion.identity);

          vertexAnimOffset = Random.Range(0, 10);
          allFlock[i].GetComponent<Renderer>().material.SetFloat("_VertexAnimOffset", vertexAnimOffset);
            allFlock[i].transform.localScale *= Random.Range(minScale, maxScale);

        }
    }

    // Update is called once per frame
    void Update()
    {
        NpcStateManager();
        SwitchNpcState();
        SwitchFlockType();
        SwitchFlockState();
    }

    void NpcStateManager()
    {
        npcDirection = player.position - goalPrefab.transform.position;
        angle = Vector3.Angle(npcDirection, head.up);

        // reset NPC if too far outside of tank
        if (Vector3.Distance(transform.position, goalPrefab.transform.position) >= tankSize + 2)
            goalPrefab.transform.position = transform.position;

        if (Vector3.Distance(player.position, goalPrefab.transform.position) < npcViewRange
            && (angle < npcViewAngle || activeNpcState == NpcState.PURSUE))

        {
            if (npcDirection.magnitude > npcEngageMagnitude) activeNpcState = NpcState.PURSUE;
            else activeNpcState = NpcState.ATTACK;
        }
        else
        {
            activeNpcState = NpcState.PATROL;
        }
    }

    void SwitchNpcState()
    {
        switch (activeNpcState)
        {
            case NpcState.PATROL:
                {
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isAttacking", false);
                    if (Random.Range(0, 10000) < 50) RandomGoal();

                    // flockPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.green;
                    /*
                    for (int i = 0; i < numFlock; i++)
                    {
                        allFlock[i].GetComponent<Renderer>().material.color = Color.green;
                    }
                    */
                    activeFlockState = FlockState.PATROL;
                }
                break;

            case NpcState.PURSUE:
                {
                    // flockPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.yellow;
                    /*
                    for (int i = 0; i < numFlock; i++)
                    {
                        allFlock[i].GetComponent<Renderer>().material.color = Color.yellow;
                    }
                    */
                    goalPrefab.transform.rotation
                        = Quaternion.Slerp(goalPrefab.transform.rotation,
                        Quaternion.LookRotation(npcDirection), rotSpeed * Time.deltaTime);

                    goalPrefab.transform.Translate(0, 0, Time.deltaTime * npcSpeed);
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isAttacking", false);
                   // RandomGoal();
                    activeFlockState = FlockState.PURSUE;
                }
                break;

            case NpcState.ATTACK:
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isWalking", false);
                    //   flockPrefab.GetComponent<FlockerHead>().changeMaterial();
                    /*
                    for (int i = 0; i < numFlock; i++)
                    {
                        allFlock[i].GetComponent<Renderer>().material.color = Color.red;
                    }
                    */
                    activeFlockState = FlockState.ATTACK;
                }
                break;
        }
    }

    void SwitchFlockType()
    {
        //different flock types from the same flock prefab script

        switch (activeFlockType)
        {
            case FlockType.BIRD:
                {
                    for (int i = 0; i < numFlock; i++)
                    {
                        allFlock[i].GetComponent<TadpoleHead>().activeFlockType = TadpoleHead.FlockType.BIRD;
                    }
                }
                break;

            case FlockType.INSECT:
                {
                    for (int i = 0; i < numFlock; i++)
                    {
                        allFlock[i].GetComponent<TadpoleHead>().activeFlockType = TadpoleHead.FlockType.INSECT;
                    }
                }
                break;

            case FlockType.TADPOLE:
                {
                    for (int i = 0; i < numFlock; i++)
                    {
                        allFlock[i].GetComponent<TadpoleHead>().activeFlockType = TadpoleHead.FlockType.TADPOLE;
                    }
                }

                break;
        }
    }

    void SwitchFlockState()
    {







        // different global behaviors for flock prefab, perhaps integrate custom features with respect to flock types

        switch (activeFlockState)
        {
            case FlockState.PATROL:
                {

                    for (int i = 0; i < numFlock; i++)
                    {
                        allFlock[i].GetComponent<TadpoleHead>().activeFlockState = TadpoleHead.FlockState.PATROL;
                    }
                  //  if (Random.Range(0, 10000) < 50) RandomGoal();
                }
                break;

            case FlockState.PURSUE:
                {
                    goalPos = goalPrefab.transform.position;

                    for (int i = 0; i < numFlock; i++)
                    {
                        allFlock[i].GetComponent<TadpoleHead>().activeFlockState = TadpoleHead.FlockState.PURSUE;
                    }
                }
                break;

            case FlockState.ATTACK:
                {
                    goalPos = goalPrefab.transform.position;
                    for (int i = 0; i < numFlock; i++)
                    {
                        allFlock[i].GetComponent<TadpoleHead>().activeFlockState = TadpoleHead.FlockState.ATTACK;
                    }

                }
                break;
        }
    }




    void RandomGoal()
    {
      goalPos = new Vector3
            (Random.Range(transform.position.x - tankSize, transform.position.x + tankSize),
            Random.Range(-1.5f, transform.position.y + tankSize),
            Random.Range(transform.position.z - tankSize, transform.position.z + tankSize));
        goalPrefab.transform.position = goalPos;
        Debug.Log("random goal");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + tankSize / 2 - .75f, transform.position.z),
            new Vector3(tankSize * 2, tankSize + 1.5f, tankSize * 2));
    }
}
