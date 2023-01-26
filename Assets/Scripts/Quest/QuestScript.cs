using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public Quest Quest { get; set; }

    public void Select()
    {
        QuestLog.Instance.ShowDescription(Quest);
    }

    public void Deselect()
    {
        if(this != null)
        {

        }
    }
}
