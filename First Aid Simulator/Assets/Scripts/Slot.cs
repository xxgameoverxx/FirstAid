using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class Slot
{
    public string Name;
    public Vector2 pos;
    public Vector3 rot;
    public List<Injury> possibleInjuries;

    public Slot()
    { }

    public Slot(XmlNode node)
    {
        Name = node.Attributes["name"].Value;
        pos = Manager.StringToVector2(node.Attributes["pos"].Value);
        rot = Manager.StringToVector3(node.Attributes["rot"].Value);
        if (Manager.injuries == null)
        {
            Debug.LogError("injuries list is empty! Check xml file for 'Injury' nodes!");
        }
        else
        {
            possibleInjuries = new List<Injury>();
            foreach (XmlNode childNode in node.ChildNodes)
            {
                string injuryName = childNode.Attributes["name"].Value;
                if (Manager.injuries.ContainsKey(injuryName))
                    possibleInjuries.Add(Manager.injuries[injuryName]);
                else
                    Debug.LogError("No such injury: " + injuryName + ". Injuries dictionary may be empty.");
            }
        }

        Debug.Log("Slot " + Name + " is created at " + pos + " with " + possibleInjuries.Count + " possible injuries.");
    }
}
