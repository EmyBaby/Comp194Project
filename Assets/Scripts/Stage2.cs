using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2 : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject panel;
    public GameObject player;
    public LineRenderer line;
    TMPro.TextMeshProUGUI guide;
    List<string> UIText;
    Vector3 pos1;
    Vector3 pos2;
    Vector3 rot1;
    Vector3 rot2;
    bool triggerPressed;
    bool raycastMode;
    int textIndex;
    float coolDown;
    public Transform leftBread;
    public Transform knife;
    
    // Start is called before the first frame update
    void Start()
    {
        pos1 = new Vector3(-1.869f, 1.738f, -3.012f);
        rot1 = new Vector3(0f, 180f, 0f);
        pos2 = new Vector3(-2.243f, 1.79f, -0.432f);
        rot2 = new Vector3(0f, 270f, 0f);
        guide = panel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        raycastMode = true;
        coolDown = 2;
        UIText = new List<string>();
        UIText.Add(@"Welcome to Stage 2 of Kitchen Safety Training. This stage will guide you on maintaining safety while using a knife in a kitchen.");
        UIText.Add(@"According to a survey by the National Electronic Injury Surveillance System, 330,000 hospitals visits a year are due to knife accidents. We can
        limit this by carefully handling a knife");
        UIText.Add(@"First thing to keep in mind is to avoid touching near the sharp-end of the blade and maintaing a firm grip on the handle in order to have strong control over the movement of the knife. 
        Pick up the knife by only grabbing the handle. Do not touch the blade!");
        UIText.Add(@"Perfect! The blade seems to be a little dirty. Put your hygiene knowledge to the test and wash the knife.
        Always keep the blade facing away from you.");
        UIText.Add(@"Squeaky clean! Now that that's out of the way, we're going to try using the knife to cut.");
        UIText.Add(@"Slice the bread on the cutting board through the middle to create two equal slices.");
        UIText.Add(@"Place the knife down.");
        UIText.Add(@"SUCCESS! You may now move on to stage 3.");
        panel.transform.parent.position = pos1;
        panel.transform.parent.eulerAngles = rot1;
        player.transform.position = new Vector3(-1.906f, 0f, -1.613f);
        player.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        knife.transform.position = new Vector3(-2.537f, 1.224f, -2.411f);
        knife.transform.eulerAngles = new Vector3(90f, -90f, 0f);
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
                        if (textIndex == 4)
                        {
                            knife.transform.position = new Vector3(-0.931f, 1.249f, -0.095f);
                            knife.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                            player.transform.position = new Vector3(0.112f, 0f, -0.49f);
                            player.transform.eulerAngles = new Vector3(0f, 270f, 0f);
                            panel.transform.parent.position = pos2;
                            panel.transform.parent.eulerAngles = rot2;
                        }

                        if (textIndex == 7)
                        {
                            SceneManager.LoadScene("Stage3");
                        }
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


        // if (textIndex == 5)
        // {
        //     player.transform.position = new Vector3(0.112f, 1.283f, -0.49f);
        //     player.transform.eulerAngles = new Vector3(0f, 270f, 0f);
        // }

        if ((textIndex >= 2 && textIndex <= 3) || (textIndex >= 5 && textIndex <= 6))
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
        if (textIndex == 2 && objectGrabbed)
        {
            textIndex++;
        }
        else if (textIndex == 3 && knife.GetComponent<ObjectState>().getIsDirty() == false)
        {
            textIndex++;
            raycastMode = true;
        }
        else if (textIndex == 5 && knife.gameObject.GetComponent<KnifeUse>().GetCut())
        {
            textIndex++;
        }
        else if (textIndex == 6 && objectGrabbed == false)
        {
            textIndex++;
            // panel.transform.GetChild(2).gameObject.SetActive(false);
            raycastMode = true;
        }
    }
}