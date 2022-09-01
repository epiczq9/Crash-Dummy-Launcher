using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlockDemolish : MonoBehaviour
{
    public GameObject roadBlockDestroyed;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Car")) {
            Debug.Log("TRIGGER");
            roadBlockDestroyed.SetActive(true);
            gameObject.SetActive(false);
            roadBlockDestroyed.transform.parent = null;
        }
    }
}
