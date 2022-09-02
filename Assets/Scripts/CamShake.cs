using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    void Start() {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }
}
