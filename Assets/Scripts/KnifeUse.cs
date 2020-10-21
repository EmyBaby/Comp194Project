using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeUse : MonoBehaviour
{
    public GameObject bread;
    public GameObject leftBread;
    public GameObject rightBread;
    bool cut;

    public void OnTriggerEnter(Collider bread)
    {
        if(bread.gameObject.tag == "Bread")
        {
            Destroy(bread.gameObject);
            Vector3 leftBreadPosition = new Vector3(-0.839f, 1.199f, -0.65f);
            Vector3 rightBreadPosition = new Vector3(-0.839f, 1.199f, -0.43f);
            Instantiate(leftBread, leftBreadPosition, Quaternion.AngleAxis(90, Vector3.right));
            Instantiate(rightBread, rightBreadPosition, Quaternion.AngleAxis(90, Vector3.right));
            cut = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 breadPosition = bread.transform.position;
        cut = false;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public bool GetCut()
    {
        return cut;
    }
}
