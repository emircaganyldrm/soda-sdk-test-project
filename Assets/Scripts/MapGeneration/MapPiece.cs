using System;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] private RoadLane[] _roadLanes;

    [Header("Car Creation")]
    [SerializeField] private float _minSpacing;
    [SerializeField] private float _maxSpacing;
    [SerializeField] private float _carMinZPosition;
    [SerializeField] private float _carMaxZPosition;


    [Header("Coin Creation")]
    [SerializeField] private float _coinSpacing;
    [SerializeField] private float _coingGroupSpacing;
    [SerializeField] private float _coinMinZPosition;
    [SerializeField] private float _coinMaxZPosition;
    private List<GameObject> _createdCoins = new List<GameObject>();


    //---------------------------------------------------------------------------------
    public void Initialize()
    {
        CreateCars();
        ReturnCoinsToPool(); // Returning existing coins back to the pool, we want coin positions / amounts randomized every time
        CreateCoins();
    }


    //---------------------------------------------------------------------------------
    private void CreateCars() // Creating cars on the lanes randomly
    {
        float _currentCarZPos = _carMinZPosition;

        for (int i = 0; i < _roadLanes.Length; i++) 
        {
            for (int j = 0; j < _roadLanes[i].CarAmount; j++)
            {
                _currentCarZPos += UnityEngine.Random.Range(_minSpacing, _maxSpacing); // applying car spacing randomly
                if (_currentCarZPos >= _carMaxZPosition) continue;
                GameObject carAI = ObjectPoolManager.Instance.GetObjectFromPool(ObjectType.CarAI);
                carAI.transform.position = transform.position + new Vector3(_roadLanes[i].XPosition, 0f, _currentCarZPos);
                carAI.GetComponent<CarAI>().Initialize(_roadLanes[i].CarSpeed);
            }

            _currentCarZPos = _carMinZPosition;
        }
    }


    //---------------------------------------------------------------------------------
    private void CreateCoins() // Creating coins on the lanes randomly
    {
        float _currentCoinZPos = _coinMinZPosition + UnityEngine.Random.Range(0f, _coingGroupSpacing);

        for (int i = 0; i < _roadLanes.Length; i++)
        {
            for (int j = 0; j < _roadLanes[i].CoinGroupAmount; j++) // We are creating coin groups, each group has certain amount of coins
            {
                for (int k = 0; k < _roadLanes[i].CoinAmount; k++)
                {
                    _currentCoinZPos += _coinSpacing; // We are using same spacing value 
                    if (_currentCoinZPos >= _coinMaxZPosition) continue;
                    GameObject coin = ObjectPoolManager.Instance.GetObjectFromPool(ObjectType.Coin);
                    coin.transform.position = transform.position + new Vector3(_roadLanes[i].XPosition, 0f, _currentCoinZPos);
                    _createdCoins.Add(coin);
                }
                _currentCoinZPos += _coingGroupSpacing;
            }

            _currentCoinZPos = _coinMinZPosition;
        }
    }


    //---------------------------------------------------------------------------------
    private void ReturnCoinsToPool()
    {
        foreach (var coin in _createdCoins)
            ObjectPoolManager.Instance.ReturnObjectToPool(coin);

        _createdCoins.Clear();
    }
}


//---------------------------------------------------------------------------------
[Serializable]
public struct RoadLane
{
    public float XPosition;
    public int CarAmount;
    public float CarSpeed;
    public int CoinAmount;
    public int CoinGroupAmount;
}
