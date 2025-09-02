using UnityEngine;

public class HorizontalMovement : MovementBase
{
    [Header("Settings")]
    [SerializeField] private HorizontalMovementSettingsSO _settings;

    private float _xGoalPosition;


    //---------------------------------------------------------------------------------
    protected override void Start()
    {
        _xGoalPosition = transform.position.x;
    }


    //---------------------------------------------------------------------------------
    public void Update()
    {
        Movement();
    }


    //---------------------------------------------------------------------------------
    private void Movement()
    {
        _xGoalPosition += InputManager.Instance.Direction.x * _settings.Speed;
        _xGoalPosition = Mathf.Clamp(_xGoalPosition, _settings.MinX, _settings.MaxX);

        Move(_xGoalPosition, _settings.HorizontalDamping);
    }
}
