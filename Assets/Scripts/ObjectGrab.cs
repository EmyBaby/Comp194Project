using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    public GameObject collidingObject;
    public GameObject objectInHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "LeftHand")
        {
            if (Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") > 0.2f && gameObject.tag == "LeftHand" && collidingObject != null)
            {
                GrabObject();
            }
            if (gameObject.tag == "LeftHand" && Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") < 0.2f && objectInHand != null)
            {
                ReleaseObject();
            }
        }
        
        if (gameObject.tag == "RightHand")
        {
            if (Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger") > 0.2f && gameObject.tag == "RightHand" && collidingObject != null)
            {
                GrabObject();
            }
            if (gameObject.tag == "RightHand" && Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger") < 0.2f && objectInHand != null)
            {
                ReleaseObject();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Knife" || other.gameObject.tag == "Cleaner")
        {
            collidingObject = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        collidingObject = null;
    }

    public void GrabObject()
    {
        objectInHand = collidingObject;
        objectInHand.transform.SetParent(this.transform);
        objectInHand.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ReleaseObject()
    {
        objectInHand.GetComponent<Rigidbody>().isKinematic = false;
        objectInHand.transform.SetParent(null);
        objectInHand = null;
    }
}
