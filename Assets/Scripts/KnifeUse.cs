using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeUse : MonoBehaviour
{
    public GameObject bread;
    public GameObject leftBread;
    public GameObject rightBread;
    public void OnTriggerEnter(Collider bread)
    {
        Vector3 leftBreadPosition = new Vector3(-0.839f, 1.199f, -0.65f);
        Vector3 rightBreadPosition = new Vector3(-0.839f, 1.199f, -0.43f);
        Destroy(bread);
        Instantiate(leftBread, leftBreadPosition, Quaternion.AngleAxis(90, Vector3.right));
        Instantiate(rightBread, rightBreadPosition, Quaternion.AngleAxis(90, Vector3.right));
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 breadPosition = bread.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
