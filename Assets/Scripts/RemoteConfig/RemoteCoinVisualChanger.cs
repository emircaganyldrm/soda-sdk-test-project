using System;
using UnityEngine;

namespace RemoteConfig
{
    public class RemoteCoinVisualChanger : RemoteController
    {
        [SerializeField] private RemoteConfigDataSO remoteConfigDataSo;
        
        [SerializeField] private GameObject coinObject;
        [SerializeField] private GameObject diamondObject;
        
        protected override void OnRemoteConfigUpdated(RemoteConfigDataSO remoteConfigDataSo)
        {
            coinObject.SetActive(!remoteConfigDataSo.useDiamonds);
            diamondObject.SetActive(remoteConfigDataSo.useDiamonds);
        }

        private void OnEnable()
        {
            if (remoteConfigDataSo != null)
            {
                coinObject.SetActive(!remoteConfigDataSo.useDiamonds);
                diamondObject.SetActive(remoteConfigDataSo.useDiamonds);
            }
        }
    }
}