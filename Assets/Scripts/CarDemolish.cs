using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDemolish : MonoBehaviour
{
    public GameObject carDestroyed, car;
    public Rigidbody[] rbParts;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("EndOfRoad")) {
            float carVelocity = car.GetComponent<CarBehaviour>().carVelocity;
            Debug.Log(carVelocity);
            carDestroyed.SetActive(true);
            gameObject.SetActive(false);
            carDestroyed.transform.parent = null;
            foreach (Rigidbody rb in rbParts) {
                rb.AddForce((Vector3.forward + Vector3.up), ForceMode.VelocityChange);
            }
        }
    }
}
