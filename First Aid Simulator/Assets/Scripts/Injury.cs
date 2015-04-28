using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class Injury
{
    public List<Item> treatment;
    public List<Item> appliedTreatment;
    public string Name;
    public int Id;
    public Sprite visual;
    public Slot slot;
    public string key;

    public Injury()
    {
        treatment = new List<Item>();
        appliedTreatment = new List<Item>();
    }

    public Injury(Injury i)
    {
        treatment = i.treatment;
        appliedTreatment = i.appliedTreatment;
        Name = i.Name;
        Id = i.Id;
        visual = i.visual;
        slot = i.slot;
        key = i.key;
    }

    public Injury(XmlNode node)
    {
        treatment = new List<Item>();
        appliedTreatment = new List<Item>();
        Name = node.Attributes["name"].Value;
        if (Manager.items == null)
        {
            Debug.LogError("Items is null!");
        }
        else
        {
            treatment = new List<Item>();
            foreach (XmlNode childNode in node.ChildNodes)
            {
                string treatmentName = childNode.Attributes["name"].Value;
                if (Manager.items.ContainsKey(treatmentName))
                {
                    treatment.Add(Manager.items[treatmentName]);
                }
                else
                {
                    Debug.LogError("Items does not contain " + treatmentName);
                }
            }
        }
        visual = Resources.Load<Sprite>("Images/" + node.Attributes["visual"].Value);
    }

    public void Reset()
    {
        appliedTreatment.Clear();
    }

}
