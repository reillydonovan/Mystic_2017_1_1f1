using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeapons : MonoBehaviour {
    Camera playerCamera;
    Ray rayFromPlayer;
    RaycastHit hit;
    public int rayLength = 100;
    public GameObject impactSparks;
    public int gunAmmo = 3;

    private const int WEAPON_GUN = 0;
    private const int WEAPON_AUTO_GUN = 1;
    private const int WEAPON_GRENADE = 2;

    private int activeWeapon = WEAPON_GUN;
    private float timer;
    private bool timerStarted;
    private bool canShoot = true;
    private int currentWeapon;

    private bool[] hasWeapon;
    private int[] ammos;
    private int[] maxAmmos;
    private float[] reloadTime;
    private string[] weaponName;

   

	// Use this for initialization
	void Start () {
        playerCamera = GetComponent<Camera>();

        ammos = new int[3];
        hasWeapon = new bool[3];
        maxAmmos = new int[3];
        reloadTime = new float[3];
        weaponName = new string[3];

        hasWeapon[WEAPON_GUN] = true;
        hasWeapon[WEAPON_AUTO_GUN] = true;
        hasWeapon[WEAPON_GRENADE] = false;

        weaponName[WEAPON_GUN] = "GUN";
        weaponName[WEAPON_AUTO_GUN] = "AUTO GUN";
        weaponName[WEAPON_GRENADE] = "GRENADE";

        ammos[WEAPON_GUN] = 10;
        ammos[WEAPON_AUTO_GUN] = 0;
        ammos[WEAPON_GRENADE] = 0;

        maxAmmos[WEAPON_GUN] = 20;
        maxAmmos[WEAPON_AUTO_GUN] = 20;
        maxAmmos[WEAPON_GRENADE] = 5;

        currentWeapon = WEAPON_GUN;

    }
	
    public void manageCollisions(ControllerColliderHit hit)
    {
        print("Collided with" + hit.collider.gameObject.name);
        if(hit.collider.gameObject.tag == "ammo_gun")
        {
            gunAmmo += 5;
            if (gunAmmo > 20) gunAmmo = 20;
            Destroy(hit.collider.gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        rayFromPlayer = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        Debug.DrawRay(rayFromPlayer.origin, rayFromPlayer.direction * rayLength, Color.red);

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(hasWeapon[WEAPON_GUN] && hasWeapon[WEAPON_AUTO_GUN] && hasWeapon[WEAPON_GRENADE])
            {
                currentWeapon++;
                if (currentWeapon > 2) currentWeapon = 0;
                
            }
            else if(hasWeapon[WEAPON_GUN] && hasWeapon[WEAPON_AUTO_GUN]) {
                if (currentWeapon == WEAPON_GUN) currentWeapon = WEAPON_AUTO_GUN;
                else currentWeapon = WEAPON_GUN;
            }
            else if(hasWeapon[WEAPON_GUN] && hasWeapon[WEAPON_GRENADE])
            {
                if (currentWeapon == WEAPON_GUN) currentWeapon = WEAPON_GRENADE;
                else currentWeapon = WEAPON_GUN;
            }
            else if(hasWeapon[WEAPON_AUTO_GUN] && hasWeapon[WEAPON_GRENADE])
            {
                if (currentWeapon == WEAPON_AUTO_GUN) currentWeapon = WEAPON_GRENADE;
                else currentWeapon = WEAPON_AUTO_GUN;
            }
            else {
            }

            print("Current Weapon: " + weaponName[currentWeapon] + "(" + ammos[currentWeapon] + ")");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentWeapon == WEAPON_GUN && ammos[WEAPON_GUN] >= 1 && canShoot)
            {

                ammos[currentWeapon]--;

                if (Physics.Raycast(rayFromPlayer, out hit, rayLength))
                {
                    print("The Object" + hit.collider.gameObject.name + "is in front of the player");

                    Vector3 positionOfImpact;

                    positionOfImpact = hit.point;

                    Instantiate(impactSparks, positionOfImpact, Quaternion.identity);

                    GameObject objectTargeted;

                    if (hit.collider.gameObject.tag == "target")
                    {
                        objectTargeted = hit.collider.gameObject;
                        objectTargeted.GetComponent<ManageNPC>().gotHit();
                    }
                }
                canShoot = false;
                timer = 0.0f;
                timerStarted = true;
                // gunAmmo--;
                print("you have" + gunAmmo + "bullets left");
            }
        }
	}
}
