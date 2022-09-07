using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RagdollBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject hip;
    private float launchForce;
    public float horizontalAirForce;
    public float verticalAirForce;
    private bool hasLanded = false;

    public string tierTarget = "null";
    public float score;
    private GameObject scoreUI;

    public GameObject joystickGO;
    public FloatingJoystick joystick;

    public GameObject countdownCanvas;
    void Start() {
        launchForce = GameObject.FindGameObjectWithTag("Car").GetComponent<CarBehaviour>().speedWhenCollided;
        //Debug.LogError(launchForce);
        rb = hip.GetComponent<Rigidbody>();
        rb.AddForce(launchForce * (Vector3.forward + Vector3.up), ForceMode.VelocityChange);
        
        joystickGO = GameObject.FindGameObjectWithTag("Joystick");
        joystick = joystickGO.GetComponent<FloatingJoystick>();

        scoreUI = GameObject.FindGameObjectWithTag("Score");
        countdownCanvas = GameObject.FindGameObjectWithTag("Countdown");
    }

    void Update() {
        if(joystick.Horizontal != 0 && !hasLanded) {
            rb.AddForce(horizontalAirForce * joystick.Horizontal * Vector3.right, ForceMode.VelocityChange);
            rb.AddForce(joystick.Vertical * verticalAirForce * Vector3.up, ForceMode.VelocityChange);
        }
    }

    public void HasLanded() {
        if (!hasLanded) {
            hasLanded = true;
            countdownCanvas.GetComponent<CountdownCanvas>().ActivateCountdown();
            //PrintScore();
        }
    }
    public void HasLanded(string tag, int scoreValue) {
        if (string.Compare(tierTarget, tag) != 0) {
            //Debug.Log(tag);
            tierTarget = tag;
            score = scoreValue;
            //PrintScore();
        }
        if (!hasLanded) {
            hasLanded = true;
            countdownCanvas.GetComponent<CountdownCanvas>().ActivateCountdown();
        }
    }

    public void HoopPass(string tag, int scoreValue) {
        if (!hasLanded) {
            Debug.Log(tag);
            tierTarget = tag;
            score += scoreValue;
        }
    }

    public void PrintScore() {
        scoreUI.GetComponent<Text>().text = score.ToString() + " Points";
    }
}
