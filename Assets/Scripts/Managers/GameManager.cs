using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public bool LevelPlaying;

    private float _playerScore;
    private float _opponentScore;
    private float _distanceBetweenPlayerAndOpponent;
    private bool _raceStarted;
    private bool _playerBeatOpponent;


    //---------------------------------------------------------------------------------
    #region Subscribing & Unsubscribing
    private void OnEnable()
    {
        EventManager.StartLevel += LevelStarted;
        EventManager.LevelCompleted += LevelCompleted;
        EventManager.RestartLevel += RestartScene;
    }

    private void OnDisable()
    {
        EventManager.StartLevel -= LevelStarted;
        EventManager.LevelCompleted -= LevelCompleted;
        EventManager.RestartLevel -= RestartScene;
    }
    #endregion


    //---------------------------------------------------------------------------------
    #region Level Actions
    private void LevelStarted()
    {
        LevelPlaying = true;
    }


    //---------------------------------------------------------------------------------
    private void LevelCompleted()
    {
        LevelPlaying = false;
    }


    //---------------------------------------------------------------------------------
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion


    //---------------------------------------------------------------------------------
    #region Score Actions
    public void IncreasePlayerScore(float increaseAmount)
    {
        _playerScore += increaseAmount;

        UIManager.Instance.UpdatePlayerScoreText((int)_playerScore);
    }


    //---------------------------------------------------------------------------------
    public void IncreaseOpponentScore(float increaseAmount)
    {
        _opponentScore += increaseAmount;

        SetDistanceBetweenPlayerAndOpponent();
    }


    //---------------------------------------------------------------------------------
    private void SetDistanceBetweenPlayerAndOpponent()
    {
        if (_playerBeatOpponent) return;

        _distanceBetweenPlayerAndOpponent = _opponentScore - _playerScore;

        UIManager.Instance.UpdateDistanceBetweenPlayerAndOpponentText((int)_distanceBetweenPlayerAndOpponent);

        if (_distanceBetweenPlayerAndOpponent > 50) // Giving some gap to avoid the race end before it starts. 50 is a magic number
        {
            UIManager.Instance.SetOpponentPanelActivity(true);
            _raceStarted = true;  // Starting race
        }
        else
        {
            if (_raceStarted)  // Player beat opponent
            {
                UIManager.Instance.SetOpponentPanelActivity(false);
                UIManager.Instance.ShowBeatMessageText();
                _playerBeatOpponent = true;
            }
        }
    }
    #endregion
}
