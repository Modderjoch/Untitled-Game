using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseGoal : Quest.QuestGoal
{
    public string Purchase;

    public override string GetDescription()
    {
        return $"Purchase a {Purchase}";
    }

    private void OnPurchasing(PurchaseEvent eventInfo)
    {
        if(eventInfo.ItemName == Purchase)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
