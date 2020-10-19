using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menuGameObject;
    public Canvas canvas;
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        canvas = menuGameObject.GetComponent<Canvas>();
        canvas.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        line.SetPosition(0, transform.position);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 10.0f))
        {
            line.SetPosition(1, hit.point);
            if(hit.collider.gameObject.name == "Start")
            {
                canvas.enabled = false;
                Menu.Stage1();
                Menu.Stage2();
                Menu.Stage3();
            }
            else if(hit.collider.gameObject.name == "Stage1")
            {
                canvas.enabled = false;
                Menu.Stage1();
            }
            else if(hit.collider.gameObject.name == "Stage2")
            {
                canvas.enabled = false;
                Menu.Stage2();
            }
            else if(hit.collider.gameObject.name == "Stage3")
            {
                canvas.enabled = false;
                Menu.Stage3();
            }
        }
    }
    static void Stage1()
    {

    }
    static void Stage2()
    {

    }
    static void Stage3()
    {

    }
}
