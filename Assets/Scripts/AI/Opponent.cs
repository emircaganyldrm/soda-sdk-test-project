using UnityEngine;
using TMPro;

public class Opponent : AIBase
{
    [SerializeField] private TextMeshPro _nameText;

    private float _forwardSpeed;
    private float _targetDistance;
    private bool _reachedDestination;


    //---------------------------------------------------------------------------------
    public void Initialize(float forwardSpeed, float targetDistance, string opponentName)
    {
        _forwardSpeed = forwardSpeed;
        _targetDistance = targetDistance;
        _nameText.text = opponentName;
    }


    //---------------------------------------------------------------------------------
    private void Update()
    {
        if (!_reachedDestination)
            Move(_forwardSpeed * Time.deltaTime * Vector3.forward);

        if (transform.position.z >= _targetDistance) // We are hiding opponent when it reaches to the destination distance
        {
            _reachedDestination = true;
            transform.position = Vector3.up * -50f;
        }

        if (GameManager.Instance.LevelPlaying)
            GameManager.Instance.IncreaseOpponentScore(_reachedDestination ? 0 : (_forwardSpeed * Time.deltaTime * Vector3.forward).z); // We are stop increasing opponent score, when it reaches destination
    }
}
