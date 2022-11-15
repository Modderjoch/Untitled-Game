using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField] private string title;
    [SerializeField] private string sender;
    [SerializeField] private string date;

    [TextArea(10, 10)]
    [SerializeField] private string description;

    [SerializeField] private List <ProduceObjective> produceObjectives;

    public QuestScript QuestScript { get; set; }

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Sender
    {
        get { return sender; }
        set { sender = value; }
    }

    public string Date
    {
        get { return date; }
        set { date = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public List<ProduceObjective> ProduceObjectives
    {
        get { return produceObjectives; }
    }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField] private int amountRequired;
    [HideInInspector] public int currentAmount;
    [SerializeField] private string type;
    [SerializeField] public string[] rewardType;
    [SerializeField] public int[] rewardAmount;

    private Quest quest;

    public int AmountRequired
    {
        get { return amountRequired; }
    }

    public int CurrentAmount
    {
        get { return currentAmount; }
        set { currentAmount = value; }
    }

    public string Type
    {
        get { return type; }
    }

    public string[] RewardType
    {
        get { return rewardType; }
    }

    public int[] RewardAmount
    {
        get { return rewardAmount; }
    }

    public void UpdateAmount(int toAddAmount)
    {
        quest = QuestLog.Instance.ReturnQuest();
        currentAmount += toAddAmount;
        
        string receivedType = CheckCompletion();

        string collect = ("collected " + toAddAmount + " kilos of " + Type);
        PlayerProgression.Instance.CollectedPopup(collect);

        if (receivedType != null )
        {
            for (int i = 0; i < quest.ProduceObjectives.Count; i++)
            {
                if(receivedType == quest.ProduceObjectives[i].Type)
                {
                    PlayerProgression.Instance.CheckType(quest.ProduceObjectives[i].RewardType, quest.ProduceObjectives[i].RewardAmount);
                    quest.ProduceObjectives.Remove(quest.ProduceObjectives[i]);
                    QuestLog.Instance.DeleteEmptyObjective();
                    break;
                }
            }
        }
    }

    public string CheckCompletion()
    {
        if(currentAmount >= amountRequired)
        {
            return Type;
        }
        else
        {
            return null;
        }
    }
}

[System.Serializable]
public class ProduceObjective : Objective
{
    
}
