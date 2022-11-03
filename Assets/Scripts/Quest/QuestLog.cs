using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questList;

    private Quest selected;

    [SerializeField] private TextMeshProUGUI questDescription;

    private static QuestLog instance;

    public static QuestLog Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }

            return instance;
        }
    }

    public void AcceptQuest(Quest quest)
    {
        GameObject go = Instantiate(questPrefab, questList);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.QuestScript = qs;
        qs.Quest = quest;

        go.transform.Find("Sender").GetComponent<TextMeshProUGUI>().text = quest.Sender;
        go.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = quest.Title;
        go.transform.Find("Date").GetComponent<TextMeshProUGUI>().text = quest.Date;
    }

    public void ShowDescription(Quest quest)
    {
        if (selected != null && selected.QuestScript != null)
        {
            selected.QuestScript.Deselect();
        }

        string objectives = "\nObjectives\n";
        string rewards = "\nRewards\n";

        selected = quest;

        string title = quest.Title;

        foreach(Objective obj in quest.ProduceObjectives)
        {
            objectives += obj.Type + ": " + obj.CurrentAmount + "/" + (obj.AmountRequired) + "\n";

            for(int i = 0; i < obj.rewardType.Length; i++)
            {
                rewards += obj.rewardType[i] + ": " + obj.rewardAmount[i] + "\n";
            }
        }
        questDescription.text = string.Format("<size=22>{0}</size>\n\n{1}\n{2}\n{3}", title, quest.Description, objectives, rewards);
    }

    public void DeleteEmptyObjective()
    {
        for(int i = 0; i < QuestGiver.Instance.quests.Count; i++)
        {
            GameObject go = GetComponentsInChildren<QuestScript>()[i].gameObject;
            string foundSender = go.transform.Find("Sender").GetComponent<TextMeshProUGUI>().text;

            if (QuestGiver.Instance.quests[i].ProduceObjectives.Count == 0 && foundSender == QuestGiver.Instance.quests[i].Sender)
            {
                QuestGiver.Instance.quests.RemoveAt(i);
                questDescription.text = "";
                go.GetComponent<QuestScript>().Deselect();
                Destroy(go); 
            }
            else
            {
                Debug.Log("Sender not found");
            }
        }
    }

    public Quest ReturnQuest()
    {
        return selected;
    }
}
