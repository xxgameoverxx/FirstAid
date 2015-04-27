using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Editor : MonoBehaviour
{

    private List<GameObject> itemsButtons;
    private List<GameObject> slotButtons;
    private GameObject items;
    private GameObject injuries;
    private GameObject slots;
    // Use this for initialization
    void Start()
    {
        Manager.ReadXML("Info");
        items = GameObject.FindGameObjectWithTag("Items");
        slots = GameObject.FindGameObjectWithTag("Slots");
        RefreshItems();
        RefreshSlots();
    }

    void RefreshItems()
    {
        if (itemsButtons == null)
            itemsButtons = new List<GameObject>();
        else
        {
            for (int i = 0; i < itemsButtons.Count; i++)
            {
                Destroy(itemsButtons[i]);
            }
            itemsButtons.Clear();
        }

        int posy = 0;
        foreach (Item i in Manager.items.Values)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Button"), Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.SetParent(items.transform);
            go.GetComponent<RectTransform>().localPosition = new Vector2(0, -40 * posy + 180);
            Text text = go.GetComponentInChildren<Text>();
            text.text = i.Name;
            itemsButtons.Add(go);
            posy++;
        }
    }

    void RefreshSlots()
    {
        if (slotButtons == null)
            slotButtons = new List<GameObject>();
        else
        {
            for (int i = 0; i < itemsButtons.Count; i++)
            {
                Destroy(itemsButtons[i]);
            }
            itemsButtons.Clear();
        }

        int posy = 0;
        foreach (Slot i in Manager.slots.Values)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Button"), Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.SetParent(slots.transform);
            go.GetComponent<RectTransform>().localPosition = new Vector2(0, -40 * posy + 180);
            Text text = go.GetComponentInChildren<Text>();
            text.text = i.Name;
            posy++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
