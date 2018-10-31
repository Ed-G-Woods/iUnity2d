using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour {
    public static XMLManager getXMLManager = null;

    public ItemDatabase ItemDB;

    private void Awake()
    {
        getXMLManager = this;
    }
    private void Start()
    {
        LoadXML();
        //SaveXML();
    }

    public void SaveXML()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/iUnity2d/6_XMLStudy/XMLFolder/ItemDB.xml", FileMode.Create);
        serializer.Serialize(stream, ItemDB);
        stream.Close();
    }
    public void LoadXML()
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
    /*public Dictionary<string, ItemEntry> ItemDirectory = new Dictionary<string, ItemEntry>();*/
    public List<ItemEntry> itemDataList = new List<ItemEntry>();
}

[System.Serializable]
public class ItemEntry
{
    public string name;
    public short ItemSizeX;
    public short ItemSizeY;
    //public Level level;
    /*public UnityEngine.UI.Image image;*/
}

public enum Level
{
    Laji,
    normal,
    rare,
    legend
}