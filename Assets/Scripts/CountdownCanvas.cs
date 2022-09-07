using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownCanvas : MonoBehaviour
{
   public void ActivateCountdown() {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
