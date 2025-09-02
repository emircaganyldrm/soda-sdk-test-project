using System;
using System.Threading.Tasks;
using UnityEngine;

public class OpponentManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Opponent _opponent;

    [Header("Settings")]
    [SerializeField] private string _opponentName;
    [SerializeField] private float _opponentForwardSpeed;
    [SerializeField] private float _opponentTargetDistance;


    //---------------------------------------------------------------------------------
    #region Subscribing & Unsubscribing
    private void OnEnable()
    {
        EventManager.StartLevel += InitializeOpponent;
    }

    private void OnDisable()
    {
        EventManager.StartLevel -= InitializeOpponent;
    }
    #endregion


    //---------------------------------------------------------------------------------
    private async void InitializeOpponent()
    {
        await Task.Delay(TimeSpan.FromSeconds(.5f)); // Applying some delay to the opponent when game starts

        _opponent.Initialize(_opponentForwardSpeed, _opponentTargetDistance, _opponentName);

        UIManager.Instance.SetOpponentNameText(_opponentName);
    }
}
