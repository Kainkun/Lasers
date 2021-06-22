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
    }
}
