using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCollisionWithPlayer : MonoBehaviour {

	void OnControllerColliderHit(ControllerColliderHit hit)
    {
        transform.GetChild(0).GetComponent<ManageWeapons>().manageCollisions(hit);
    }
}
