using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public bool isHaveSelect =false;
    public GIManager _GIManager;

    private void Awake()
    {
        if (_GIManager == null)
        {
            _GIManager = GetComponentInParent<GIManager>();
        }
    }

}
