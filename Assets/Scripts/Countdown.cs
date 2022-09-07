using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Timers;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour {
    public GameObject gameManager;
    private string timeRemaining;
    private GameObject ragdoll;
    void Start() {
        TimersManager.SetTimer(this, 8f, SwitchScene);
        ragdoll = GameObject.FindGameObjectWithTag("Ragdoll");
    }

    // Update is called once per frame
    void Update() {
        timeRemaining = TimersManager.RemainingTime(SwitchScene).ToString("F0");
        Debug.Log(TimersManager.RemainingTime(SwitchScene));
        if (TimersManager.RemainingTime(SwitchScene) <= 5) {
            ragdoll.GetComponent<RagdollBehaviour>().PrintScore();
            GetComponent<Text>().text = timeRemaining;
        }
    }

    public void SwitchScene() {
        Debug.Log("GOTOV TAJMER");
        gameManager.GetComponent<GameManager>().ChangeScene();
    }
}
