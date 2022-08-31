using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public FloatingJoystick joystick;
    private Rigidbody rb;
    public float speed = 50f;
    private float posX;

    private float currentTurnAngle = 0f;
    private readonly float maxTurnAngle = 15f;

    private float lerpTime = 0f;

    public bool pressToGo = true;

    public GameObject driver;
    public Transform driverPos;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (Input.GetButtonDown("Fire2")) {
            Instantiate(driver, driverPos);
        }
    }

    void FixedUpdate() {
        //rb.velocity = new Vector3(joystick.Horizontal * turnSpeed, 0, speed);
        if (pressToGo) {
            if (Input.GetButton("Fire1")) {
                rb.AddForce(Vector3.forward * speed, ForceMode.VelocityChange);
            }
        } else {
            rb.AddForce(Vector3.forward * speed, ForceMode.VelocityChange);
        }
        
        if (joystick.Horizontal != 0) {
            currentTurnAngle = maxTurnAngle * EaseIn(joystick.Horizontal, 3);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentTurnAngle, transform.eulerAngles.z);
            posX = (rb.velocity.z * joystick.Horizontal) / 300;
            transform.position += new Vector3(posX, 0, 0);
        } else {
            if (currentTurnAngle != 0) {
                CorrectTurn();
            }
        }
    }

    void CorrectTurn() {
        if (lerpTime < 1) {
            currentTurnAngle = Mathf.Lerp(currentTurnAngle, 0, lerpTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentTurnAngle, transform.eulerAngles.z);
            lerpTime += Time.deltaTime;
        } else {
            currentTurnAngle = 0;
            lerpTime = 0;
        }

    }
    float EaseIn(float num, int pow) {
        return Mathf.Pow(num, pow);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Boost")) {
            rb.velocity *= 1.5f;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("EndOfRoad")) {
            Instantiate(driver, driverPos);
            Debug.LogError("RAGDOLL");
            GetComponent<CarBehaviour>().enabled = false;
        }
    }

}
