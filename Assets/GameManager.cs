using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public CinemachineBrain mainCinemachineBrain;
    public Camera mainCamera;
    public Player player;
    public UiBar chargeBar;
    
    public static CinemachineBasicMultiChannelPerlin camNoise;

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        mainCinemachineBrain = CinemachineCore.Instance.GetActiveBrain(0);
        mainCamera = mainCinemachineBrain.OutputCamera;
        player = FindObjectOfType<Player>();
        chargeBar = GameObject.Find("ChargeBG").GetComponent<UiBar>();
        camNoise = mainCinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void FreezeFrame(float time)
    {
        StartCoroutine(C_FreezeFrame(time));
    }
    
    IEnumerator C_FreezeFrame(float time)
    {
        Time.timeScale = 0.05f;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
    }

    public void SetCamShakeAmplitude(float amplitude)
    {
        camNoise.m_AmplitudeGain = amplitude;
    }

    public void CamShake(float amplitude, float time)
    {
        StartCoroutine(C_CamShake(amplitude, time));
    }
    
    IEnumerator C_CamShake(float amplitude, float time)
    {
        SetCamShakeAmplitude(amplitude);
        yield return new WaitForSecondsRealtime(time);
        SetCamShakeAmplitude(0);
    }
}
