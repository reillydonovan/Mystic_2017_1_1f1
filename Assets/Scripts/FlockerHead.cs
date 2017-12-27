using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FlockerHead : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    Vector3 averageHeading;
    Vector3 averagePosition;
    public float neighborDistance;
    bool turning = false;

    public enum FlockType {BIRD, INSECT, TADPOLE };
    public enum FlockState { PATROL, PURSUE, ATTACK};
    public FlockType activeFlockType = FlockType.BIRD;
    public FlockState activeFlockState = FlockState.PATROL;

    public float minSpeedRange;
    public float maxSpeedRange;
    string manager;
    GameObject flockManager;
    // Use this for initialization

    void Start()
    {

        speed = Random.Range(0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeFlockType();
        ChangeDirectionManager();
        ChangeFlockProperties();
        if (turning)
        {
            ChangeDirection();
        }
        else
        {
            ChangeFlockState();
        }
        MoveAgent();        
    }



    public void changeMaterial()
    {
        gameObject.GetComponent<Renderer>().sharedMaterial.color = Color.red;
    //    Debug.Log("Change Material has been triggered to Red");

    }

    void ChangeFlockType()
    {
        switch (activeFlockType)
        {
            case FlockType.BIRD:
                {
                    manager = "BirdManager";
                    transform.parent = GameObject.Find(manager).transform;
                }
                break;

            case FlockType.INSECT:
                {
                    manager = "InsectManager";
                    transform.parent = GameObject.Find(manager).transform;
                }
                break;

            case FlockType.TADPOLE:
                {
                    manager = "TadpoleManager";
                    transform.parent = GameObject.Find(manager).transform;
                }
                break;
        }

        flockManager = GameObject.Find(manager);

    }

    void ChangeDirectionManager()
    {
        if (Vector3.Distance(transform.position, transform.parent.position) >= flockManager.GetComponent<FollowFlock>().tankSize)
       // || transform.position.y <= -1.5f)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }
    }

    void ChangeFlockProperties()
    {
        speed = flockManager.GetComponent<FollowFlock>().agentSpeed;
        rotationSpeed = flockManager.GetComponent<FollowFlock>().agentRotation;
        neighborDistance = flockManager.GetComponent<FollowFlock>().agentNeighborDistance;
        minSpeedRange = flockManager.GetComponent<FollowFlock>().minSpeed;
        maxSpeedRange = flockManager.GetComponent<FollowFlock>().maxSpeed;
    }

    void ChangeDirection()
    {
        Vector3 direction = transform.parent.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction),
            rotationSpeed * Time.deltaTime);
        speed = Random.Range(minSpeedRange, maxSpeedRange);
    }

    void ChangeFlockState()
    {
        switch (activeFlockState)
        {
            case FlockState.PATROL:
                {
                    
                    if(manager == "BirdManager")
                    {
                        if (Random.Range(0, 7) < 1) ApplyRules();
                    }
                    else
                    {
                        if (Random.Range(0, 7) < 1) ApplyRules();
                    }
                 //   Debug.Log("state is patrol");
                }
                break;

            case FlockState.PURSUE:
                {
                    if (manager == "BirdManager") {
                        if (Random.Range(0, 7) < 1) ApplyRules();
                    }
                    else {
                      if (Random.Range(0, 4) < 1) ApplyRules();
                    }
                //    Debug.Log("state is pursue");
                }
                break;

            case FlockState.ATTACK:
                {
                    
                    if (manager == "BirdManager")
                    {
                        if (Random.Range(0, 10) < 1) ApplyRules();
                    }
                    else
                    {
                        ApplyRules();
                    }
                    //  Debug.Log("state is attack");
                }
                break;
        }
    }

    void MoveAgent()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    public void ApplyRules()
    {
        GameObject[] gos;
        gos = flockManager.GetComponent<FollowFlock>().allFlock; 

        Vector3 vCenter = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float gSpeed = 0.1f;

        Vector3 goalPos = FollowFlock.goalPos;


        float dist;

        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);

                if (dist <= neighborDistance)
                {
                    vCenter += go.transform.position;
                    groupSize++;

                    if (dist < 1.0f)
                    {
                        vAvoid = vAvoid + (this.transform.position - go.transform.position);
                    }

                    FlockerHead anotherFlock = go.GetComponent<FlockerHead>();
                    gSpeed = gSpeed + anotherFlock.speed; // - .05f;
                }
            }

        }

        if (groupSize > 0)
        {
            vCenter = vCenter / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vCenter + vAvoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }
    }
}
