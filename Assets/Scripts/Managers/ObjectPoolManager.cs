using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField] private PoolObject[] _poolObjects;


    //---------------------------------------------------------------------------------
    private void Awake()
    {
        CreatePool();
    }


    //---------------------------------------------------------------------------------
    private void CreatePool()
    {
        for (int i = 0; i < _poolObjects.Length; i++)
        {
            for (int j = 0; j < _poolObjects[i].Size; j++)
            {
                GameObject obj = Instantiate(_poolObjects[i].Prefab);
                obj.transform.parent = transform;
                obj.SetActive(false);
                _poolObjects[i].ObjectList.Add(obj);
            }
        }
    }


    //---------------------------------------------------------------------------------
    public GameObject GetObjectFromPool(ObjectType type)
    {
        PoolObject targetPoolObject = null;

        foreach (var poolObject in _poolObjects)
            if (type == poolObject.Type)
                targetPoolObject = poolObject;

        foreach (GameObject obj in targetPoolObject.ObjectList)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(targetPoolObject.Prefab); // If there is no object, we expand the object pool
        newObj.transform.parent = transform;
        newObj.SetActive(false);
        targetPoolObject.ObjectList.Add(newObj);

        return newObj;
    }


    //---------------------------------------------------------------------------------
    public void ReturnObjectToPool(GameObject obj)
    {
        // Resetting everything when object has returned back to pool
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        obj.transform.localScale = Vector3.one;
        obj.SetActive(false);
    }
}


//---------------------------------------------------------------------------------
[Serializable]
public class PoolObject
{
    public ObjectType Type;
    public GameObject Prefab;
    public int Size;
    public List<GameObject> ObjectList = new List<GameObject>();
}


//---------------------------------------------------------------------------------
public enum ObjectType
{
    CarAI,
    MapPiece,
    Coin
}
