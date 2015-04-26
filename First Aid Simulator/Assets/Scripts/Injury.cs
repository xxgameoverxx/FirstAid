using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class Injury
{
    public List<Item> treatment;
    public List<Item> appliedTreatment;
    public int Id;
    Sprite visual;
    Slot pos;
    string Name;

    public Injury()
    { }

    public Injury(XmlNode node)
    {
        Name = node.Attributes["name"].Value;
        if (XmlReader.items == null)
        {
            Debug.LogError("Items is null!");
        }
        else
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                string treatmentName = childNode.Attributes["name"].Value;
                if (XmlReader.items.ContainsKey(treatmentName))
                {
                    treatment.Add(XmlReader.items[treatmentName]);
                }
                else
                {
                    Debug.LogError("Items does not contain " + treatmentName);
                }
            }
        }
    }

}
