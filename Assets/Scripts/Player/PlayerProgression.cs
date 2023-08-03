using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgression : MonoBehaviour
{
    [Header("Base stats")]
    [SerializeField] private int currency;
    [SerializeField] private int XP;
    [SerializeField] private int level;
    [SerializeField] private int targetXP = 100;
    private int totalXP = 0;

    [Header("Base stats UI")]
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI XPText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image XPBar;

    [Header("Pop-up variables")]
    [SerializeField][Range(0.0f, 5.0f)] private float sec = 1f;
    [SerializeField][Range(0.0f, 5.0f)] private float collectSec = 1f;

    [Header("Pop-up UI")]
    [SerializeField] private TextMeshProUGUI collectedText;
    [SerializeField] private GameObject collectedPanel;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject coinPanel;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private GameObject expPanel;

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
        AddCurrency(0);
        AddXP(0);
        LevelUp(0);
        RefreshXP();
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

    private void LevelUp(int addLevel)
    {
        level += addLevel;
        levelText.text = level.ToString();
    }

    public void AddCurrency(int toAddAmount)
    {
        currency += toAddAmount;

        if (currency >= 1000) { currencyText.text = "€" + (currency / 1000f).ToString(("#.##") + "K").Replace(",", "."); } 
        else { currencyText.text = "€ " + currency.ToString(); }
        
        PopUp(toAddAmount.ToString(), "currency-add");
    }

    public void RemoveCurrency(int toRemoveAmount)
    {
        currency -= toRemoveAmount;

        if (currency >= 1000) { currencyText.text = "€" + (currency / 1000f).ToString(("#.##") + "K").Replace(",", "."); }
        else { currencyText.text = "€ " + currency.ToString(); }

        PopUp(toRemoveAmount.ToString(), "currency-remove");
    }

    public int ReturnCurrency()
    {
        return currency;
    }

    public void AddXP(int toAddAmount)
    {
        totalXP += toAddAmount;
        LevelUp(Mathf.FloorToInt((totalXP/targetXP) - level));
        XP = totalXP - (level * targetXP);

        XPText.text = XP.ToString() + "/" + targetXP;
        RefreshXP();
        PopUp(toAddAmount.ToString(), "exp");
    }

    private void RefreshXP()
    {
        float maxXP = targetXP;

        XPBar.fillAmount = XP / maxXP;
    }

    public void PopUp(string toAddText, string type)
    {
        switch (type)
        {
            case "currency-add":
                coinText.text = "+€" + toAddText;
                StartCoroutine(DisablePopup(sec, "currency-add"));
                break;
            case "currency-remove":
                coinText.text = "-€" + toAddText;
                StartCoroutine(DisablePopup(sec, "currency-remove"));
                break;
            case "exp":
                expText.text = "+" + toAddText + "XP";
                StartCoroutine(DisablePopup(sec, "exp"));
                break;
            case "collect":
                collectedText.text = toAddText;
                StartCoroutine(DisablePopup(collectSec, "collect"));
                break;
            default:
                Debug.Log("Nothing found");
                break;
        }
    }

    IEnumerator DisablePopup(float seconds, string type)
    {
        switch (type)
        {
            case "currency-add":
                coinPanel.gameObject.SetActive(true);
                Debug.Log("currency popup");
                yield return new WaitForSeconds(seconds);
                coinPanel.gameObject.SetActive(false);
                break;
            case "currency-remove":
                coinPanel.gameObject.SetActive(true);
                Debug.Log("currency popup");
                yield return new WaitForSeconds(seconds);
                coinPanel.gameObject.SetActive(false);
                break;
            case "exp":
                expPanel.gameObject.SetActive(true);
                yield return new WaitForSeconds(seconds);
                expPanel.gameObject.SetActive(false);
                break;
            case "collect":
                collectedPanel.gameObject.SetActive(true);
                yield return new WaitForSeconds(seconds);
                collectedPanel.gameObject.SetActive(false);
                break;
            default:
                Debug.Log("Nothing found");
                break;
        }
    }
}
