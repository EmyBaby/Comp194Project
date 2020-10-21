using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeUse : MonoBehaviour
{
    public GameObject bread;
    public GameObject leftBread;
    public GameObject rightBread;
    public GameObject tinyBread;
    float coolDown;
    int cutCount;
    bool cut1;

    public void OnTriggerEnter(Collider bread)
    {
        if(bread.gameObject.tag == "Bread" && coolDown >= 1)
        {
            coolDown = 0;
            Destroy(bread.gameObject);
            Vector3 leftBreadPosition = new Vector3(-0.839f, 1.199f, -0.65f);
            Vector3 rightBreadPosition = new Vector3(-0.839f, 1.199f, -0.43f);
            Instantiate(leftBread, leftBreadPosition, Quaternion.AngleAxis(90, Vector3.right));
            Instantiate(rightBread, rightBreadPosition, Quaternion.AngleAxis(90, Vector3.right));
            cut1 = true;
            coolDown = 0;
        }

        if ((bread.gameObject.name == "LeftBread(Clone)" || bread.gameObject.name == "RightBread(Clone)") && coolDown >= 1)
        {
            Debug.Log(bread.gameObject);
            Vector3 leftBreadPosition = bread.transform.position + new Vector3(0f, 0f, -0.0518f);
            Vector3 rightBreadPosition = bread.transform.position + new Vector3(0f, 0f, 0.0544f);            
            Destroy(bread.gameObject);
            Instantiate(tinyBread, leftBreadPosition, Quaternion.AngleAxis(90, Vector3.right));
            Instantiate(tinyBread, rightBreadPosition, Quaternion.AngleAxis(90, Vector3.right));
            cutCount++;
            coolDown = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 breadPosition = bread.transform.position;
        cut1 = false;
        coolDown = 1;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (coolDown < 1)
        {
            coolDown += Time.deltaTime;
        }
    }

    public bool GetCut()
    {
        return cut1;
    }

    public bool GetCut2()
    {
        if (cutCount == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
