using UnityEngine;

[CreateAssetMenu]
public class HorizontalMovementSettingsSO : ScriptableObject
{
    [Header("Movement")]
    public float Speed;
    public float HorizontalDamping;

    [Header("Clamp")]
    public float MinX;
    public float MaxX;
}
