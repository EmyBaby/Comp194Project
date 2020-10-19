using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkKnob : MonoBehaviour
{
    float previousY;
    Vector3 knobEulers;
    bool handTouching;
    bool handColliding;
    GameObject hand;
    
    // Start is called before the first frame update
    void Start()
    {
        handTouching = false;
        knobEulers = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (handColliding)
        {
            knobEulers = transform.eulerAngles;
            if (hand.tag == ("LeftHand") && Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") >= 0.30 || hand.tag == ("RightHand") && Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger") >= 0.30)
            {
                if (handTouching)
                {
                    transform.eulerAngles += new Vector3(0, hand.transform.eulerAngles.y - previousY, 0);
                    if (gameObject.tag == ("RightKnob"))
                    {
                        if (transform.eulerAngles.y > 270)
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        if (transform.eulerAngles.y > 90)
                        transform.eulerAngles = new Vector3(0, 90, 0);
                    }
                    if (gameObject.tag == ("LeftKnob"))
                    {
                        if (transform.eulerAngles.y > 180)
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        if (transform.eulerAngles.y < 90)
                        transform.eulerAngles = new Vector3(0, 90, 0);
                    }
                }
                else
                {
                    handTouching = true;
                }
            }
            previousY = hand.transform.eulerAngles.y;
        }
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
