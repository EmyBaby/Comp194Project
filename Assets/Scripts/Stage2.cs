using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject panel;
    public LineRenderer line;
    TMPro.TextMeshProUGUI guide;
    List<string> UIText;
    bool triggerPressed;
    bool raycastMode;
    int textIndex;
    float coolDown;
    public Transform leftBread;
    public Transform knife;
    // Start is called before the first frame update
    void Start()
    {
        guide = panel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        raycastMode = true;
        coolDown = 2;
        UIText = new List<string>();
        UIText.Add(@"Welcome to Stage 2 of Kitchen Safety Training. This stage will guide you on maintaining safety while using a knife in a kitchen.");
        UIText.Add(@"According to a survey by the National Electronic Injury Surveillance System, 330,000 hospitals visits a year are due to knife accidents. We can
        limit by carefully handling a knife");
        UIText.Add(@"First thing to keep in mind is to avoid touching near the sharp-end of the blade and maintaing a firm grip on the handle in order to have 
        strong control over the movement of the knife. Pick up the knife by only grabbing the handle. Do not touch the blade!");
        UIText.Add(@" Nice! Slice the bread on the cutting board to create two equal slices.");
        UIText.Add(@"Place the knife down.");
        UIText.Add(@"SUCCESS!");
    }

    // Update is called once per frame
 void Update()
    {
        if(guide.text != UIText[textIndex])
        {
            guide.text = UIText[textIndex];
        }
        
        if (raycastMode)
        {
            line.gameObject.SetActive(true);
            line.SetPosition(0, rightHand.transform.position);
            panel.transform.GetChild(1).gameObject.SetActive(true);    
            RaycastHit hit;
            if (Physics.Raycast(new Ray(rightHand.transform.position, rightHand.transform.forward), out hit, 10.0f))
            {
                line.SetPosition(1, hit.point);
                if ((Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") >= 0.30 || Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") >= 0.30) && coolDown >= 2)
                {
                    if(hit.collider.gameObject.name == "NextButton")
                    {
                        TriggerPress();
                        textIndex++;
                    }
                }
            }
            else
            {
                line.SetPosition(1, rightHand.transform.position + (rightHand.transform.forward * 10));
            }
        }
        else
        {
            line.gameObject.SetActive(false);
            panel.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (textIndex >= 2 && textIndex <= 4)
        {
            raycastMode = false;
            KnifeControll();

        }
    }

    void FixedUpdate()
    {
        if (triggerPressed)
        {
            triggerPressed = false;
        }
        if (coolDown < 2)
        {
            coolDown += Time.deltaTime;
        }

    }

    void TriggerPress()
    {
        triggerPressed = true;
        coolDown = 0;
    }

    void KnifeControll()
    {
        // panel.transform.GetChild(2).gameObject.SetActive(true);
        bool objectGrabbed = knife.transform.gameObject.GetComponent<OVRGrabbable>().isGrabbed;
        if(textIndex == 2 && objectGrabbed)
        {
            textIndex++;
        }
        else if(textIndex == 3 && knife.gameObject.GetComponent<KnifeUse>().GetCut())
        {
            textIndex++;
        }
        else if(textIndex == 4 && objectGrabbed == false)
        {
            textIndex++;
            // panel.transform.GetChild(2).gameObject.SetActive(false);
            raycastMode = true;
        }
    }
}