using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Timers;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
 
    void Start() {
        TimersManager.SetTimer(this, 10f, SwitchScene);
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(TimersManager.RemainingTime(SwitchScene));
    }

    public void SwitchScene() {
        Debug.Log("GOTOV TAJMER");
    }
}
