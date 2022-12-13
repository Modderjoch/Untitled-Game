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
    [SerializeField] private int level = 0;
    [SerializeField] private int targetXP = 100;

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
        currencyText.text = "€ " + currency.ToString();
        XPText.text = XP.ToString() + "/" + targetXP;
        levelText.text = level.ToString();

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
        currencyText.text = "€ " + currency.ToString();

        PopUp(toAddAmount.ToString(), "currency");
    }

    public void AddXP(int toAddAmount)
    {
        XP += toAddAmount;

        LevelUp(Mathf.FloorToInt(XP/targetXP));

        XP -= level * targetXP;
        XPText.text = XP.ToString() + "/" + targetXP;

        RefreshXP();
        PopUp(toAddAmount.ToString(), "exp");
    }

    private void RefreshXP()
    {
        float maxXP = targetXP;

        XPBar.fillAmount = XP / maxXP;
    }

    public void PopUp(string toAddAmount, string type)
    {
        switch (type)
        {
            case "currency":
                coinText.text = "+€" + toAddAmount;
                StartCoroutine(DisablePopup(sec, "currency"));
                break;
            case "exp":
                expText.text = "+" + toAddAmount + "XP";
                StartCoroutine(DisablePopup(sec, "exp"));
                break;
            case "collect":
                collectedText.text = toAddAmount;
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
            case "currency":
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

        //if(type == "currency")
        //{
        //    coinPanel.gameObject.SetActive(true);
        //    Debug.Log("currency popup");
        //    yield return new WaitForSeconds(seconds);
        //    coinPanel.gameObject.SetActive(false);
        //}
        //else if(type == "exp")
        //{
        //    expPanel.gameObject.SetActive(true);
        //    yield return new WaitForSeconds(seconds);
        //    expPanel.gameObject.SetActive(false);
        //}
        //else if(type == "collect")
        //{
        //    collectedPanel.gameObject.SetActive(true);
        //    yield return new WaitForSeconds(seconds);
        //    collectedPanel.gameObject.SetActive(false);
        //}
        //else
        //{
        //    Debug.Log("Nothing found")
        //}
    }
}
