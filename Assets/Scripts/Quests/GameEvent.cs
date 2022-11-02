using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public string EventDescription;
}

public class PurchaseEvent : GameEvent
{
    public string ItemName;

    public PurchaseEvent(string name)
    {
        ItemName = name;
    }
}
