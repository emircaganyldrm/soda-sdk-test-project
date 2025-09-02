using UnityEngine;

public class ForwardMovement : MovementBase
{
    [Header("Settings")]
    [SerializeField] private ForwardMovementSettingsSO _settings;

    private float _currentSpeed;
    private float _speedIncreaseTimer;


    //---------------------------------------------------------------------------------
    protected override void Start()
    {
        _currentSpeed = _settings.BaseSpeed; // setting current speed
    }


    //---------------------------------------------------------------------------------
    private void Update()
    {
        Move(_currentSpeed * Time.deltaTime * Vector3.forward);

        if (_canMove)
        {
            GameManager.Instance.IncreasePlayerScore((_currentSpeed * Time.deltaTime * Vector3.forward).z);

            if (_currentSpeed < _settings.MaxSpeed) // We increase the current speed at specified time intervals, if its not more than max speed
            {
                if (_speedIncreaseTimer >= _settings.TimeBtwSpeedIncrease)
                {
                    _currentSpeed += _settings.SpeedIncreaseAmount;
                    _speedIncreaseTimer = 0f;
                }
                else
                {
                    _speedIncreaseTimer += Time.deltaTime;
                }
            }
        }           
    }
}
