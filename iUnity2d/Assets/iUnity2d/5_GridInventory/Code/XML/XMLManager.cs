using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour {
    public static XMLManager GgetXMLManager = null;

    public ItemDatabase ItemDB;

    private void Awake()
    {
        GgetXMLManager = this;
    }
    private void Start()
    {
        LoadXML();
        //SaveXML();
    }


    public void TCreateXML()
    {
        string path = Application.dataPath + "/iUnity2d/5_GridInventory/Asset/XMLFiles/T.xml";
        if (/*!File.Exists(path)*/true)
        {
            XmlDocument newxml=new XmlDocument();
            XmlElement root = newxml.CreateElement("RootElement");
            XmlElement Element1 = newxml.CreateElement("Element1");
            Element1.SetAttribute("ID", "001");
            XmlElement Element2 = newxml.CreateElement("Element2");
            Element2.SetAttribute("ID", "002");
            Element2.InnerText = "A Text dont know what to do";
            XmlElement Element3 = newxml.CreateElement("Element3");
            Element3.SetAttribute("ID", "003");
            Element3.InnerText = "Another text dont know what to do";

            Element1.AppendChild(Element2);
            Element1.AppendChild(Element3);
            root.AppendChild(Element1);
            newxml.AppendChild(root);

            newxml.Save(path);
        }
    }
    public void TLoadXml()
    {
        XmlDocument myxml = new XmlDocument();
        myxml.Load(Application.dataPath + "/iUnity2d/5_GridInventory/Asset/XMLFiles/T.xml");
        XmlNodeList xmlNodeList = myxml.SelectSingleNode("RootElement").ChildNodes;
        foreach(XmlElement a in xmlNodeList)
        {
            if (a.HasChildNodes)
            {
                foreach(XmlElement b in a.ChildNodes)
                {
                    Debug.Log(b.GetAttribute("ID") + " : "+b.InnerText);
                }
            }
        }
    }
    public void TUpdateXML()
    {
        string path = Application.dataPath + "/iUnity2d/5_GridInventory/Asset/XMLFiles/T.xml";
        if (File.Exists(path))
        {
            XmlDocument myxml = new XmlDocument();
            myxml.Load(path);
            XmlNodeList xmlNodeList = myxml.SelectSingleNode("RootElement").ChildNodes;
            foreach (XmlElement a in xmlNodeList)
            {
                if (a.GetAttribute("ID")=="001")
                {
                    a.SetAttribute("ID", "100");
                    foreach(XmlElement b in a.ChildNodes)
                    {
                        if (b.GetAttribute("ID")=="002")
                        {
                            b.SetAttribute("ID", "110");
                        }
                        if (b.GetAttribute("ID") == "003")
                        {
                            b.SetAttribute("ID", "120");
                            b.InnerText = "Ooo,I'm making game!!";
                        }
                    }
                }

            }

            myxml.Save(path);
        }
    }
    public void TAddXml()
    {
        string path = Application.dataPath + "/iUnity2d/5_GridInventory/Asset/XMLFiles/T.xml";
        if (File.Exists(path))
        {
            XmlDocument myxml = new XmlDocument();
            myxml.Load(path);
            XmlNode r = myxml.SelectSingleNode("RootElement");
            XmlElement newElement = myxml.CreateElement("NewElement");
            newElement.SetAttribute("Name", "NewnewnewE");
            newElement.InnerText = "This is a new element";

            r.AppendChild(newElement);
            myxml.AppendChild(r);

            myxml.Save(path);
        }
    }


    public void SaveXML_DictionaryTest()
    {
//         Dictionary<string, int> dic = new Dictionary<string, int>();
//         dic.Add("abc", 123);
//         dic.Add("bcd", 234);
        List<KeyValuePair<string, int>> kvlist = new List<KeyValuePair<string, int>>();
        kvlist.Add(new KeyValuePair<string, int>("aaa", 111));
        kvlist.Add(new KeyValuePair<string, int>("bbb", 222));

        XmlSerializerNamespaces cleanNamespace = new XmlSerializerNamespaces();
        cleanNamespace.Add("","");

        XmlSerializer serializer = new XmlSerializer(kvlist.GetType());
        FileStream stream = new FileStream(Application.dataPath + "/iUnity2d/5_GridInventory/Asset/XMLFiles/DictionaryTest.xml", FileMode.Create);
        serializer.Serialize(stream, kvlist, cleanNamespace);
        stream.Close();
        
    }


    /// <summary>
    /// 
    /// </summary>


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



/// <summary>
/// 
/// </summary>


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

public enum Items
{
    SmallPotion,
    BigPotion,
    GreatSword,
    Dagger
}
