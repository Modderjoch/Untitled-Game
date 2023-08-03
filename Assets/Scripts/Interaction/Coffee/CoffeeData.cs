using UnityEngine;

[CreateAssetMenu(fileName = "Coffee Data", menuName = "Coffee/Coffee Data")]
public class CoffeeData : ScriptableObject
{
    [Header("General Data")]
    public string coffeeName;
    public string description;
    public Sprite image;
    public float weight;
    public int pricePerKG;

    [Header("Raw Data")]
    public int moistureLevel;

    [Header("Roasted Data")]
    public int roastLevel;

    [Header("Grinded Data")]
    public int grindLevel;

    public enum Type
    {
        Raw,
        Roasted,
        Grinded,
        Packaged
    }

    public Type type;
}
