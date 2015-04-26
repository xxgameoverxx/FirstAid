using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;


public static class XmlReader
{

    public static Dictionary<string, Injury> injuries;
    public static List<Slot> slots;
    public static Dictionary<string, Item> items;

    public static void ReadXML(string name)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath + "/Resources/Xml/" + name + ".xml");

        foreach (XmlNode node in xmlDoc.ChildNodes)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name == "Injuries")
                {
                    ReadInjuries(childNode);
                }
                else if(childNode.Name == "Slots")
                {
                    ReadSlots(childNode);
                }
                else if(childNode.Name == "Treatments")
                {
                    ReadItems(childNode);
                }
            }
        }
    }

    private static void ReadInjuries(XmlNode node)
    {
        injuries = new Dictionary<string, Injury>();
        foreach (XmlNode childNode in node.ChildNodes)
        {
            injuries.Add(childNode.Attributes["name"].Value, new Injury(childNode));
        }
    }

    private static void ReadSlots(XmlNode node)
    {
        slots = new List<Slot>();
        foreach (XmlNode childNode in node.ChildNodes)
        {
            slots.Add(new Slot(childNode));
        }
    }

    private static void ReadItems(XmlNode node)
    {
        items = new Dictionary<string,Item>();
        foreach(XmlNode childNode in node.ChildNodes)
        {
            items.Add(childNode.Attributes["name"].Value, new Item(childNode));
        }
    }

    public static Vector2 StringToVector2(string s)
    {
        return new Vector2(float.Parse(s.Split(","[0])[0]), float.Parse(s.Split(","[0])[1]));
    }
}
