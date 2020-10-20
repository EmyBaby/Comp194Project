using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandState : MonoBehaviour
{
    public GameObject dirtPrefab;
    public GameObject bubbleGen;
    GameObject bubbles;
    List<GameObject> dirtList;
    bool isDirty;
    bool dirtOnHand;
    bool inWater;
    bool touchCleaner;
    bool cleaning;
    float waterTime;
    float soapTime;
    float destroyTimer;
    int handFlip;
    
    // Start is called before the first frame update
    void Start()
    {
        isDirty = true;
        dirtOnHand = false;
        handFlip = 1;
        inWater = false;
        touchCleaner = false;
        cleaning = false;
        bubbles = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDirty)
        {
            if (!dirtOnHand)
            {
                if (gameObject.tag == "LeftHand")
                {
                    handFlip = -1;
                }
                Instantiate(dirtPrefab, transform.position, transform.rotation, transform).transform.localPosition += new Vector3(0.05844998f * handFlip, -0.02250004f, -0.0662000f);
                Instantiate(dirtPrefab, transform.position, transform.rotation, transform).transform.localPosition += new Vector3(0.02400005f * handFlip, -0.04050004f, -0.03750002f);
                Instantiate(dirtPrefab, transform.position, transform.rotation, transform).transform.localPosition += new Vector3(0.02139997f * handFlip, -0.05710006f, -0.06659997f);
                Instantiate(dirtPrefab, transform.position, transform.rotation, transform).transform.localPosition += new Vector3(0.02929997f * handFlip, -0.05710006f, -0.02610004f);
                Instantiate(dirtPrefab, transform.position, transform.rotation, transform).transform.localPosition += new Vector3(0.0288f * handFlip, -0.02400005f, -0.02090001f);
                Instantiate(dirtPrefab, transform.position, transform.rotation, transform).transform.localPosition += new Vector3(0.06040001f * handFlip, -0.02272f, -0.0281f);
                Instantiate(dirtPrefab, transform.position, transform.rotation, transform).transform.localPosition += new Vector3(0.0453999f * handFlip, -0.0005999804f, -0.074f);
                Instantiate(dirtPrefab, transform.position, transform.rotation, transform).transform.localPosition += new Vector3(0.05869997f * handFlip, -0.04980004f, -0.05869997f);
                Instantiate(dirtPrefab, transform.position, transform.rotation, transform).transform.localPosition += new Vector3(0.01520002f * handFlip, -0.03600001f, -0.0704f);
                dirtList = new List<GameObject>();
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.tag == "Dirt")
                    {
                        dirtList.Add(transform.GetChild(i).gameObject);
                    }
                }
                foreach (GameObject dirt in dirtList)
                {
                    // Debug.Log(dirt);
                }
                dirtOnHand = true;
            }
            if (soapTime >= 5 && bubbles == null)
            {
                bubbles = Instantiate(bubbleGen, transform.position, transform.rotation, transform);
                waterTime = 0;
            }
            if (bubbles && waterTime >= 10)
            {
                isDirty = false;
                cleaning = true;
            }
            // isDirty = false;
            // cleaning = true;

        }
        else
        {
            if (dirtOnHand)
            {
                if (cleaning)
                {
                    foreach (GameObject dirt in dirtList)
                    {
                        dirt.AddComponent<Rigidbody>();
                        dirt.transform.SetParent(null);
                        dirt.GetComponent<SphereCollider>().enabled = true;
                    }
                    GameObject.Destroy(bubbles);
                    cleaning = false;
                    // Debug.Log("rigidbodies added");
                }
                destroyTimer += Time.deltaTime;
                if (destroyTimer >= 5)
                {
                    foreach (GameObject dirt in dirtList)
                    {
                        UnityEngine.Object.Destroy(dirt);
                    }
                    // Object.Destroy(transform.Find("DirtSphere(Clone)").gameObject);
                    // Debug.Log("Dirt destroyed");
                    destroyTimer = 0;
                    dirtOnHand = false;
                }
                waterTime = 0;
                soapTime = 0;
            }
        }
        // Debug.Log(gameObject.GetComponent<OVRGrabber>().m_grabbedObj.gameObject.tag);
        try{
        // if (gameObject.GetComponent<OVRGrabber>().m_grabbedObj.gameObject)
        // {
            if (gameObject.GetComponent<OVRGrabber>().m_grabbedObj.gameObject.tag == "Cleaner")
            {
                touchCleaner = true;
                // Debug.Log("Reads cleaner tag & sets touchCleaner true");
            }
            else
            {
                touchCleaner = false;
            }
        }
        catch(NullReferenceException ex){

        }
        // }
        // else
        // {
        //     touchCleaner = false;
        // }
    }

    void FixedUpdate()
    {
        if (inWater)
        {
            waterTime += Time.deltaTime;
            // Debug.Log(waterTime);
        }
        if (waterTime >= 5 && touchCleaner)
        {
            soapTime += Time.deltaTime;
            // Debug.Log(soapTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Water")
        {
            inWater = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            inWater = false;
            Debug.Log("Not touching water");
        }
    }

    public float getWaterTime()
    {
        return waterTime;
    }

    public float getSoapTime()
    {
        return soapTime;
    }
}
