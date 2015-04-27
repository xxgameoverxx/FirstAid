using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InGameManager : MonoBehaviour
{

    Dictionary<string, Injury> activeInjuryDict;
    Dictionary<string, Item> treatments;
    Dictionary<Slot, bool> emptySlots;
    List<Report> fullReport;
    List<GameObject> buttons;

    Injury activeInjury;
    GameObject items;
    GameObject injuries;
    string lastKey;

    void CreateItems()
    {
        treatments = new Dictionary<string, Item>();
        int posy = 0;
        foreach (Item i in Manager.items.Values)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Button"), Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.SetParent(items.transform);
            go.GetComponent<RectTransform>().localPosition = new Vector2(0, -40 * posy + 180);
            Text text = go.GetComponentInChildren<Text>();
            treatments.Add(System.Convert.ToChar(posy + 97).ToString(), i);
            text.text = System.Convert.ToChar(posy + 97).ToString() + " - " + i.Name;
            posy++;
        }
    }

    void GenerateInjuries()
    {
        activeInjuryDict = new Dictionary<string, Injury>();
        int posy = 0;
        foreach (Injury i in Manager.injuries.Values)
        {
            activeInjuryDict.Add(System.Convert.ToChar(posy + 110).ToString(), i);
            posy++;
        }
        RefreshInjuries();
    }

    void RefreshInjuries()
    {
        int posy = 0;
        for (int i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i]);
        }
        buttons.Clear();

        foreach (KeyValuePair<string, Injury> keyVal in activeInjuryDict)
        {
            if (keyVal.Value == null)
                continue;
            GameObject go = Instantiate(Resources.Load("Prefabs/Button"), Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.SetParent(injuries.transform);
            go.GetComponent<RectTransform>().localPosition = new Vector2(0, -40 * posy + 180);
            Text text = go.GetComponentInChildren<Text>();
            text.text = keyVal.Key + " - " + keyVal.Value.Name;
            buttons.Add(go);
            posy++;
        }
    }


    void Start()
    {
        Manager.ReadXML("Info");
        buttons = new List<GameObject>();
        items = GameObject.FindGameObjectWithTag("Items");
        injuries = GameObject.FindGameObjectWithTag("Injuries");
        CreateItems();
        GenerateInjuries();
    }

    void Update()
    {
        if (Input.anyKey)
        {
            if (treatments.ContainsKey(Input.inputString) && activeInjury != null)
            {
                activeInjury.appliedTreatment.Add(treatments[Input.inputString]);
                Debug.Log("Treatment used: " + treatments[Input.inputString].Name);
            }
            else if (activeInjuryDict.ContainsKey(Input.inputString))
            {
                activeInjury = activeInjuryDict[Input.inputString];
                lastKey = Input.inputString;
                Debug.Log("Active injury is set to " + activeInjury.Name);
            }
        }
        if (Input.GetKey(KeyCode.Return) && activeInjury != null)
        {
            Report rep = new Report(activeInjury);
            activeInjuryDict.Remove(lastKey);
            RefreshInjuries();
        }

    }
}
