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
    public GameObject trayHolder;
    public Text temperature;
    int time;
    bool isOvenOn;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        temperature = GetComponent<Text>();
        isOvenOn = false;

        guide = panel.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        raycastMode = true;
        coolDown = 2;
        UIText = new List<string>();
        UIText.Add(@"Welcome to Stage 3 of Kitchen Safety Training. This stage will show you the importance of cooking at the right temperature and how to stay safe while in the proximity of high heat content.");
        UIText.Add(@"In this stage we will be using chicken to bake as an example. The proper temperatures to cook chicken is to have the chicken's interanl temperature at 165 degrees fahrenheit.
        This can be done by leaving the chicken in the oven at 350*F for 25-30 minutes.");
        UIText.Add(@"First start by putting the chicken into the oven by openning the oven and grabbing the chicken on the tray with the tray holder attached to your hand.");
        UIText.Add(@"Place the chicken tray on the bottom of the oven and clase the oven door.");
        UIText.Add(@"Turn the oven knob about 270 degrees to change the temperature of the oven to 350*F and let the chicken cook for 30 minutes."); //10 sec in reality; by 3 min = 1 sec?
        UIText.Add(@"Your chicken is fully cooked! Now you can safely enjoy the chicken!");
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
        if(textIndex == 2 && trayHolder.gameObject.GetComponent<ChickenInOven>().IsInOven())
        {
            textIndex++;
        }
        else if(textIndex == 3 && time == 165)
        {
            textIndex++;
        }
        else if(textIndex == 4 )
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