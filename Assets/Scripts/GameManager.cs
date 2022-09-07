using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int nextLevel;
    public void ChangeScene() {
        SceneManager.LoadScene(nextLevel);
    }
    public void ChangeScene(int i) {
        SceneManager.LoadScene(i);
    }

    
}
