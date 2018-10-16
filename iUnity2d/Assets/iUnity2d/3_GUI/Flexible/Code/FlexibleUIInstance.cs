using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FlexibleUIInstance : Editor {

    [MenuItem("GameObject/Flexible UI/Button",priority =0)]
    public static void Flexible_Button()
    {
        Create("FlexibleUI/FlexibleButton","Button");
    }

    static GameObject clickedObject;

    private static GameObject Create(string path,string objectName) //objectName is now not use
    {
        GameObject instance = Instantiate(Resources.Load<GameObject>(path));
        //instance.name = objectName;
        clickedObject = UnityEditor.Selection.activeObject as GameObject;
        if (clickedObject != null)
        {
            instance.transform.SetParent(clickedObject.transform, false);
        }
        return instance;
    }

}
