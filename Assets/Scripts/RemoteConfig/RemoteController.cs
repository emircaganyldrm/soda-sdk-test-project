using Soda.Runtime;
using UnityEngine;

namespace RemoteConfig
{
    public abstract class RemoteController : MonoBehaviour
    {
        public void SetRemoteConfigData(RemoteConfigDataSO remoteConfigDataSo)
        {
            if (SodaSDK.RemoteConfig.IsInitialized)
            {
                OnRemoteConfigUpdated(remoteConfigDataSo);
            }
        }

        protected abstract void OnRemoteConfigUpdated(RemoteConfigDataSO remoteConfigDataSo);
    }
}