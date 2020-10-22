using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGrabber : MonoBehaviour
{
    public GameObject chicken;
    public GameObject tray;
    public void OnTriggerEnter(Collider chicken)
    {
        if(chicken.gameObject.tag == "ChickenTray")
        { 
            //Destroy(chicken.gameObject);
            Vector3 trayHolderPosition = gameObject.transform.position;
            Quaternion trayHolderRotation = gameObject.transform.rotation;
            //Instantiate(attachedChicken, trayHolderPosition, trayHolderRotation);
            chicken.gameObject.transform.parent = tray.transform;
        }
    }
    /*void Update()
    {
        Vector3 trayHolderPosition = gameObject.transform.position;
        Quaternion trayHolderRotation = gameObject.transform.rotation;
        attachedChicken.transform.position = trayHolderPosition;
        attachedChicken.transform.rotation = trayHolderRotation;
    }*/
    /*void OnCollisionEnter(Collision c) {                  //Sticky collision
        var joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = c.rigidbody;
    }*/
}
