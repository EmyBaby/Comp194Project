using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuGameObject;
    public GameObject rightHand;
    public Canvas canvas;
    public LineRenderer line;
    public static bool singleScene;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        canvas = menuGameObject.GetComponent<Canvas>();
        canvas.enabled = true;
        singleScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(rightHand.transform.position, rightHand.transform.forward);
        line.SetPosition(0, rightHand.transform.position);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 10.0f))
        {
            line.SetPosition(1, hit.point);
            if (Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") >= 0.30 || Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") >= 0.30)
            {
                if(hit.collider.gameObject.name == "StartButton")
                {
                    Debug.Log("Pressed start");
                    canvas.enabled = false;
                    Menu.Stage1();
                    singleScene = false;
                    
                }
                else if(hit.collider.gameObject.name == "Stage1Button")
                {
                    Debug.Log("Pressed stage 1");
                    canvas.enabled = false;
                    Menu.Stage1();
                    singleScene = true;
                }
                else if(hit.collider.gameObject.name == "Stage2Button")
                {
                    Debug.Log("Pressed stage 2");
                    canvas.enabled = false;
                    Menu.Stage2();
                    singleScene = true;
                    
                }
                else if(hit.collider.gameObject.name == "Stage3Button")
                {
                    Debug.Log("Pressed stage 3");
                    canvas.enabled = false;
                    Menu.Stage3();
                    singleScene = true;
                }
            }
        }
        else
        {
            line.SetPosition(1, rightHand.transform.position + (rightHand.transform.forward * 10));
        }
    }
    
    static void Stage1()
    {
        SceneManager.LoadScene("Stage1");
    
    }
    
    static void Stage2()
    {
        SceneManager.LoadScene("Stage2");
    }
    
    static void Stage3()
    {
        SceneManager.LoadScene("Stage3");
    }
}
