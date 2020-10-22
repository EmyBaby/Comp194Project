using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenKnob : MonoBehaviour
{
    float previousY;
    Vector3 knobEulers;
    bool handTouching;
    bool handColliding;
    bool ovenOn;
    bool isCooking;
    GameObject hand;
    public GameObject heat;
    
    // Start is called before the first frame update
    void Start()
    {
        handTouching = false;
        ovenOn = false;
        isCooking = false;
        knobEulers = transform.localEulerAngles;
        heat.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (handColliding)
        {
            knobEulers = transform.localEulerAngles;
            if (hand.tag == ("LeftHand") && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") >= 0.30 || hand.tag == ("RightHand") && Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") >= 0.30)
            {
                if (handTouching)
                {
                    transform.localEulerAngles += new Vector3(0, hand.transform.localEulerAngles.y - previousY, 0);
                    Debug.Log(transform.localEulerAngles.y);
                    if (transform.localEulerAngles.y > 315 && transform.localEulerAngles.y <= 337.5)
                    transform.localEulerAngles = new Vector3(0, 315, 0);
                    if (transform.localEulerAngles.y > 337.5)
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    handTouching = true;
                }
            }
            previousY = hand.transform.localEulerAngles.y;
        }

        if(transform.localEulerAngles.y >= 30 && transform.localEulerAngles.y <= 300)
        {
            heat.SetActive(true);
            heat.GetComponent<Light>().intensity = 1;
            ovenOn = true;
            isCooking = false;
            if(transform.localEulerAngles.y >= 240 && transform.localEulerAngles.y <= 300)
            {
                isCooking = true;
                heat.GetComponent<Light>().intensity = 2;
            }
        }
    }
    public bool OvenIsOn()
    {
        return ovenOn;
    }
    public bool ChickenIsCooking()
    {
        return isCooking;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("LeftHand") || other.tag == ("RightHand"))
        {
            hand = other.gameObject;
            handColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        handTouching = false;
        handColliding = false;
        hand = null;
    }
}
