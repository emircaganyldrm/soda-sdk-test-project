using UnityEngine;

[CreateAssetMenu]
public class DriftSettingsSO : ScriptableObject
{
    [Header("Settings")]
    public float RotateSpeed;
    public float RotationLerpSpeed;
    public float RotationFixLerpSpeed;
    public float CarMinYRotation = -45f;
    public float CarMaxYRotation = 45f;
    public float BodyMinZRotation = -7f;
    public float BodyMaxZRotation = 7f;

    [Header("Score")]
    public float TimeBtwScoreIncrease;
    public int ScoreIncreaseAmount;
}
