using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtWaterDetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            transform.parent.GetComponent<ObjectState>().setTouchCleaner(true);
        }
    }

    void onTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            transform.parent.GetComponent<ObjectState>().setTouchCleaner(false);
        }
    }
}
