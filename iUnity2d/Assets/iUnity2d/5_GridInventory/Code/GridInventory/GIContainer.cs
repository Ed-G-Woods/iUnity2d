using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Container under GIManager-ScrollRect , Container for GridManager and InventoryManager
public class GIContainer : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    private GridManager _GridManager;
    private InventoryManager _InventoryManager;

    public void setSize(Vector2 size) //for Scrollable
    {
        RectTransform _rect = GetComponent<RectTransform>();
        _rect.sizeDelta = size;
    }
    public void ContainerSetup()
    {
        float x = _GridManager.GridNumX * _GridManager.GIData.CellSpace;
        float y = _GridManager.GridNumY * _GridManager.GIData.CellSpace;
        Vector2 newsize = new Vector2(x, y);
        setSize(newsize);

        RectTransform _GridMGRect = _GridManager.GetComponent<RectTransform>();
        _GridMGRect.anchoredPosition = new Vector2(_GridManager.GIData.CellSpace / 2, -(_GridManager.GIData.CellSpace / 2));
    }

    private void Awake()
    {
        _GridManager = GetComponentInChildren<GridManager>();
        _InventoryManager = GetComponentInChildren<InventoryManager>();
    }

    void Start () {
        ContainerSetup();
    }

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GIManager.GgetGIManager._GridManager = _GridManager;
        GIManager.GgetGIManager._InventroryManager = _InventoryManager;
    }
    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //         GIManager.GgetGIManager._GridManager = null;
        //         GIManager.GgetGIManager._InventroryManager = null;
        GIManager.GgetGIManager._GridManager.ResetColorInFreecells();
    }
}
