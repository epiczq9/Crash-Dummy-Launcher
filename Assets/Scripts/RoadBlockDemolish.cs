using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlockDemolish : MonoBehaviour
{
    public GameObject roadBlockDestroyed, car;
    public Rigidbody[] rbParts;

    private void Start() {
        car = GameObject.FindGameObjectWithTag("Car");
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Car")) {
            float carVelocity = car.GetComponent<CarBehaviour>().carVelocity;
            Debug.Log(carVelocity);
            Debug.Log("ROADBLOCK");
            if (carVelocity > 35f) {
                roadBlockDestroyed.SetActive(true);
                gameObject.SetActive(false);
                roadBlockDestroyed.transform.parent = null;
                foreach (Rigidbody rb in rbParts) {
                    //Vector3 launchDir = (transform.position - car.transform.position);
                    //launchDir = new Vector3(launchDir.x / 3, 0, launchDir.z);
                    rb.AddForce(Vector3.forward * carVelocity, ForceMode.VelocityChange);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Car")) {
            float carVelocity = car.GetComponent<CarBehaviour>().carVelocity;
            //Debug.Log(carVelocity);
            if (carVelocity > 35f) {
                roadBlockDestroyed.SetActive(true);
                gameObject.SetActive(false);
                roadBlockDestroyed.transform.parent = null;
                Vector3 launchDir = (transform.position - car.transform.position);
                launchDir = new Vector3(launchDir.x / 3, 0, Mathf.Abs(launchDir.z));
                //Debug.Log(launchDir);
                foreach (Rigidbody rb in rbParts) {
                    rb.AddForce(0.8f * carVelocity * launchDir.normalized, ForceMode.VelocityChange);
                    
                }
            }
        }
    }
}
