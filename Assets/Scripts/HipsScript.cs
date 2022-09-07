using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Timers;

public class HipsScript : MonoBehaviour
{
    private bool launched = false;
    public UnityEvent hasLanded;
    public GameObject ragdollMain;
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        TimersManager.SetTimer(this, 2f, Launched);
    }

    private void FixedUpdate() {
        //Debug.Log(rb.velocity);
        if (rb.velocity == Vector3.zero && launched) {
            Debug.Log("STUCK");
            ragdollMain.GetComponent<RagdollBehaviour>().HasLanded();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Untagged")) {
            //hasLanded.Invoke();
            ragdollMain.GetComponent<RagdollBehaviour>().HasLanded(collision.gameObject.tag, collision.gameObject.GetComponent<ScoreValue>().scoreValue);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("EndArea")) {
            ragdollMain.GetComponent<RagdollBehaviour>().HasLanded();
        } else if (!other.gameObject.CompareTag("Untagged")) {
            //hasLanded.Invoke();
            ragdollMain.GetComponent<RagdollBehaviour>().HoopPass(other.gameObject.tag, other.gameObject.GetComponent<ScoreValue>().scoreValue);
        }
    }

    private void Launched() {
        launched = true;
    }
}
