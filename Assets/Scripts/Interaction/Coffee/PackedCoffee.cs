using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PackedCoffee : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;
    public string _name;

    public float amount;
    public string type;
    [TextArea(2, 10)]
    [SerializeField] private string description;

    private QuestLog questLog;
    private Quest quest;

    [SerializeField] private TextMeshProUGUI labelTitle;
    [SerializeField] private TextMeshProUGUI labelDescription; 
    [SerializeField] private TextMeshProUGUI labelWeight;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public string InteractionName => _name;

    private void Awake()
    {
        questLog = QuestLog.Instance;

        labelTitle.text = _name;
        labelWeight.text = amount.ToString() + " kilos";
        labelDescription.text = description;
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        quest = questLog.ReturnQuest();

        Debug.Log(quest);

        foreach (Objective obj in quest.ProduceObjectives)
        {
            if(obj != null)
            if(obj.Type.ToLower() == _name.ToLower())
            {
                obj.UpdateAmount(amount);
                questLog.ShowDescription(quest);
                return true;
            }
        }
        return true;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        return true;
    }
}
