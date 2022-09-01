using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject hip;
    private float launchForce;
    public float horizontalAirForce;
    private bool hasLanded = false;

    public GameObject joystickGO;
    public FloatingJoystick joystick;
    void Start() {
        launchForce = GameObject.FindGameObjectWithTag("Car").GetComponent<CarBehaviour>().speedWhenCollided;
        Debug.LogError(launchForce);
        rb = hip.GetComponent<Rigidbody>();
        rb.AddForce((Vector3.forward + Vector3.up) * launchForce, ForceMode.VelocityChange);
        joystickGO = GameObject.FindGameObjectWithTag("Joystick");
        joystick = joystickGO.GetComponent<FloatingJoystick>();
    }

    void Update() {
        if(joystick.Horizontal != 0 && !hasLanded) {
            rb.AddForce(Vector3.right * joystick.Horizontal * horizontalAirForce, ForceMode.VelocityChange);
        }
    }

    public void HasLanded() {
        if (!hasLanded) {
            Debug.Log("Target");
            hasLanded = true;
        }
    }
}
