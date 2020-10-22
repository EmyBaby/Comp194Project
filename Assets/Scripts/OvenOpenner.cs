using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenOpenner : MonoBehaviour
{
    public GameObject door;
    public GameObject openDoor;
    public static float timer;

    public void OnTriggerEnter(Collider door)
    {
        if(door.gameObject.tag == "Door" && timer >= 2)
        {
            door.gameObject.SetActive(false);
            Vector3 openDoorPosition = new Vector3(-2.036f, 0.062f, 0.658f);
            Instantiate(openDoor, openDoorPosition, Quaternion.AngleAxis(90, Vector3.right));
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        timer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (timer < 2)
        {
            timer += Time.deltaTime;
        }
    }
}
