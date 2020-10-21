using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGrabber : MonoBehaviour
{
    public GameObject tray;
    public GameObject chickenTray;
    void OnTriggerEnter(Collider tray)
    {
        if(tray.transform.tag == "Tray Holder")
        {
            Destroy(gameObject);
            Vector3 trayPosition = tray.transform.position;
            Instantiate(chickenTray, trayPosition, Quaternion.AngleAxis(0, Vector3.right));
        }
    }
    void Update()
   {
       
   } 
}
