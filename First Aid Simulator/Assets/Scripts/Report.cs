using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Report
{
    public int point = 0;
    public string detailedReport;
    public Injury injury;


    public Report()
    {
    }

    public Report(Injury _injury)
    {
        this.injury = new Injury(_injury);
        for(int i = 0; i < injury.appliedTreatment.Count; i++)
        {
            if (injury.treatment.Count < i + 1)
            {
                point -= 5;
                Debug.LogError("overtreated!");
                break;
            }
            else if(injury.treatment.Contains(injury.appliedTreatment[i]))
            {
                point += 5;
                Debug.Log("Correct treatment: " + injury.appliedTreatment[i].Name);
                if(injury.appliedTreatment[i] != injury.treatment[i])
                {
                    point -= 3;
                    Debug.LogWarning("Wrong order!" + "(" + injury.appliedTreatment[i].Name + ")");
                }
                else
                {
                    point += 3;
                }
            }
            else
            {
                Debug.LogWarning("Wrong treatment! " + injury.appliedTreatment[i]);
            }
        }

        //detailedReport = "Correct treatmen was: ";
        //foreach(Item i in injury.treatment)
        //{
        //    detailedReport += ", " + i.Name;
        //}
        //detailedReport += "\nApplied treatment was: ";
        foreach(Item i in injury.appliedTreatment)
        {
            detailedReport += " " + i.Name + "\n";
        }
    }
}
