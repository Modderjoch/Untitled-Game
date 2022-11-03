using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Block : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _name;

    [SerializeField] private int amount;
    [SerializeField] private string type;

    [SerializeField] QuestLog questLog;
    private Quest quest;

    public string InteractionPrompt => _prompt;
    public string InteractionName => _name;

    public bool Interact(PlayerInteraction playerInteraction)
    {
        quest = questLog.ReturnQuest();

        foreach (Objective obj in quest.ProduceObjectives)
        {
            if(obj.Type.ToLower() == type.ToLower())
            {
                obj.UpdateAmount(amount);
                questLog.ShowDescription(quest);
                return true;
            }
        }
        return true;
    }
}
