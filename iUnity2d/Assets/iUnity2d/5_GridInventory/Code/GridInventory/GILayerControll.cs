using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//在GIlayer下,用来控制GI perfab的生成..删除..etc..
public class GILayerControll : MonoBehaviour {

    public GameObject PPlayerInventory;
    public GameObject PShop;

    public void CleanAllGI()
    {
        //         Transform[] GIs = GetComponentsInChildren<Transform>();
        //         foreach(Transform gi in GIs)
        //         {
        //             GameObject.Destroy(gi.gameObject);
        //         }
        foreach (Transform g in transform)
        {
            GameObject.Destroy(g.gameObject);
        }
    }

    public void CreatePlayerInventory()
    {
        GameObject  playerinventory=Instantiate<GameObject>(PPlayerInventory);
        playerinventory.transform.SetParent(transform,false);
        //playerinventory.transform.localScale = Vector3.one;
    }

    public void CreateShop()
    {
        GameObject shop = Instantiate<GameObject>(PShop);
        shop.transform.SetParent(transform, false);
    }


}
