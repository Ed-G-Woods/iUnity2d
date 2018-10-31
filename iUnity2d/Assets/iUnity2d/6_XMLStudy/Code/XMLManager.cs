using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour {

    public ItemDatabase ItemDB;

    private void Awake()
    {
        
    }

    public void saveXML()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/iUnity2d/6_XMLStudy/XMLFolder/ItemDB.xml", FileMode.Create);
        serializer.Serialize(stream, ItemDB);
        stream.Close();
    }
    public void loadXML()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/iUnity2d/6_XMLStudy/XMLFolder/ItemDB.xml", FileMode.Open);
        ItemDB = serializer.Deserialize(stream) as ItemDatabase;
        stream.Close();
    }
}

[System.Serializable]
public class ItemDatabase
{
    public List<ItemEntry> list = new List<ItemEntry>();
    public int aNumber;
}

[System.Serializable]
public class ItemEntry
{
    public string name;
    public int x;
    public int y;
    public Level level;
    /*public UnityEngine.UI.Image image;*/
}

public enum Level
{
    Laji,
    normal,
    rare,
    legend
}