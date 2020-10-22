using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenCloser : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    public GameObject openDoor;
    public void OnTriggerEnter(Collider openDoor)
    {
        if(openDoor.gameObject.tag == "Open Door" && OvenOpenner.timer >= 2)
        {
            Destroy(openDoor.gameObject);
            door.gameObject.SetActive(true);
            OvenOpenner.timer = 0;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
