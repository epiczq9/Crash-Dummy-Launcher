using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float score = 0f;
    public GameObject ragdoll;
    void Start() {
        ragdoll = GameObject.FindGameObjectWithTag("Ragdoll");
    }

    void FinalScore() {
        
    }
}
