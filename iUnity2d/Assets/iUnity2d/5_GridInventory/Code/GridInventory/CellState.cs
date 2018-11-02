using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellState : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Vector2Int CellPos;
    public bool isFree = true;

    public GridManager _GridManager;

    private UnityEngine.UI.Image _CellImage;

    private void Awake()
    {
        //gridInventoryManager = gameObject.GetComponentInParent<GIManager>();
        _CellImage = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    void Start () {
	}

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        _GridManager.CurrentCell = this;
        _GridManager._GIManager._GridManager = _GridManager;
    }
    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        _GridManager.CurrentCell = null;
    }

    public void SetColor(Color newColor)
    {
        _CellImage.color = newColor;
    }

}
