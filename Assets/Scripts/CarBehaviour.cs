using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarBehaviour : MonoBehaviour
{
    public FloatingJoystick joystick;
    private Rigidbody rb;
    public float speed = 50f;
    public float speedWhenCollided;
    private float posX;

    private float currentTurnAngle = 0f;
    private readonly float maxTurnAngle = 15f;
    private Vector3 lrInput;

    private float lerpTime = 0f;

    public bool pressToGo = true;
    private bool driverLaunched = false;
    public bool onGround = false;

    public GameObject driver;
    public Transform driverPos;

    public Text speedometer;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        /*if (Input.GetButtonDown("Fire2")) {
            //Instantiate(driver, driverPos);
            rb.velocity = Vector3.zero;
        }*/
        speedometer.text = rb.velocity.z.ToString("F0");
    }

    void FixedUpdate() {
        //rb.velocity = new Vector3(joystick.Horizontal * turnSpeed, 0, speed);
        if (onGround) {
            if (pressToGo) {
                if (Input.GetButton("Fire1")) {
                    rb.AddForce(Vector3.forward * speed, ForceMode.VelocityChange);
                }
            } else {
                rb.AddForce(Vector3.forward * speed, ForceMode.VelocityChange);
            }
        }


        if (onGround) {
            if (joystick.Horizontal != 0) {
                currentTurnAngle = maxTurnAngle * joystick.Horizontal;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentTurnAngle, transform.eulerAngles.z);
                //posX = (rb.velocity.z * joystick.Horizontal) / 300;
                //transform.position += new Vector3(posX, 0, 0);
                lrInput = new Vector3(joystick.Horizontal, 0, 0);
                rb.MovePosition(transform.position + lrInput * rb.velocity.z / 3 * Time.deltaTime);
                //rb.AddForce(lrInput * speed, ForceMode.VelocityChange);
            } else {
                if (currentTurnAngle != 0) {
                    CorrectTurn();
                }
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
        if (other.gameObject.CompareTag("SpeedoMeter")) {
            speedWhenCollided = rb.velocity.z;
            //Debug.LogError(speedWhenCollided);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ramp")) {
            onGround = true;
        }

        if (collision.gameObject.CompareTag("EndOfRoad") && !driverLaunched) {
            rb.velocity = Vector3.zero;
            driverLaunched = true;
            Instantiate(driver, driverPos);
            GetComponent<CarBehaviour>().enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ramp")) {
            onGround = false;
        }
    }
}
