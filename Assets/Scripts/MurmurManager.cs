using UnityEngine;
//using HoloToolkit.Unity.SpatialMapping;

public class MurmurManager : MonoBehaviour
{
 //   public bool canvasToggle;
   // public bool tapToggle;
//    public GameObject canvasTarget;

//    public GameObject sharingPrefab;
//    public GameObject spatialPrefab;

    //Create an array to hold any number of objects you want
    public GameObject[] objectArray = new GameObject[10]; //initialize the array for e.g. 10 cubes

    //Holds the index of the objectArray, which corresponds to the next cube to be activated
    private int activateNext = 0;



    // Use this for initialization
    void Start()
    {

        for (int i = 1; i < objectArray.Length; i++)
        {
            objectArray[i].SetActive(false);
        }
        objectArray[0].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        //whenever a click occurs, and as long as activateNext is less than the length of the array...
        /*
        if (Input.GetKeyDown(KeyCode.B)) // && activateNext < objectArray.Length)
        {
            backObject();
        }
        if (Input.GetKeyDown(KeyCode.F)) // && activateNext < objectArray.Length)
        {
            backObject();
        }
        */
    }

  public void nextObject()
    {
        //... activate next cube
        activateNext++;
        if (activateNext > objectArray.Length - 1) activateNext = 0;
        objectArray[activateNext].SetActive(true);
        if (activateNext > 0) objectArray[activateNext - 1].SetActive(false);
        if (activateNext == 0) objectArray[objectArray.Length - 1].SetActive(false);


        
    }

    public void backObject()
    {
        //... activate next cube
        activateNext--;
        if (activateNext < 0) activateNext = objectArray.Length - 1;
         objectArray[activateNext].SetActive(true);
        if (activateNext < objectArray.Length -1) objectArray[activateNext + 1].SetActive(false);
        if (activateNext == objectArray.Length -1) objectArray[0].SetActive(false);

        /*
        foreach (Transform child in objectArray[activateNext].transform)
        {
            child.gameObject.SetActive(true);
        }
        */

    }
    /*
    public void ToggleTapToPlace ()
    {
        tapToggle = !tapToggle;

        for(int i = 0; i < objectArray.Length -1; i++)
        {
         //   objectArray[i].GetComponent<TapToPlace>().enabled = tapToggle;
            objectArray[i].SetActive(tapToggle);
        }
    

    }*/

    public void ToggleCanvas()
    {
   //     canvasToggle = !canvasToggle;

   //     canvasTarget.gameObject.SetActive(canvasToggle);
   //     Debug.Log("Canvas Toggle is " + canvasToggle);

    }



}