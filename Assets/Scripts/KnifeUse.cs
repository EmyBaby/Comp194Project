using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeUse : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject other;
    public GameObject blade;
    bool handColliding;
    GameObject hand;
    void Start()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Rigidbody>()) 
        {
            hand = other.gameObject;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        hand = null;
    }
    public void GrabObject()
    {
        other = hand;
        other.transform.SetParent (this.transform);
        other.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void ReleaseObject()
    {
        other.GetComponent<Rigidbody>().isKinematic = false;
        other.transform.SetParent (null);
        other = null;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") > 0.2f && hand != null) 
        {
            GrabObject();
        }
        if(Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") < 0.2f && other != null) 
        {
            ReleaseObject();
        }
    }
}
