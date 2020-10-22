using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenInOven : MonoBehaviour
{
    public GameObject ovenFloor;
    public GameObject chicken;
    public GameObject chickenPrefab;
    void OnTriggerEnter(Collider ovenFloor)
    {
        if(ovenFloor.gameObject.tag == "OvenFloor")
        {
            chicken.gameObject.SetActive(false);
            Vector3 chickenOvenPosition = new Vector3(-2.046f, 0.125f, 1.468f);
            Instantiate(chickenPrefab, chickenOvenPosition, Quaternion.AngleAxis(0, Vector3.up));
        }
    }
}
