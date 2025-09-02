using UnityEngine;

namespace RemoteConfig
{
    [CreateAssetMenu(fileName = "RemoteConfigData", menuName = "RemoteConfig/RemoteConfigData", order = 0)]
    public class RemoteConfigDataSO : ScriptableObject
    {
        public Color CarColor;
        public float CarSpeed;
        public bool useDiamonds;
    }
}