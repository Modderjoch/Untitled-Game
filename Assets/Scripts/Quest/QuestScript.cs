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
        GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
        QuestLog.Instance.ShowDescription(Quest);
    }

    public void Deselect()
    {
        if(this != null)
        {
            GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        }
    }
}
