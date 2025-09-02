using UnityEngine;

public class MapReorderingCollider : MonoBehaviour, IInteractable
{


    //---------------------------------------------------------------------------------
    public void InteractionAction() // We put this middle of the map piece, whenever player hits this, we are telling map manager to reorder maps
    {
        MapManager.Instance.ReorderMapPieces();
    }
}
