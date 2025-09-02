using System;
using System.Collections.Generic;
using System.Linq;
using RemoteConfig;
using Soda.Runtime;
using UnityEngine;

public class RemoteConfigManager : Singleton<RemoteConfigManager>
{
    [SerializeField] private RemoteConfigDataSO remoteConfigDataSo;
    
    private RemoteController[]  _remoteControllers;
    private void OnEnable()
    {
        SodaSDK.OnInitialized += SendRemoteData;
        EventManager.StartLevel += SendRemoteData;
        EventManager.RestartLevel += SendRemoteData;
    }

    private void OnDisable()
    {
        SodaSDK.OnInitialized -= SendRemoteData;
        EventManager.StartLevel -= SendRemoteData;
        EventManager.RestartLevel -= SendRemoteData;
    }

    private void Awake()
    {
        _remoteControllers = FindObjectsOfType<RemoteController>();
    }

    private void Start()
    {
        SendRemoteData();
    }

    private void SendRemoteData()
    {
        if (!SodaSDK.RemoteConfig.IsInitialized) return;
        
        remoteConfigDataSo.CarColor = SodaSDK.RemoteConfig.GetColor("carColor");
        remoteConfigDataSo.CarSpeed  = SodaSDK.RemoteConfig.GetFloat("carSpeed");
        remoteConfigDataSo.useDiamonds =  SodaSDK.RemoteConfig.GetBool("useDiamonds");
        foreach (RemoteController remoteController in _remoteControllers)
        {
            remoteController.SetRemoteConfigData(remoteConfigDataSo);
        }
    }
}
