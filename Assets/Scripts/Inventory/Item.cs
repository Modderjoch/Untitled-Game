using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : MonoBehaviour
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;

    public Dictionary<string, int> data = new Dictionary<string, int>();

    public Item(int id, string title, string description, Dictionary<string, int> data)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Materials/Textures/Sprites/" + title);
        this.data = data;
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = Resources.Load<Sprite>("Materials/Textures/Sprites/" + item.title);
        this.data = item.data;
    }
}
