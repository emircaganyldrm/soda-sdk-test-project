using UnityEngine;

[CreateAssetMenu]
public class ForwardMovementSettingsSO : ScriptableObject
{
    [Header("Movement")]
    public float BaseSpeed;
    public float MaxSpeed;

    [Header("Increasing")]
    public float TimeBtwSpeedIncrease;
    public float SpeedIncreaseAmount;
}
