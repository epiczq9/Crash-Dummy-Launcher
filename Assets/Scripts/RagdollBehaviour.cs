using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject hip;
    public float launchForce;
    public float horizontalAirForce;
    void Start() {
        rb = hip.GetComponent<Rigidbody>();
        rb.AddForce((Vector3.forward + Vector3.up) * launchForce, ForceMode.VelocityChange);
    }

    void Update() {
    
    }
}
