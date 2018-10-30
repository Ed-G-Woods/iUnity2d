using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SpawnGIItmeButton : MonoBehaviour,IPointerClickHandler
{

    [SerializeField] private GameObject GIItemPerfab;
    [SerializeField] private InventoryManager _InventoryManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        SpawnGIItem();
    }

    void SpawnGIItem()
    {
        if (_InventoryManager.isHaveSelect) return;

        GameObject item = Instantiate<GameObject>(GIItemPerfab);

        item.transform.SetParent(_InventoryManager.transform);
        item.transform.localScale = Vector3.one;

        GIItem itemGIItem = item.GetComponent<GIItem>();
        itemGIItem.ItemSizeX = 1;
        itemGIItem.ItemSizeY = 1;
        itemGIItem.ItemName = "potion";

        itemGIItem.StartMove();
    }
}
