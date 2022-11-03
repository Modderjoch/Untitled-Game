using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> quests;

    //Debugging only
    [SerializeField] private QuestLog tmpLog;

    private static QuestGiver instance;

    public static QuestGiver Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestGiver>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        //Here we need to accept a quest;
        tmpLog.AcceptQuest(quests[0]);
        tmpLog.AcceptQuest(quests[1]);
        tmpLog.AcceptQuest(quests[2]);
    }
}
