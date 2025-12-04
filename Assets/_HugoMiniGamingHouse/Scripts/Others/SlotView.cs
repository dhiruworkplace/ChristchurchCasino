using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public abstract class SlotView : MonoBehaviour
{
    public TextMeshPro title;
    public TextMeshPro cost;
    public float spendCount = 10;
    public Transform parent;
    public GameObject upgrade;
    public GameObject levelMax;

    public SlotResource SlotResource
    {
        get
        {
            return GameResource.Instance.SlotResourceWithType(slotData.type);
        }
    }

    protected GameObject Prefab
    {
        get
        {
            return SlotResource.prefab;
        }
    }

    public SlotData slotData;
    protected SecuredDouble Cost
    {
        get
        {
            return slotData.cost.Round();
        }
    }

    protected abstract SecuredDouble CostMax();
    protected SecuredDouble SpendValue
    {
        get
        {
            SecuredDouble r = CostMax() / spendCount;
            return r.Round();
        }
    }

    public abstract bool IsMaxLevel();

    protected SlotBase slotBase;
    public SlotBase SlotBase
    {
        get
        {
            return slotBase;
        }
    }

    public void Initialize(SlotData slotData)
    {
        this.slotData = slotData;
        GenAvatar();
        UpdateInfo();
    }

    protected Vector3 slotBaseScale;
    protected void GenAvatar()
    {
        if (!slotData.Unlocked)
        {
            return;
        }

        GameObject g = Instantiate(Prefab, parent);
        slotBaseScale = g.transform.localScale;

        g.transform.localPosition = Vector3.zero;
        g.transform.localEulerAngles = Vector3.zero;
        slotBase = g.GetComponent<SlotBase>();
        slotBase.Initialize(this);
    }

    public virtual void UpdateInfo()
    {
        if (Cost == 0)
        {
            slotData.cost = CostMax();
        }

        title.text = slotData.level == 0 ? "UNLOCK" : "LEVEL " + slotData.level;
        cost.text = Cost.FormattedString;

        upgrade.SetActive(!IsMaxLevel());
        levelMax.SetActive(IsMaxLevel());
    }

    public virtual void SpendMoney()
    {
        if (SpendValue <= Cost)
        {
            if (Game.Instance.gameData.SpentCoin(SpendValue))
            {
                slotData.SpendCost(SpendValue);
            }
            else
            {

                SecuredDouble money = Game.Instance.gameData.money;
                if (SpendValue > money)
                {
                    if (Game.Instance.gameData.SpentCoin(money))
                    {
                        slotData.SpendCost(money);
                    }
                }
            }
        }
        else
        {
            if (Game.Instance.gameData.SpentCoin(Cost))
            {
                slotData.SpendCost(Cost);
            }
        }

        if (slotData.cost.Round() <= 0)
        {
            Upgrade();
        }

        UpdateInfo();
    }

    public virtual void Upgrade()
    {
        slotData.Upgrade();
        slotData.cost = CostMax();

        if (slotBase == null)
        {
            GenAvatar();
        }
        slotBase.UpdateInfo();

        UpgradeEffect();

        MoneyEffect moneyEffect = upgrade.GetComponent<MoneyEffect>();
        if (moneyEffect != null)
        {
            moneyEffect.StopSpend();
        }
    }

    public void UpgradeEffect()
    {
        GameObject effect = Instantiate(GameResource.Instance.upgradeEffect);
        effect.transform.position = transform.position;
        Destroy(effect, 2);

        slotBase.transform.DOScale(slotBaseScale * 1.2f, 0.2f).SetEase(Ease.OutBack).onComplete += () => {
            slotBase.transform.DOScale(slotBaseScale, 0.2f);
        };

        SoundManager.Instance.PlayUpgrade(transform.position);
    }
}
