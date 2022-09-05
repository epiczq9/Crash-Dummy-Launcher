using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CarBehaviour : MonoBehaviour
{
    public FloatingJoystick joystick;
    private Rigidbody rb;
    public float carVelocity = 0f;
    public float baseSpeed = 0.5f;
    private float actualSpeed = 0f;
    public float speedWhenCollided;
    private float posX;
    public CinemachineVirtualCamera vCam;

    private float currentTurnAngle = 0f;
    private readonly float maxTurnAngle = 15f;
    private Vector3 lrInput;

    private float lerpTime = 0f;

    public bool pressToGo = true;
    private bool driverLaunched = false;
    public bool onGround = false;

    public GameObject driver;
    public Transform driverPos;

    public GameObject carDestroyed;
    public Rigidbody[] rbParts;

    public Text speedometer;
    void Start() {
        rb = GetComponent<Rigidbody>();
        actualSpeed = baseSpeed;
    }

    private void Update() {
        /*if (Input.GetButtonDown("Fire2")) {
            //Instantiate(driver, driverPos);
            rb.velocity = Vector3.zero;
        }*/
        carVelocity = rb.velocity.z;
        speedometer.text = carVelocity.ToString("F0");
        vCam.GetComponent<CamShake>().ShakeCamera(carVelocity / 200);
    }

    void FixedUpdate() {
        //rb.velocity = new Vector3(joystick.Horizontal * turnSpeed, 0, speed);
        if (onGround) {
            if (pressToGo) {
                if (Input.GetButton("Fire1")) {
                    rb.AddForce(Vector3.forward * actualSpeed, ForceMode.VelocityChange);
                }
            } else {
                rb.AddForce(Vector3.forward * actualSpeed, ForceMode.VelocityChange);
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
                if (Mathf.Abs(joystick.Horizontal) > 0.075f) {
                    actualSpeed = baseSpeed * 0.75f;
                }
                //Debug.Log(joystick.Horizontal);
            } else {
                if (currentTurnAngle != 0) {
                    CorrectTurn();
                }
                actualSpeed = baseSpeed;
            }
        }

        if(rb.velocity.z >= 300f) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 250f);
        }
    }

    void CorrectTurn() {
        if (lerpTime < 0.5f) {
            currentTurnAngle = Mathf.Lerp(currentTurnAngle, 0, lerpTime*2);
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
        if (other.gameObject.CompareTag("RoadBlock")) {
            rb.velocity *= 0.95f;
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
            DemolishCar();
            GetComponent<CarBehaviour>().enabled = false;
        }

        if (collision.gameObject.CompareTag("GuardRail")) {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ramp")) {
            onGround = false;
        }
    }

    void DemolishCar() {
        Debug.Log(carVelocity);
        carDestroyed.SetActive(true);
        carDestroyed.transform.parent.gameObject.SetActive(false);
        carDestroyed.transform.parent = null;
        foreach (Rigidbody rb in rbParts) {
            rb.AddForce((Vector3.forward + Vector3.up) * carVelocity / 4f, ForceMode.VelocityChange);
        }
    }
}
