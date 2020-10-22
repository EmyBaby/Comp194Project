using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGrabber : MonoBehaviour
{
    void OnCollisionEnter(Collision c) {
        var joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = c.rigidbody;
    }
}
