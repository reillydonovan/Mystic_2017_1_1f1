using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShapeN : MonoBehaviour 
{
    public bool torus = false;
    public bool spiral = false;
    // number of verts along the 'longitude'
    public int phiDivs = 10;
	// number of verts along the 'latitude'
	public int thetaDivs = 10;

	//there are two waves that can be applied to and modify the surface of the spere
	//there is no reason that these have to be the only two waves, but I cannot decide 
	//on a approach to easily make ANY number of them in the editor...

	//set of vars exposed to create waves parallel to the 'latitude'
	// of the sphere
	public int xMod1Period = 4; //how many ups and downs there are
	public float xMod1PhaseOffset = 1.0f; //how far the wave is shifted
	public float xMod1Scale = 2.0f; //how big the wave is
	public float xMod1YOffset = 1.1f; //how big the base of the wave is
	public float xMod1TimeResponse = 1.0f; //the amount the wave moves with time

	//set of vars exposed to create waves parallel to the 'longitude'
	// of the sphere
	public int yMod1Period = 4; //how many ups and downs there are
	public float yMod1PhaseOffset = 1.0f; //how far the wave is shifted
	public float yMod1Scale = 2.0f; //how big the wave is
	public float yMod1YOffset = 1.1f; //how big the base of the wave is
	public float yMod1TimeResponse = 1.0f; //the amount the wave moves with time

    public float m1 = 0.0f;
    public float m1change = 0.0f;
    public float n11 = 0.0f;
    public float n12 = 0.0f;
    public float n13 = 0.0f;
    public float a1 = 1.0f;
    public float b1 = 1.0f;

    public float m2 = 0.0f;
    public float m2change = 0.0f;
    public float n21 = 0.0f;
    public float n22 = 0.0f;
    public float n23 = 0.0f;
    public float a2 = 1.0f;
    public float b2 = 1.0f;

    public float r = 200.0f;
    float x = 0.0f;
    float y = 0.0f;
    float z = 0.0f;
    public float a = 0.0f;
    public float b = 0.0f;


    // Use this for initialization
    void Start () 
	{
		//we need a mesh filter
		GetComponent<MeshFilter>().mesh = new Mesh();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.UpdateMesh( GetComponent<MeshFilter>().mesh );
	}

	// create or update a mesh object to have a sphere with hamonic waves all over
	// it
	Mesh UpdateMesh(Mesh m)
	{
		if(m == null)
		{
			m = new Mesh();
		}

		//clear out the old mesh
		m.Clear();

		Vector3[] vectors = new Vector3[phiDivs * thetaDivs];
		Vector2[] uvs = new Vector2[phiDivs * thetaDivs];
		float radsPerPhiDiv = Mathf.PI/(phiDivs-1);
		float radsPerThetaDiv = 2 * Mathf.PI/thetaDivs;

		float seconds = Time.timeSinceLevelLoad;

		// build an array of vectors holding the vertex data
		int vIndex = 0;
		for(int i=0; i < phiDivs; i++)
		{
			float phi = radsPerPhiDiv*i;
           // float r1 = Shape(phi, m1, n11, n12, n13, a1, b1);

         //  float phi = Remap(i, 0, phiDivs - 1, -1 * Mathf.PI, Mathf.PI);
            float r2 = Shape(phi, m2, n21, n22, n23, a2, b2);
            for (int j=0; j < thetaDivs; j++)
			{
                float theta = radsPerThetaDiv*j;
              //  float theta = Remap(j, 0, thetaDivs, -1 * Mathf.PI / 2, Mathf.PI / 2);
                //  float r2 = Shape(theta, m2, n21, n22, n23, a2, b2);
                float r1 = Shape(theta, m1, n11, n12, n13, a1, b1);
                //the get radius function is where 'hamonics' are added
                float r = GetRadius(phi,theta,seconds);
				//add uvs so that we can texture the mesh if we want
				uvs[vIndex] = new Vector2(j*1.0f/thetaDivs,i*1.0f/phiDivs);
                //create a vertex 
                //
                //optimization alert: since the only thing that changes here is the radius
                //(when the number of divisions stays the same) we could cache these numbers
                // and use a shader to create and apply the variations in radius and compute 
                // the normals.
                /*
                 vectors[vIndex++] = new Vector3(r * Mathf.Sin(phi) * Mathf.Cos(theta),
                                                 r * Mathf.Sin(phi) * Mathf.Sin(theta),
                                                 r * Mathf.Cos(phi));	
                                              */
                if (torus)
                {
                    //   x = Mathf.Cos(lon) * (r1 + r2 * Mathf.Cos(lat));
                    //   y = Mathf.Sin(lon) * (r1 + r2 * Mathf.Cos(lat));
                    //   z = r * r2 * Mathf.Sin(lat);
                    //  vectors[vIndex++] = new Vector3(x, y, z);

                    //getting closer
                    /*
                    x = Mathf.Cos(theta) * (r1 + r2 * Mathf.Cos(phi));
                    y = Mathf.Sin(theta) * (r1 + r2 * Mathf.Cos(phi));
                    z = r * r2 * Mathf.Sin(phi);
                    */
                    // x = (R + r * cos(theta)) * cos phi
                    // y = (R + r * Cos(theta)) * sin(phi)
                    // z = r * sin(theta)
                    x = Mathf.Cos(phi) * (r1 + r2 * Mathf.Cos(theta));
                    y = Mathf.Sin(phi) * ( r1 + r2 * Mathf.Cos(theta));
                    z = r * r2 * Mathf.Sin(theta);
                }
                else if (spiral)
                    {
                        x = r * (r1 * a * theta * Mathf.Cos(theta)) * r2 * Mathf.Cos(phi) * Mathf.Cos(theta);
                        y = r * (r1 * a * theta * Mathf.Sin(theta)) * r2 * Mathf.Sin(phi) * Mathf.Cos(theta);
                        z = r * r2 * Mathf.Sin(theta);
                    }
                else
                {
                    x = r * r1 * r2 * Mathf.Sin(phi) * Mathf.Cos(theta);
                    y = r * r1 * r2 * Mathf.Sin(phi) * Mathf.Sin(theta);
                    z = r * r2 * Mathf.Cos(phi);
                }
                /*
                                x = r * r1 * r2 * Mathf.Cos(theta) * Mathf.Cos(phi);
                                y = r * r1 * r2 * Mathf.Sin(theta) * Mathf.Cos(phi);
                                z = r * r2 * Mathf.Sin(phi);
                                */
                vectors[vIndex++] = new Vector3(x, y, z);
            }
		}
		m.vertices = vectors;
		m.uv = uvs;

		//assign triangles - these take the form of 'triangle strips' wrapping the
		// circumference of the sphere
		//there is room to optimise this by not recalculating/reassigning if the 
		//count of the vertecies hasn't changed because the topology will still
		// be the same.
		int triCount = 2 * (phiDivs-1) * (thetaDivs);
		int[] triIndecies = new int[triCount*3];
		int curTriIndex = 0;
		for(int i=0; i < phiDivs-1; i++)
		{
			for(int j=0; j < thetaDivs; j++)
			{
				int ul = i*thetaDivs+j;//"upper left" vert
				int ur = i*thetaDivs+ ((j+1) % thetaDivs);//"upper right" vert
				int ll = (i+1)*thetaDivs+j;//"lower left" vert
				int lr = (i+1)*thetaDivs+ ((j+1) % thetaDivs); //"lower right" vert
				//triangle one
				triIndecies[curTriIndex++] = ul;
				triIndecies[curTriIndex++] = ll;
				triIndecies[curTriIndex++] = ur;

				//triangle two
				triIndecies[curTriIndex++] = ll;
				triIndecies[curTriIndex++] = lr;
				triIndecies[curTriIndex++] = ur;
			}
		}
		m.triangles = triIndecies;
		//use the triangle info to calculate vertex normals so we dont have to B)
		m.RecalculateNormals();
		return m;
	}

	//get radius applies waves along phi and theta based on the public variables
	//optimization note:
	// this would not be impossible to code as a shader... however, getting multiple 
	// waves affecting the surface at once might take some careful thinking...
	float GetRadius(float phi, float theta, float time = 0)
	{  
		return xMod1YOffset + xMod1Scale*Mathf.Sin(xMod1TimeResponse*time + theta*xMod1Period*time*.1f + xMod1PhaseOffset*time) +
			yMod1YOffset + yMod1Scale*Mathf.Sin(yMod1TimeResponse*time + phi*yMod1Period*time*.1f + yMod1PhaseOffset*time);
	}

    //SuperShape Formula
    float Shape(float _theta, float _m, float _n1, float _n2, float _n3, float _a, float _b)
    {
        float t1 = Mathf.Abs((1 / _a) * Mathf.Cos(_m * _theta / 4));
        t1 = Mathf.Pow(t1, _n2);

        float t2 = Mathf.Abs((1 / _b) * Mathf.Sin(_m * _theta / 4));
        t2 = Mathf.Pow(t2, _n3);

        float t3 = t1 + t2;

        float r = Mathf.Pow(t3, -1 / _n1);
        return r;

    }

    //show a representation in the editor window
    private void OnDrawGizmos () {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireMesh(UpdateMesh(null),transform.position,transform.rotation,transform.localScale);

	}

    float Remap(float val, float srcMin, float srcMax, float dstMin, float dstMax)
    {
        if (val >= srcMax) return dstMax;
        if (val <= srcMin) return dstMin;
        return dstMin + (val - srcMin) / (srcMax - srcMin) * (dstMax - dstMin);
    }
}
