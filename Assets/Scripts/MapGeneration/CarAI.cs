using UnityEngine;

public class CarAI : AIBase, IInteractable
{
    private float _forwardSpeed;


    //---------------------------------------------------------------------------------
    public void Initialize(float forwardSpeed)
    {
        _forwardSpeed = forwardSpeed;
    }


    //---------------------------------------------------------------------------------
    private void Update()
    {
        Move(_forwardSpeed * Time.deltaTime * Vector3.forward);

        if (EventManager.GetPlayerZPosition() - 10f >= transform.position.z) // When this object is a certaing distance behind from player, we are returning this back to the pool
        {
            ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
        }
    }


    //---------------------------------------------------------------------------------
    public void InteractionAction()
    {
        EventManager.LevelCompleted(); // Completing level when player hits car ai
    }
}
