using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HipsScript : MonoBehaviour
{
    public UnityEvent hasLanded;
    public GameObject ragdollMain;  
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Target")) {
            //hasLanded.Invoke();
        }
        if (!collision.gameObject.CompareTag("Untagged")) {
            ragdollMain.GetComponent<RagdollBehaviour>().HasLanded(collision.gameObject.tag);
        }
    }
}
