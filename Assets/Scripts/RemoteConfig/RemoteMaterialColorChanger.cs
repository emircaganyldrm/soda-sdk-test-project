using UnityEngine;

namespace RemoteConfig
{
    public class RemoteMaterialColorChanger : RemoteController
    {
        [SerializeField] private Renderer[] renderers;
        
        protected override void OnRemoteConfigUpdated(RemoteConfigDataSO remoteConfigDataSo)
        {
            ChangeColor(remoteConfigDataSo.CarColor);
        }

        private void ChangeColor(Color color)
        {
            foreach (var renderer in renderers)
            {
                if (renderer != null)
                {
                    renderer.material.color = color;
                }
            }
        }
    }
}