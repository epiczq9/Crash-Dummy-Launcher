using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HipsScript : MonoBehaviour
{
    public UnityEvent hasLanded;
    public GameObject ragdollMain;
    private Rigidbody rb;
    private int score = 0;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if(rb.velocity == Vector3.zero) {

        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Target")) {
            //hasLanded.Invoke();
        }
        if (!collision.gameObject.CompareTag("Untagged")) {
            ragdollMain.GetComponent<RagdollBehaviour>().HasLanded(collision.gameObject.tag, collision.gameObject.GetComponent<ScoreValue>().scoreValue);
        }
    }
}
