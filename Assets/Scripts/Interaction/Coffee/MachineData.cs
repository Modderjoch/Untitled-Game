using UnityEngine;

[CreateAssetMenu(fileName = "Machine Data", menuName = "Coffee/Machine Data")]
public class MachineData : ScriptableObject
{
    [Header("General Data")]
    public string machineName;
    public string description;
    public Sprite image;
    public int unitPrice;
    [Range(0f, 1f)]public float weightLoss;

    [Header("Raw Data")]
    public int moistureRange;

    [Header("Roasted Data")]
    public int roastRange;

    [Header("Grinded Data")]
    public int grindRange;

    public enum Type
    {
        Roaster,
        Grinder
    }

    public Type type;
}
