using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject _levelStartPanel;
    [SerializeField] private GameObject _levelCompletedPanel;
    [SerializeField] private GameObject _opponentScorePanel;
    [SerializeField] private GameObject _beatMessagePanel;

    [Header("Player")]
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TextMeshProUGUI _beatMessageText;

    [Header("Opponent")]
    [SerializeField] private TextMeshProUGUI _distanceBetweenPlayerAndOpponentText;
    [SerializeField] private TextMeshProUGUI _opponentNameText;

    [Header("PopUp")]
    [SerializeField] private TextMeshProUGUI _scorePopUpText;

    [Header("Currency")]
    [SerializeField] private TextMeshProUGUI _currencyAmountText;


    //---------------------------------------------------------------------------------
    #region Subscribing & Unsubscribing
    private void OnEnable()
    {
        EventManager.StartLevel += DisableLevelStartPanels;
        EventManager.LevelCompleted += ActivateSuccessPanel;
    }

    private void OnDisable()
    {
        EventManager.StartLevel -= DisableLevelStartPanels;
        EventManager.LevelCompleted -= ActivateSuccessPanel;
    }
    #endregion


    //---------------------------------------------------------------------------------
    public void UpdatePlayerScoreText(int scoreValue)
    {
        _playerScoreText.text = scoreValue + "M";
    }


    //---------------------------------------------------------------------------------
    private void DisableLevelStartPanels()
    {
        _levelStartPanel.SetActive(false);
    }


    //---------------------------------------------------------------------------------
    private void ActivateSuccessPanel()
    {
        _levelCompletedPanel.SetActive(true);
        _scorePopUpText.gameObject.SetActive(false);
    }


    //---------------------------------------------------------------------------------
    public void SetOpponentPanelActivity(bool state)
    {
        if (_opponentScorePanel.activeSelf == state) return;

        _opponentScorePanel.SetActive(state);
    }


    //---------------------------------------------------------------------------------
    public void SetOpponentNameText(string opponentName)
    {
        _opponentNameText.text = opponentName;
    }


    //---------------------------------------------------------------------------------
    public void UpdateDistanceBetweenPlayerAndOpponentText(int scoreValue)
    {
        _distanceBetweenPlayerAndOpponentText.text = scoreValue + "M";
    }


    //---------------------------------------------------------------------------------
    public void ShowScoreIncreasePopUpText(int textValue, bool isPlayerRotatingRight)
    {
        if (textValue == 0)
            _scorePopUpText.text = "";
        else
            _scorePopUpText.text = "+" + textValue;

        _scorePopUpText.rectTransform.anchoredPosition = new Vector2(isPlayerRotatingRight ? 400 : -400, 0); // Setting popup text position relative to drifting direction
    }


    //---------------------------------------------------------------------------------
    public async void ShowBeatMessageText()
    {
        _beatMessagePanel.SetActive(true);
        _beatMessageText.text = "YOU BEAT " + _opponentNameText.text;

        await Task.Delay(TimeSpan.FromSeconds(2.5f));

        _beatMessagePanel.SetActive(false);
    }


    //---------------------------------------------------------------------------------
    public void UpdateCurrencyAmountText(int currencyAmount)
    {
        _currencyAmountText.text = currencyAmount.ToString();
    }
}
