using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BotSlotView : MonoBehaviour
{
    public TextMeshPro title;
    public TextMeshPro cost;
    public float spendCount = 10;
    public GameObject buy;

    public BotResource BotResource
    {
        get
        {
            return GameResource.Instance.BotResource(botData.type);
        }
    }

    protected GameObject Prefab
    {
        get
        {
            return BotResource.model;
        }
    }

    public BotData botData;
    protected SecuredDouble Cost
    {
        get
        {
            return botData.cost.Round();
        }
    }

    protected SecuredDouble spendValue;

    public void Initialize(BotData botData)
    {
        this.botData = botData;
        spendValue = botData.cost / spendCount;
        if (spendValue < 1)
        {
            spendValue = 1;
        }

        GenBot();
        UpdateInfo();
    }

    protected void GenBot()
    {
        if (!botData.Unlocked)
        {
            return;
        }

        GameObject g = Instantiate(Prefab);
        g.transform.position = transform.position;
        g.transform.localEulerAngles = Vector3.zero;
    }

    public virtual void UpdateInfo()
    {
        buy.SetActive(!botData.Unlocked);
        cost.text = Cost.FormattedString;

        title.text = "EMPLOYEE";
    }

    public virtual void SpendMoney()
    {
        if (spendValue <= Cost)
        {
            if (Game.Instance.gameData.SpentCoin(spendValue))
            {
                botData.SpendCost(spendValue);
            }
            else
            {

                SecuredDouble money = Game.Instance.gameData.money;
                if (spendValue > money)
                {
                    if (Game.Instance.gameData.SpentCoin(money))
                    {
                        botData.SpendCost(money);
                    }
                }
            }
        }
        else
        {
            if (Game.Instance.gameData.SpentCoin(Cost))
            {
                botData.SpendCost(Cost);
            }
        }

        if (botData.cost.Round() <= 0)
        {
            Upgrade();
        }

        UpdateInfo();
    }

    public virtual void Upgrade()
    {
        botData.Upgrade();
        GenBot();

        MoneyEffect moneyEffect = buy.GetComponent<MoneyEffect>();
        if (moneyEffect != null)
        {
            moneyEffect.StopSpend();
        }

        SoundManager.Instance.PlayUpgrade(transform.position);
    }

}
