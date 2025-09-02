using UnityEngine;

namespace RemoteConfig
{
    public class RemoteMoveSpeedController : RemoteController
    {
        [SerializeField] private ForwardMovementSettingsSO forwardMovementSettings;
        
        protected override void OnRemoteConfigUpdated(RemoteConfigDataSO remoteConfigDataSo)
        {
            forwardMovementSettings.BaseSpeed = remoteConfigDataSo.CarSpeed;
        }
    }
}