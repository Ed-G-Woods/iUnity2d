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

    void Start () {
	}
	void Update () {
	}
}
