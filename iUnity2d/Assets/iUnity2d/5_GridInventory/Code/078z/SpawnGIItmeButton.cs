using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SpawnGIItmeButton : MonoBehaviour,IPointerClickHandler
{
    

    [SerializeField] private Items item = Items.SmallPotion;
    [SerializeField] private GameObject GIItemPerfab;

    private GIManager _GIManager;
    private UnityEngine.UI.Image _image;
    private string itemName;

    private void Awake()
    {
        _image = GetComponent<UnityEngine.UI.Image>();

        
    }
    void Start () {
        itemName = item.ToString();
        _GIManager = GIManager.GgetGIManager;
    }
	void Update () {
	}

    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        SpawnGIItem();
    }

    void SpawnGIItem()
    {
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
        ItemDatabase idb = XMLManager.getXMLManager.ItemDB;

        foreach (ItemEntry i in idb.itemDataList)
        {
            if (i.name == itemName)
            {
                _item.ItemName = i.name;
                _item.ItemSizeX = i.ItemSizeX;
                _item.ItemSizeY = i.ItemSizeY;
                _item.setItemImage(_image.sprite);
                return;
            }
        }
    }
}

public enum Items
{
    SmallPotion,
    BigPotion,
    GreatSword,
    Dagger
}
