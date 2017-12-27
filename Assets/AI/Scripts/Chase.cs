using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {

    public Transform player;
    public Transform head;
    public GameObject flockPrefab;
    public GameObject flockManager;
    Animator anim;

    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;
    public float rotSpeed = 0.2f;
    public float speed = 1.5f;
    public float npcViewRange;
    public float npcViewAngle;
    public float npcEngageMagnitude;

    public float accuracyWP = 5.0f;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = player.position - this.transform.position;
       // direction.y = 0;
        float angle = Vector3.Angle(direction, head.up);

        if( state == "patrol" && waypoints.Length > 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            flockPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.green;

            /* Waypoint Manager
              if(Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
               {

            currentWP++;
        if(currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }

                currentWP = Random.Range(0, waypoints.Length);
               }
            
            direction = waypoints[currentWP].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
            */
        }     

        if (Vector3.Distance(transform.position, flockManager.transform.position) >= flockManager.GetComponent<FollowFlock>().tankSize + 1)
            transform.position = flockManager.transform.position;

        if (Vector3.Distance(player.position, this.transform.position) < npcViewRange && (angle < npcViewAngle || state == "pursuing")) //state == "pursuing")

        {
             state = "pursuing";
            flockPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.yellow;
          

            this.transform.rotation 
                = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

            anim.SetBool("isIdle", false);
            if(direction.magnitude > npcEngageMagnitude)
            {
                this.transform.Translate(0, 0, Time.deltaTime * speed);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
                flockPrefab.GetComponent<FlockerHead>().changeMaterial();
       

            }
        }
        else
        {
         //   anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
             state = "patrol";
        }
	}
}
