using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HipsScript : MonoBehaviour
{
    public UnityEvent hasLanded;
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Target")) {
            hasLanded.Invoke();
        }
    }
}
