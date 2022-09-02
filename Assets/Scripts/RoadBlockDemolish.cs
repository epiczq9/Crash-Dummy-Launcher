using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlockDemolish : MonoBehaviour
{
    public GameObject roadBlockDestroyed, car;
    public Rigidbody[] rbParts;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Car")) {
            float carVelocity = car.GetComponent<CarBehaviour>().carVelocity;
            Debug.Log(carVelocity);
            if (carVelocity > 50f) {
                roadBlockDestroyed.SetActive(true);
                gameObject.SetActive(false);
                roadBlockDestroyed.transform.parent = null;
                foreach (Rigidbody rb in rbParts) {
                    rb.AddForce(Vector3.forward * carVelocity, ForceMode.VelocityChange);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Car")) {
            float carVelocity = car.GetComponent<CarBehaviour>().carVelocity;
            Debug.Log(carVelocity);
            if (carVelocity > 50f) {
                roadBlockDestroyed.SetActive(true);
                gameObject.SetActive(false);
                roadBlockDestroyed.transform.parent = null;
                foreach (Rigidbody rb in rbParts) {
                    Vector3 launchDir = (transform.position - car.transform.position).normalized;
                    rb.AddForce(launchDir * carVelocity, ForceMode.VelocityChange);
                }
            }
        }
    }
}
