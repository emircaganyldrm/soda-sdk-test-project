using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    public Vector2 Direction { get; private set; }
    public bool Touching { get; private set; }

    private Vector2 _touchPosition;
    private int _touchID;
    private float _multiplier;

    private const int NO_TOUCH = -1;
    private const float DIRECTON_MULTIPLIER = 10f;


    //---------------------------------------------------------------------------------
    private void Awake()
    {
        _touchID = NO_TOUCH;

        _multiplier = DIRECTON_MULTIPLIER / Screen.width; // To make multiplier relative to screen size
    }


    //---------------------------------------------------------------------------------
    public void Update()
    {
        GatherInput();
    }


    //---------------------------------------------------------------------------------
    private void GatherInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            _touchID = 0;
            _touchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _touchID = NO_TOUCH;
            Direction = Vector2.zero;
        }

        if (_touchID != NO_TOUCH)
        {
            Direction = ((Vector2)Input.mousePosition - _touchPosition) * _multiplier;
            _touchPosition = Input.mousePosition;
        }

        Touching = _touchID != NO_TOUCH;
    }
}