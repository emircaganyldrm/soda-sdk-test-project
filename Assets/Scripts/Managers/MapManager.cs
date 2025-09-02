using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private List<MapPiece> _activeMapPieces;
    [SerializeField] private float _mapPieceSpacing = 240f;
    [SerializeField] private int _activeMapLimit = 3;


    //---------------------------------------------------------------------------------
    private void Start()
    {
        CreateMap();
    }


    //---------------------------------------------------------------------------------
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // To test reordering easily
        {
            ReorderMapPieces();
        }
    }


    //---------------------------------------------------------------------------------
    private void CreateMap()
    {
        float mapPieceCurrentZPos = -_mapPieceSpacing;

        for (int i = 0; i < _activeMapLimit; i++) // Creating certain amount of map pieces
        {
            if (i != 0) mapPieceCurrentZPos = _activeMapPieces[i - 1].transform.position.z + _mapPieceSpacing;

            GameObject mapPiece = ObjectPoolManager.Instance.GetObjectFromPool(ObjectType.MapPiece); // Getting map piece from pool
            mapPiece.transform.position = new Vector3(0f, 0f, mapPieceCurrentZPos); // Positioning 
            mapPiece.GetComponent<MapPiece>().Initialize(); // Initializing map piece
            _activeMapPieces.Add(mapPiece.GetComponent<MapPiece>()); // Adding to the list. We will need this one when we reordering map pieces
        }
    }


    //---------------------------------------------------------------------------------
    public void ReorderMapPieces() // Repositioning map pieces to make infinite level
    {
        _activeMapPieces[0].transform.position = new Vector3(0f, 0f, _activeMapPieces[0].transform.position.z + _mapPieceSpacing * _activeMapLimit); // Getting first map, and putting it to the last 
        _activeMapPieces[0].Initialize(); // Initializing again to make map piece elements randomized
        _activeMapPieces.Move(_activeMapPieces[0], _activeMapPieces.Count); // Using an attribute to move an element in list easier
    }
}
