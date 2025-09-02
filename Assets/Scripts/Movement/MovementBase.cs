using UnityEngine;

public abstract class MovementBase : MonoBehaviour
{
    protected bool _canMove;


    //---------------------------------------------------------------------------------
    #region Subscribing & Unsubscribing
    private void OnEnable()
    {
        EventManager.StartLevel += EnableMovement;
        EventManager.LevelCompleted += DisableMovement;
        EventManager.GetPlayerZPosition += GetPlayerZPosition;
    }

    private void OnDisable()
    {
        EventManager.StartLevel -= EnableMovement;
        EventManager.LevelCompleted -= DisableMovement;
        EventManager.GetPlayerZPosition -= GetPlayerZPosition;
    }
    #endregion


    //---------------------------------------------------------------------------------
    protected virtual void Start()
    {

    }


    //---------------------------------------------------------------------------------
    protected void Move(Vector3 position)
    {
        if (!_canMove) return;

        transform.position += position;
    }


    //---------------------------------------------------------------------------------
    protected void Move(float xGoal, float damping)
    {
        if (!_canMove) return;

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, xGoal, (1f - damping) * Time.deltaTime * 30f), transform.position.y, transform.position.z);
    }


    //---------------------------------------------------------------------------------
    protected virtual void EnableMovement() => _canMove = true;
    protected virtual void DisableMovement() => _canMove = false;


    //---------------------------------------------------------------------------------
    public float GetPlayerZPosition() => transform.position.z;
}