using UnityEngine;

public abstract class AIBase : MonoBehaviour
{


    //---------------------------------------------------------------------------------
    protected virtual void Move(Vector3 position)
    {
        if (!GameManager.Instance.LevelPlaying) return;

        transform.position += position;
    }
}
