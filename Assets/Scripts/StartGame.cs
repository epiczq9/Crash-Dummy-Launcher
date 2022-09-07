using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        TimersManager.SetTimer(this, 0.05f, StartTheGame);
    }

    private void StartTheGame() {
        SceneManager.LoadScene(1);
    }
}
