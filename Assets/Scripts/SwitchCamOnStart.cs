using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Timers;

public class SwitchCamOnStart : MonoBehaviour
{
    public CinemachineVirtualCamera vCam1, vCam2;
    public float interval;
    void Start() {
        vCam1.Priority = 25;
        vCam2.Priority = 10;
        TimersManager.SetTimer(this, interval, ChangeCam);
    }

    public void ChangeCam() {
        vCam1.Priority = 10;
        vCam2.Priority = 25;
    }
}
