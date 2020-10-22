using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGrabber : MonoBehaviour
{
    public GameObject chicken;
    public GameObject tray;
    bool chickenGrab;
    void Start()
    {
        chickenGrab = false;
    }
    public void OnTriggerEnter(Collider chicken)
    {
        if(chicken.gameObject.tag == "ChickenTray")
        { 
            //Destroy(chicken.gameObject);
            //Vector3 trayHolderPosition = gameObject.transform.position;
            //Quaternion trayHolderRotation = gameObject.transform.rotation;
            //Instantiate(attachedChicken, trayHolderPosition, trayHolderRotation);
            chicken.transform.localEulerAngles = new Vector3(0, 270, 0);
            chicken.transform.SetParent(tray.transform);
            chicken.transform.localPosition = new Vector3(0.15f, 0.055f, 0f);
            chickenGrab = true;
        }
    }
    public bool ChickenIsGrabbed()
    {
        return chickenGrab;
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
