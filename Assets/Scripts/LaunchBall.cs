using UnityEngine;
using System.Collections;

public class LaunchBall : MonoBehaviour {


    public Vector3 launchBallHome = new Vector3(0.0f, 0.0f, 4.0f);
    public float lbSpeed;
    public bool checkLBToggle = true;
    private Color lbColor;    
    Renderer rend;
    bool lbJump;
    
 

    // Use this for initialization
    void Start()
    {
        gameObject.transform.position = launchBallHome;
        lbSpeed = 20f;        
        lbColor = new Color(0.1f, 0.2f, 0.5f);
        rend = gameObject.GetComponent<Renderer>();
        lbJump = true;
        rend.material.color = lbColor;

    }

	// Update is called once per frame
	void Update () {

       
	}

    //Class Methods
   public void TeleportBall()
    {
       
            transform.position = new Vector3(transform.position.x, -1, transform.position.z);
        

    }

    public void ChangeColor()
    {
        float redColor = 0.1f;

        lbColor.r = redColor;
        lbColor.g = Random.Range(0, 1f);
        lbColor.b = Random.Range(0, 1f);
        rend.material.color = lbColor;
    }
    public void LaunchBallJump()
    {
        transform.Translate(Vector3.up * lbSpeed * Time.deltaTime);        
    }
    void CheckLB()
    {
        int i = Random.Range(0, 1000);
        if(i < 997)
        {
            lbJump = true;

        }
        else
        {
            
            lbJump = false;
            transform.position = launchBallHome;
        }

    }


}
