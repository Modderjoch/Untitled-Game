using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questList;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private TextMeshProUGUI questObjectiveReward;
    [SerializeField] private TextMeshProUGUI questInfo;

    private Quest selected;    

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

        string objectives = "<u><b>Objectives</b></u>\n";
        string rewards = "<u><b>Rewards</b></u>\n";

        selected = quest;

        string title = quest.Title;
        string sender = quest.Sender;
        string date = quest.Date;

        foreach(Objective obj in quest.ProduceObjectives)
        {
            objectives += obj.Type + ": " + obj.CurrentAmount + "/" + (obj.AmountRequired) + "\n";

            for(int i = 0; i < obj.rewardType.Length; i++)
            {
                rewards += obj.rewardType[i] + ": " + obj.rewardAmount[i] + "\n";
            }
        }

        questDescription.text = string.Format("\n{0}\n", quest.Description);
        questObjectiveReward.text = string.Format("{0}\n{1}\n", objectives, rewards);
        questInfo.text = string.Format("\n{0}\n{1}\n{2}\n", title, sender, date);
    }

    private void ClearDescription()
    {
        questDescription.text = "";
        questObjectiveReward.text = "";
        questInfo.text = "";

        Debug.Log("Cleared Description" + questDescription.text + " " + questObjectiveReward.text + " " + questInfo);
    }

    public void DeleteEmptyObjective()
    {
        for(int i = 0; i < QuestGiver.Instance.quests.Count; i++)
        {
            GameObject go = questList.gameObject.GetComponentsInChildren<QuestScript>()[i].gameObject;
            string foundSender = go.transform.Find("Sender").GetComponent<TextMeshProUGUI>().text;

            if (QuestGiver.Instance.quests[i].ProduceObjectives.Count == 0 && foundSender == QuestGiver.Instance.quests[i].Sender)
            {
                QuestGiver.Instance.quests.RemoveAt(i);
                go.GetComponent<QuestScript>().Deselect();
                Destroy(go); 
            }
            else
            {
                ClearDescription();
            }
        }
    }

    public Quest ReturnQuest()
    {
        return selected;       
    }
}
