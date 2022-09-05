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
        Debug.LogError(launchForce);
        rb = hip.GetComponent<Rigidbody>();
        rb.AddForce((Vector3.forward + Vector3.up) * launchForce, ForceMode.VelocityChange);
        
        joystickGO = GameObject.FindGameObjectWithTag("Joystick");
        joystick = joystickGO.GetComponent<FloatingJoystick>();

        scoreUI = GameObject.FindGameObjectWithTag("Score");
    }

    void Update() {
        if(joystick.Horizontal != 0 && !hasLanded) {
            rb.AddForce(Vector3.right * joystick.Horizontal * horizontalAirForce, ForceMode.VelocityChange);
            rb.AddForce(Vector3.up * joystick.Vertical * verticalAirForce, ForceMode.VelocityChange);
        }
    }

    public void HasLanded(string tag, int scoreValue) {
        if (string.Compare(tierTarget, tag) != 0) {
            Debug.Log(tag);
            tierTarget = tag;
            score = scoreValue;
            PrintScore();
        }
        if (!hasLanded) {
            hasLanded = true;
            countdownCanvas.SetActive(true);
        }
    }

    public void PrintScore() {
        scoreUI.GetComponent<Text>().text = score.ToString() + " Points";
    }
}
