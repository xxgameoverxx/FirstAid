using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour
{

    Dictionary<KeyCode, Injury> activeInjuryDict;
    Dictionary<KeyCode, Item> treatments;
    Dictionary<Slot, bool> emptySlots;
    List<Report> fullReport;

    Injury activeInjury;

    Injury GenerateInjury(Slot slot)
    {
        return new Injury();
    }

    // Use this for initialization
    void Start()
    {
        XmlReader.ReadXML("Info");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
