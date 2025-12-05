using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> factoryUpgrades = new List<GameObject>();
    public List<GameObject> machineUpgrades = new List<GameObject>();
    public List<GameObject> employees = new List<GameObject>();

    public GameData GameData
    {
        get
        {
            return Game.Instance.gameData;
        }
    }

    public List<SlotData> MachineDatas
    {
        get
        {
            List<SlotData> slotDatas = GameData.slotDatas;
            return slotDatas.FindAll((SlotData sl) => sl.type == SlotData.Type.CardMachine || sl.type == SlotData.Type.ChipMachine);
        }
    }

    public List<SlotData> UnlockedMachines
    {
        get
        {
            return MachineDatas.FindAll((SlotData s) => s.Unlocked);
        }
    }

    public List<BotData> BotDatas
    {
        get
        {
            return GameData.botDatas;
        }
    }

    public List<BotData> UnlockedBotDatas
    {
        get
        {
            return BotDatas.FindAll((BotData b) => b.Unlocked);
        }
    }

    private IEnumerator Start()
    {
        yield return null;
        OnUpgrade();
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.AddListener(EventKey.UPGRADE, OnUpgrade);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventKey.UPGRADE, OnUpgrade);
    }

    private void OnUpgrade()
    {
        foreach (var item in factoryUpgrades)
        {
            item.SetActive(UnlockedMachines.Count > 0);
        }

        List<SlotData> machineDatas = MachineDatas;
        for (int i = 0; i < machineDatas.Count; i++)
        {
            machineUpgrades[i].SetActive(UnlockedMachines.Count > i - 1);
        }

        List<BotData> botdatas = BotDatas;
        for (int i = 0; i < botdatas.Count; i++)
        {
            employees[i].SetActive((UnlockedBotDatas.Count > i - 1) && UnlockedMachines.Count > 1);
        }
    }
}
