using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCanvas : MonoBehaviour
{
    void Update() {
        if (Input.GetButton("Fire1")) {
            Destroy(gameObject);
        }
    }
}
