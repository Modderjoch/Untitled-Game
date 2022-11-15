using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    [SerializeField] private int currency;
    [SerializeField] private int XP;

    [SerializeField] private float sec = 2f;

    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI XPText;
    [SerializeField] private TextMeshProUGUI collectedText;
    [SerializeField] private GameObject collectedPanel;

    private static PlayerProgression instance;
    public static PlayerProgression Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerProgression>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        currencyText.text = currency.ToString();
        XPText.text = XP.ToString();
    }

    public void CheckType(string[] type, int[] toAddAmount)
    {
        for(int i = 0; i < type.Length; i++)
        {
            if (type[i] == "XP")
            {
                AddXP(toAddAmount[i]);
            }
            else if(type[i] == "Currency")
            {
                AddCurrency(toAddAmount[i]);
            }
            else
            {
                Debug.Log(type[i] + "was not recognized");
            }
        }
    }

    public void AddCurrency(int toAddAmount)
    {
        currency += toAddAmount;
        currencyText.text = currency.ToString();
    }

    public void AddXP(int toAddAmount)
    {
        XP += toAddAmount;
        XPText.text = XP.ToString();
    }

    public void CollectedPopup(string text)
    {
        collectedText.text = text;

        StartCoroutine(DisablePopup(sec));
    }

    IEnumerator DisablePopup(float seconds)
    {
        collectedPanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(seconds);

        collectedPanel.gameObject.SetActive(false);
    }
}
