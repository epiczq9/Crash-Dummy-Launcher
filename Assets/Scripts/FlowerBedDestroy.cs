using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBedDestroy : MonoBehaviour
{
    public GameObject flowerbedDestroyed, car;
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
                flowerbedDestroyed.SetActive(true);
                gameObject.SetActive(false);
                flowerbedDestroyed.transform.parent = null;
                foreach (Rigidbody rb in rbParts) {
                    Vector3 launchDir = (transform.position - car.transform.position);
                    launchDir = new Vector3(launchDir.x / 3, 0, launchDir.z).normalized;
                    rb.AddForce(launchDir * carVelocity/2, ForceMode.VelocityChange);
                }
            }
        }
    }
}
