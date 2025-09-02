using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    [SerializeField] private ButtonActionTypes _actionType;

    private bool _clickedOnce;


    //---------------------------------------------------------------------------------
    private void Update()
    {
        if (InputManager.Instance.Touching && !_clickedOnce && _actionType == ButtonActionTypes.StartLevel) // To avoid not have to release finger to start level
        {
            ButtonClickAction();
            _clickedOnce = true;
        }
    }


    //---------------------------------------------------------------------------------
    public void ButtonClickAction()
    {
        switch (_actionType)
        {
            case ButtonActionTypes.StartLevel:
                EventManager.StartLevel();
                break;
            case ButtonActionTypes.RestartLevel:
                EventManager.RestartLevel();
                break;
        }
    }
}


//---------------------------------------------------------------------------------
public enum ButtonActionTypes
{
    StartLevel,
    RestartLevel
}
