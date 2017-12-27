using UnityEngine;

public class ActivateCubes : MonoBehaviour
{

    //Create an array to hold any number of cubes you want
    public GameObject[] cubeArray = new GameObject[10]; //initialize the array for e.g. 10 cubes

    //Holds the index of the cubeArray, which corresponds to the next cube to be activated
    private int activateNext = 0;


    // Use this for initialization
    void Start()
    {

        for (int i = 1; i < cubeArray.Length; i++)
        {
            cubeArray[i].SetActive(false);
        }
        cubeArray[0].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        //whenever a click occurs, and as long as activateNext is less than the length of the array...
        /*
        if (Input.GetKeyDown(KeyCode.B)) // && activateNext < cubeArray.Length)
        {
            backObject();
        }
        if (Input.GetKeyDown(KeyCode.F)) // && activateNext < cubeArray.Length)
        {
            backObject();
        }
        */
    }

  public void nextObject()
    {
        //... activate next cube
        activateNext++;
        if (activateNext > cubeArray.Length - 1) activateNext = 0;
        cubeArray[activateNext].SetActive(true);
        if (activateNext > 0) cubeArray[activateNext - 1].SetActive(false);
        if (activateNext == 0) cubeArray[cubeArray.Length - 1].SetActive(false);


        
    }

    public void backObject()
    {
        //... activate next cube
        activateNext--;
        if (activateNext < 0) activateNext = cubeArray.Length - 1;
        cubeArray[activateNext].SetActive(true);
        if (activateNext < cubeArray.Length -1) cubeArray[activateNext + 1].SetActive(false);
        if (activateNext == cubeArray.Length -1) cubeArray[0].SetActive(false);
        
    }
}