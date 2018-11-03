using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIManager : MonoBehaviour {

    public static GIManager GgetGIManager; 
    public GridManager _GridManager;    //当前的_GridManager
    public InventoryManager _InventroryManager;     //当前的_InventroryManager
    public GISelected _GISelected;

    private void Awake()
    {
        GgetGIManager = this;

        if (_GridManager==null)
        {
            _GridManager = GetComponentInChildren<GridManager>();
        }
        if (_InventroryManager == null)
        {
            _InventroryManager = GetComponentInChildren<InventoryManager>();
        }
        if (_GISelected ==null)
        {
            _GISelected = GetComponentInChildren<GISelected>();
        }
    }


    void Start () {
    }
    void Update () {
	}
}
