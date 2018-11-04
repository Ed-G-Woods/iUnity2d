using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLayerContrl : MonoBehaviour {

    public GameObject[] ObjectsHidden;
    
    public void HidOjcets(bool hid)
    {
        foreach (GameObject o in ObjectsHidden)
        {
            o.SetActive(!hid);
        }
    }
    
}
