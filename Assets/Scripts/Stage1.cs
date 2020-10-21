using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject panel;
    public GameObject cuttingBoard;
    public LineRenderer line;
    TMPro.TextMeshProUGUI guide;
    GameObject soap;
    GameObject sponge;
    List<string> UIText;
    bool triggerPressed;
    bool raycastMode;
    float coolDown;
    int textIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        guide = panel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        panel.transform.GetChild(2).gameObject.SetActive(false);
        raycastMode = true;
        coolDown = 2;
        soap = GameObject.Find("Soap");
        sponge = GameObject.Find("Sponge");
        UIText = new List<string>();
        UIText.Add(@"Hello! Welcome to Kitchen Safety Training. This training will teach you a couple things about kitchen safety.
The first and most basic concept that must be applied at all times is hygiene!");
        UIText.Add(@"The CDC estimates one in six Americans are affected by food poisioning, causing 128,000 hospitalizations and 3,000 deaths a year.
Did you know the easiest way to prevent this is washing your hands? Let's try!");
        UIText.Add(@"Your hands are filthy. The most thorough way to clean is to wash your hands for at least 20 seconds with soap and water.
For your first task, open the faucet and wet your hands for 5 seconds.");
        UIText.Add(@"Great! Now it's time to use soap.
Rub both your hands with the soap bar for 5 seconds until bubbles form!");
        UIText.Add(@"Perfect. Now it's time to rinse off your hands for a deep clean.
Rinse your hands off for about 10 seconds until the soap washes off and dirt falls.");
        UIText.Add(@"Your hands are all clean! You're ready to handle objects. However, basic hygiene applies to kitchenware as well.
For your next task, you will repeat the same cleaning process with a cutting board and soapy sponge");
        UIText.Add(@"Wet the board for 5 seconds, rub it with the sponge for 5 seconds, then rinse it for 10 seconds.");
        UIText.Add(@"Good job! You now have basic experience with kitchen hygiene.");
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
                        triggerPressed = true;
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

        if (textIndex == 1)
        {
            cuttingBoard.SetActive(false);
        }

        if (textIndex >= 2 && textIndex <= 4)
        {
            raycastMode = false;
            WashHandGUI();

        }

        if (textIndex == 5)
        {
            GameObject.Find("Sink_Counter").transform.GetChild(4).GetChild(0).gameObject.GetComponent<Sink>().setTriggerMode(true);
            soap.SetActive(false);
            sponge.SetActive(true);
            cuttingBoard.SetActive(true);
        }

        if (textIndex == 6)
        {
            raycastMode = false;
            WashObjectGUI();
        }
    }

    void FixedUpdate()
    {
        if (triggerPressed)
        {
            coolDown = 0;
            triggerPressed = false;
        }
        if (coolDown < 2)
        {
            coolDown += Time.deltaTime;
        }

    }

    void WashHandGUI()
    {

        panel.transform.GetChild(2).gameObject.SetActive(true);
        if (textIndex == 2)
        {
            panel.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = $"Left hand wet: {Mathf.Round(leftHand.GetComponent<ObjectState>().getWaterTime() * 100)/100} sec Right hand wet: {Mathf.Round(rightHand.GetComponent<ObjectState>().getWaterTime() * 100)/100} sec";
            if (leftHand.GetComponent<ObjectState>().getWaterTime() >= 5 && rightHand.GetComponent<ObjectState>().getWaterTime() >= 5)
            textIndex++;
        }
        if (textIndex == 3)
        {
            panel.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = $"Left hand soap: {Mathf.Round(leftHand.GetComponent<ObjectState>().getSoapTime() * 100)/100} sec Right hand soap: {Mathf.Round(rightHand.GetComponent<ObjectState>().getSoapTime() * 100)/100} sec";
            if (leftHand.GetComponent<ObjectState>().getSoapTime() >= 5 && rightHand.GetComponent<ObjectState>().getSoapTime() >= 5)
            textIndex++;
        }
        if (textIndex == 4)
        {
            float leftHandWaterTime = Mathf.Round(leftHand.GetComponent<ObjectState>().getWaterTime() * 100)/100;
            float rightHandWaterTime = Mathf.Round(rightHand.GetComponent<ObjectState>().getWaterTime() * 100)/100;
            
            if (leftHand.GetComponent<ObjectState>().getIsDirty() == false)
            {
                leftHandWaterTime = 10;
            }
            if (rightHand.GetComponent<ObjectState>().getIsDirty() == false)
            {
                rightHandWaterTime = 10;
            }
            
            panel.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = $"Left hand rinse: {leftHandWaterTime} sec Right hand rinse: {rightHandWaterTime} sec";
            if (leftHand.GetComponent<ObjectState>().getIsDirty() == false && rightHand.GetComponent<ObjectState>().getIsDirty() == false)
            {
                textIndex++;
                panel.transform.GetChild(2).gameObject.SetActive(false);
                raycastMode = true;
            }

        }
    }

    void WashObjectGUI()
    {
        panel.transform.GetChild(2).gameObject.SetActive(true);
        if (cuttingBoard.GetComponent<ObjectState>().getIsDirty() == false)
        {
            textIndex++;
            raycastMode = true;
        }
        if (cuttingBoard.GetComponent<ObjectState>().getSoapTime() >= 5 && cuttingBoard.GetComponent<ObjectState>().getWaterTime() < 10)
        {
            panel.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = $"Rinse: {Mathf.Round(cuttingBoard.GetComponent<ObjectState>().getWaterTime() * 100)/100} sec";
        }
        if (cuttingBoard.GetComponent<ObjectState>().getWaterTime() >= 5 && cuttingBoard.GetComponent<ObjectState>().getSoapTime() < 5)
        {
            panel.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = $"Lather: {Mathf.Round(cuttingBoard.GetComponent<ObjectState>().getSoapTime() * 100)/100} sec";
        }
        if (cuttingBoard.GetComponent<ObjectState>().getWaterTime() < 5 && cuttingBoard.GetComponent<ObjectState>().getSoapTime() == 0)
        {
            panel.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = $"Wet: {Mathf.Round(cuttingBoard.GetComponent<ObjectState>().getWaterTime() * 100)/100} sec";
        }
    }
}
