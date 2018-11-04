using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SpawnGIItmeButton : MonoBehaviour, IPointerClickHandler
{


    public Items item = Items.SmallPotion;
    [SerializeField] private GameObject GIItemPerfab;
    
    private UnityEngine.UI.Image _image;
    private string itemName;

    private void Awake()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
    }
    void Start () {
        itemName = item.ToString();
    }

    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        SpawnGIItem();
    }

    void SpawnGIItem()
    {

        GIManager _GIManager = GIManager.GgetGIManager;
        if (_GIManager == null) return;

        if (_GIManager._GISelected.isHaveSelected) return;

        GameObject item = Instantiate<GameObject>(GIItemPerfab);

        item.transform.SetParent(_GIManager._GISelected.transform);
        item.transform.localScale = Vector3.one;

        GIItem itemGIItem = item.GetComponent<GIItem>();
        setItemData(itemGIItem);
        itemGIItem.StartMove();
    }

    private void setItemData(GIItem _item)
    {
        ItemDatabase idb = XMLManager.GgetXMLManager.ItemDB;

        foreach (ItemEntry i in idb.itemDataList)
        {
            if (i.name == itemName)
            {
                _item.gameObject.name = i.name;
                _item.ItemName = i.name;
                _item.ItemSizeX = i.ItemSizeX;
                _item.ItemSizeY = i.ItemSizeY;
                _item.setItemImage(_image.sprite);
                return;
            }
        }
    }
}
