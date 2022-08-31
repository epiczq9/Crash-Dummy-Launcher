using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelMovement : MonoBehaviour
{
    public FloatingJoystick joystick;
    private float posX;
    private float lerpTime = 0f;

    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    public float acceleration = 500f;
    public float brakingForce = 300f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;
    private float currentTurnAngle = 0f;

    void Start()
    {
        
    }

    private void FixedUpdate() {
        currentAcceleration = acceleration;
        

        if (Input.GetButton("Jump")) {
            currentBrakeForce = brakingForce;
        } else {
            currentBrakeForce = 0f;
        }

        
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;
        
        
        frontRight.brakeTorque = currentBrakeForce;
        frontLeft.brakeTorque = currentBrakeForce;
        backRight.brakeTorque = currentBrakeForce;
        backLeft.brakeTorque = currentBrakeForce;

        if(joystick.Horizontal != 0) {
            currentTurnAngle = maxTurnAngle * EaseIn(joystick.Horizontal, 3);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentTurnAngle, transform.eulerAngles.z);
            posX = EaseIn(joystick.Horizontal, 3) / 5;
            transform.position += new Vector3(posX, 0, 0);
        } else {
            if(currentTurnAngle != 0) {
                CorrectTurn();
            }
        }
    }

    void CorrectTurn() {
        if(lerpTime < 1) {
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
}
