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
    GameObject select;

    Injury activeInjury;
    GameObject items;
    GameObject human;
    GameObject report;
    string lastKey;
    string appliedProcedureText = "Procedure";
    Text appliedProcedure;
    Text currentInjury;
    Text reportApplied;
    Text reportCorrect;

    void CreateItems()
    {
        treatments = new Dictionary<string, Item>();
        int posy = 0;
        foreach (Item i in Manager.items.Values)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Item"), Vector3.zero, Quaternion.identity) as GameObject;
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
        int index = 0;
        if (activeInjuryDict == null)
            activeInjuryDict = new Dictionary<string, Injury>();
        else
            activeInjuryDict.Clear();

        foreach (Slot s in Manager.slots.Values)
        {
            int j = Random.Range(0, s.possibleInjuries.Count);
            Debug.Log(j + " / " + (s.possibleInjuries.Count));
            Injury i = new Injury(s.possibleInjuries[j]);
            i.slot = s;
            activeInjuryDict.Add(System.Convert.ToChar(index + 110).ToString(), i);

            index++;
        }
        RefreshInjuries();
        report.SetActive(false);
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
            {
                continue;
            }
            GameObject go = Instantiate(Resources.Load("Prefabs/Injury"), Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.SetParent(human.transform);
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.localPosition = keyVal.Value.slot.pos;
            rt.transform.rotation = Quaternion.Euler(keyVal.Value.slot.rot);
            Text text = go.GetComponentInChildren<Text>();
            text.text = keyVal.Key + " - " + keyVal.Value.Name;
            go.GetComponent<Image>().sprite = keyVal.Value.visual;
            buttons.Add(go);
            posy++;
        }
        if(activeInjuryDict.Count == 0)
        {
            GenerateReport();
            select.transform.position = new Vector3(10000, 0, 0);
            report.SetActive(true);
        }
    }


    void Start()
    {
        Manager.ReadXML("Info");
        buttons = new List<GameObject>();
        fullReport = new List<Report>();
        items = GameObject.FindGameObjectWithTag("Items");
        human = GameObject.FindGameObjectWithTag("Human");
        select = GameObject.FindGameObjectWithTag("Select");
        report = GameObject.FindGameObjectWithTag("Report");
        currentInjury = GameObject.FindGameObjectWithTag("CurrentInjury").GetComponent<Text>();
        appliedProcedure = GameObject.FindGameObjectWithTag("AppliedProcedure").GetComponent<Text>();
        reportApplied = GameObject.FindGameObjectWithTag("ReportApplied").GetComponent<Text>();
        reportCorrect = GameObject.FindGameObjectWithTag("ReportCorrect").GetComponent<Text>();
        CreateItems();
        GenerateInjuries();
    }

    void UpdateProcedure()
    {
        appliedProcedureText = "";
        foreach(Item i in activeInjury.appliedTreatment)
        {
            appliedProcedureText += i.Name + "\n";
        }
        appliedProcedure.text = appliedProcedureText;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            if (treatments.ContainsKey(Input.inputString) && activeInjury != null)
            {
                activeInjury.appliedTreatment.Add(treatments[Input.inputString]);
                Debug.Log("Treatment used: " + treatments[Input.inputString].Name);
                UpdateProcedure();
            }
            else if (activeInjuryDict.ContainsKey(Input.inputString))
            {
                activeInjury = activeInjuryDict[Input.inputString];
                lastKey = Input.inputString;
                select.transform.position = activeInjury.slot.pos + new Vector2(human.transform.position.x, human.transform.position.y);
                currentInjury.text = activeInjury.Name;
                Debug.Log("Active injury is set to " + activeInjury.Name);
                UpdateProcedure();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && activeInjury != null && activeInjuryDict.Count > 0)
        {
            fullReport.Add(new Report(activeInjury));
            activeInjury.Reset();
            activeInjuryDict.Remove(lastKey);
            RefreshInjuries();
        }
        else if(Input.GetKeyDown(KeyCode.Return) && activeInjuryDict.Count == 0)
        {
            GenerateInjuries();
        }
    }

    void GenerateReport()
    {
        string correct = "";
        string applied = "";
        foreach(Report r in fullReport)
        {
            correct += "\n" + r.injury.Name + "--\n\n";
            foreach(Item i in r.injury.treatment)
            {
                correct += i.Name + " \n";
            }

            applied += "\n--" + r.injury.Name + "\n\n";
            applied += r.detailedReport;
        }

        reportApplied.text = applied;
        reportCorrect.text = correct;
    }
}
