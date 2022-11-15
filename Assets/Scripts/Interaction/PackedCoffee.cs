using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PackedCoffee : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _name;

    [SerializeField] private int amount;
    [SerializeField] private string type;
    [TextArea(2, 10)]
    [SerializeField] private string description;

    private QuestLog questLog;
    private Quest quest;

    [SerializeField] private TextMeshProUGUI labelTitle;
    [SerializeField] private TextMeshProUGUI labelDescription; 
    [SerializeField] private TextMeshProUGUI labelWeight;

    public string InteractionPrompt => _prompt;
    public string InteractionName => _name;

    private void Awake()
    {
        questLog = QuestLog.Instance;

        labelTitle.text = type;
        labelWeight.text = amount.ToString() + " kilos";
        labelDescription.text = description;
    }

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
