using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [Header("Data")]
    [SerializeField] private CurrencyDataSO _data;


    //---------------------------------------------------------------------------------
    private void Start()
    {
        UIManager.Instance.UpdateCurrencyAmountText(_data.CurrencyAmount); // Updating currency amoun text on canvas
    }


    //---------------------------------------------------------------------------------
    public void IncreaseCurrency(int increaseAmount) // Increasing currency amount
    {
        _data.CurrencyAmount += increaseAmount;

        UIManager.Instance.UpdateCurrencyAmountText(_data.CurrencyAmount);
    }
}
