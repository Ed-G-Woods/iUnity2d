using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIManager : MonoBehaviour {

    public GridManager _GridManager;
    public InventoryManager _InventroryManager;

    private void Awake()
    {
        if (_GridManager==null)
        {
            _GridManager = GetComponentInChildren<GridManager>();
        }
        if (_InventroryManager == null)
        {
            _InventroryManager = GetComponentInChildren<InventoryManager>();
        }
    }

    public void setSize(Vector2 size) //for Scrollable
    {
        RectTransform _rect = GetComponent<RectTransform>();
        _rect.sizeDelta = size;
    }

    void Start () {
        //TODO:for Scrollable  ScrollRect不应该放在GIManager上层。应该在GIManager下，GridManager&InventoryManager上，可以考虑加一个border
        //可以考虑加一个border，把GridManager&InventoryManager放在border中，for Scrollable的代码也放在border中，。并把CellState中的与GIManager的耦合去除
        //for Scrollable
        float x = _GridManager.GridNumX * _GridManager.GIData.CellSpace;
        float y = _GridManager.GridNumY * _GridManager.GIData.CellSpace;
        Vector2 newsize = new Vector2(x, y);
        setSize(newsize);
        RectTransform _GridMGRect = _GridManager.GetComponent<RectTransform>();
        _GridMGRect.anchoredPosition = new Vector2(_GridManager.GIData.CellSpace / 2, -(_GridManager.GIData.CellSpace / 2));
        //-for Scrollable
    }
    void Update () {
	}
}
