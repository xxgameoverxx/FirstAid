using UnityEngine;
using System.Xml;
using System.Collections;

public class Item
{
    public Sprite visual;
    public string Name;

    public Item()
    {}

    public Item(XmlNode node)
    {
        Name = node.Attributes["name"].Value;
        Debug.Log("Item " + Name + " is created");
    }
}
