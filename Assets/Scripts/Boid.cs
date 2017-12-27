using UnityEngine;

using System.Collections;

public class Boid {

    Vector3 currentVelocity;

    GameObject body;
    public Vector3 velocity = Vector3.zero;
    public int ID;
    Color originalColor;
    Renderer r;

    public Boid(int BoidID)
    {
        body = (GameObject)Object.Instantiate(Control.physBoid, RandomVector(30), Quaternion.identity);
        
        r = body.GetComponent<Renderer>();
        originalColor = r.material.color;
        ID = BoidID;
        body.name = "Boid " + ID;
        velocity = RandomVector(2f);
        MonoBehaviour.print("Boid " + ID + " created.");
    }

	// Use this for initialization
	void Start () {
        
	}

    public void SetColorIntensity(float amount)
    {
        float intensity = amount;
        r.material.color = new Color(originalColor.r * intensity, originalColor.g * intensity, originalColor.b * intensity, 1);
    }
	
	// Update is called once per frame
	public void UpdateBoid (bool Use2D) {
        //Add the velocity * time elapsed to the position
        currentVelocity = Vector3.Lerp(currentVelocity, velocity, Time.deltaTime * 4);
        body.transform.position += currentVelocity * Time.deltaTime;



        if (velocity.magnitude > 2)
        {
            velocity = velocity.normalized * 2;
        }

        if (position.magnitude > Control.maxDistanceFromOrigin) body.transform.position = -body.transform.position.normalized * Control.maxDistanceFromOrigin;

        Debug.DrawLine(body.transform.position, body.transform.position + velocity, Color.red);

        if (Use2D)
        {
            Vector3 pos = body.transform.position;
            pos.y = 0;
            body.transform.position = pos;
        }
    }



    public Vector3 position
    {
        get
        {
            return body.transform.position;
        }
    }
    

    Vector3 RandomVector(float minMax)
    {
        return new Vector3(Random.Range(-minMax, minMax), Random.Range(-minMax, minMax), Random.Range(-minMax, minMax));
    }
}
