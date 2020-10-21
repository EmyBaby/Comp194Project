using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3 : MonoBehaviour
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

    public GameObject oven;
    public GameObject ovenKnob;
    public Text temperature;
    int time;
    bool isOvenOn;
    bool isInOven;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        temperature = GetComponent<Text>();
        isOvenOn = false;
        isInOven = false;

        guide = panel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        raycastMode = true;
        coolDown = 2;
        UIText = new List<string>();
        UIText.Add(@"Welcome to Stage 3 of Kitchen Safety Training. This stage will show you the importance of cooking to the right temperature.");
        UIText.Add(@"In this stage we will be using chicken as an example. The proper temperatures to cook chicken is 165 degrees fahrenheit.");
        UIText.Add(@"First start by putting the chicken into the oven.");
        UIText.Add(@"Once the temperature reads 165 degrees fahrenheit take it out of the oven.");
        UIText.Add(@"When cooking meat products it is important to make sure it is cook throughly as to avoid becoming sick.");
    }

    // Update is called once per frame
 void Update()
    {
        temperature.text = time.ToString();
        if (ovenKnob.transform.localEulerAngles.y >= 35)
        {
            isOvenOn = true;
        }

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
            CookChicken();
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

    void CookChicken()
    {
        if(textIndex == 2 && isInOven)
        {
            textIndex++;
        }
        else if(textIndex == 3 && time == 165)
        {
            textIndex++;
        }
        else if(textIndex == 4 && isInOven == false)
        {
            
        }
    }

    void OnTriggerStay()
    {
        if(isOvenOn)
        {
            time++;
        }
    }
}