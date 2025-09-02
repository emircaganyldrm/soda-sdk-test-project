using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{


    //---------------------------------------------------------------------------------
    public void InteractionAction()
    {
        CurrencyManager.Instance.IncreaseCurrency(1);

        ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
    }
}
