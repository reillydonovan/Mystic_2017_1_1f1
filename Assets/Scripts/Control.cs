using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Control : MonoBehaviour {

    public static Vector3 centreOfMass = Vector3.zero;
    public static Vector3 globalVelocity = Vector3.zero;
    public GameObject COMSphere;
    
    public GameObject controlBall;

    public bool use2d = false;

    public static int numberOfBoids = 300;
    public Text boidCountReporter;
    
    public Slider centreOfMassController;
    public Slider distanceController;
    public Slider velocityController;
    public Slider controLBallController;

    public static GameObject physBoid;
    public GameObject boidBody;

    public Boid[] boids;

    Vector3 tempVector;

    float maxProxyValue = 1;

    Vector3 c1;
    Vector3 c2;
    Vector3 c3;
    Vector3 ballpos;

    public static float maxDistanceFromOrigin = 50f;

    public float sensitivity = 10;
    public float heightRot = 0;
    public float yRot = 0;
    float rotIntmd;
    float camDistance = 30;
    Vector3 centreOfCam = Vector3.zero;
    public Toggle centreOnSwarm;

	void Start () {
        physBoid = boidBody;

        //Create the boid array
        boids = new Boid[numberOfBoids];
        for (int b = 0; b < numberOfBoids; b++)
        {
            boids[b] = new Boid(b);
            //print("Created a boid.");
        }
        boidCountReporter.text = "Boids: " + numberOfBoids;
	}
	
	// Update is called once per frame
	void Update () {
        //Update control ball
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = camDistance;
            Ray r = Camera.main.ScreenPointToRay(mousePos);
            //ballpos += Camera.main.transform.forward * camDistance;
            
            controlBall.transform.position = r.origin + r.direction * camDistance;
        }

        //Get camera position
        if (Input.GetMouseButton(1))
        {
            yRot += Input.GetAxis("Mouse X") * sensitivity / 100;
            heightRot -= Input.GetAxis("Mouse Y") * sensitivity / 100;

            if (yRot > Mathf.PI * 2)
            {
                yRot = 0;
            }
            else
            {
                if (yRot < Mathf.PI * -2)
                {
                    yRot = Mathf.PI * -2;
                }
            }

            if (heightRot > Mathf.PI / 2)
            {
                heightRot = Mathf.PI / 2;
            }
            else
            {
                if (heightRot < -Mathf.PI / 2)
                {
                    heightRot = -Mathf.PI / 2;
                }
            }
        }
        //Change distance
        camDistance += Input.GetAxis("Mouse ScrollWheel") * 10;

        float y = camDistance * Mathf.Sin(heightRot);
        rotIntmd = camDistance * Mathf.Cos(heightRot);
        float z = rotIntmd * Mathf.Cos(yRot);
        float x = rotIntmd * Mathf.Sin(yRot);
        Camera.main.transform.position = new Vector3(x, y, z);
        Camera.main.transform.LookAt(Vector3.zero, Vector3.up);


        UpdateConstants();
        COMSphere.transform.position = centreOfMass;

        //Update all boids
        for (int b = 0; b < numberOfBoids; b++)
        {
            c1 = PercievedCentreOfMass(b);
            c2 = PercievedVelocity(b);
            c3 = BoidRepulsion(b);
            float intensity = c3.magnitude;
            if (intensity > maxProxyValue) maxProxyValue = intensity;

            
            boids[b].SetColorIntensity(1- (intensity / maxProxyValue));

            boids[b].velocity = boids[b].velocity + c1 + c2 + c3 + distanceController.value * FollowBall(b);
            boids[b].UpdateBoid(use2d);
        }
    }


    void UpdateConstants()
    {
        //Calculate global centre of mass
        tempVector = Vector3.zero;
        for (int j = 0; j < numberOfBoids; j++)
        {
            tempVector += boids[j].position;
        }
        centreOfMass = tempVector / numberOfBoids;
        tempVector = Vector3.zero;

        //Calculate global velocity
        for (int b = 0; b < numberOfBoids; b++)
        {
            tempVector += boids[b].velocity;
        }
        tempVector = tempVector / numberOfBoids;
        globalVelocity = tempVector;
    }


    
    public Vector3 FollowBall(int boidID)
    {
        //Calculate vector from boid to control ball
        return (controlBall.transform.position - boids[boidID].position) * controLBallController.value * reverseSigmoid(Vector3.Distance(boids[boidID].position, controlBall.transform.position));
    }

    public Vector3 BoidRepulsion(int boidID)
    {
        Vector3 returnVect = Vector3.zero;

        for (int b = 0; b < numberOfBoids; b++)
        {
            if ((b != boidID)&&((boids[boidID].position - boids[b].position).magnitude < distanceController.value))
            {
                returnVect = returnVect - (boids[b].position - boids[boidID].position);
            }
        }
        return returnVect;
    }

    public Vector3 PercievedCentreOfMass(int boidID)
    {
        Vector3 COMP = (centreOfMass - (boids[boidID].position / numberOfBoids));
        return (COMP - boids[boidID].position) * centreOfMassController.value ;
    }

    public Vector3 PercievedVelocity(int boidID)
    {
        return velocityController.value * (globalVelocity - (boids[boidID].velocity / numberOfBoids));
    }


    float reverseSigmoid(float x)
    {
        return 1 / Mathf.Exp(x / Mathf.Abs(distanceController.value));
    }
}
