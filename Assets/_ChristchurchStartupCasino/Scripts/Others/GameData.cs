using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public SecuredDouble money;
    public List<BotData> botDatas = new List<BotData>();
    public int BotCount
    {
        get
        {
            return botDatas.Count;
        }
    }

    public List<SlotData> slotDatas = new List<SlotData>();
    public int SlotCount
    {
        get
        {
            return slotDatas.Count;
        }
    }

    public void AddMoney(SecuredDouble value)
    {
        money += value;
        EventDispatcher.Instance.Dispatch(EventKey.OnCoinChanged, money);
        Game.Instance.Save();
    }

    public void SpendCoin(SecuredDouble value)
    {
        money -= value;
        EventDispatcher.Instance.Dispatch(EventKey.OnCoinChanged, money);
        Game.Instance.Save();
    }

    public bool SpentCoin(SecuredDouble value)
    {
        if (money >= value)
        {
            money -= value;
            EventDispatcher.Instance.Dispatch(EventKey.OnCoinChanged, money);
            Game.Instance.Save();
            return true;
        }
        return false;
    }

    public void AddPerson(BotData personData)
    {
        this.botDatas.Add(personData);
        Game.Instance.Save();
    }
}

[Serializable]
public class SlotData
{
    public enum Type { ChipFactory, CardFactory, ChipMachine, CardMachine }
    public Type type;
    public int level;
    public SecuredDouble cost;

    public int Index
    {
        get
        {
            return Game.Instance.gameData.slotDatas.IndexOf(this);
        }
    }

    public bool Unlocked {
        get
        {
            return level > 0;
        }
    }

    public void SpendCost(SecuredDouble value)
    {
        if (this.cost > 0)
        {
            this.cost -= value;
            Game.Instance.Save();
        }
    }

    public void Upgrade()
    {
        level++;
        Game.Instance.Save();
        EventDispatcher.Instance.Dispatch(EventKey.UPGRADE);
    }
}

[Serializable]
public class BotData
{
    public BotType type;
    public int level;
    public SecuredDouble cost;

    public bool Unlocked
    {
        get
        {
            return level > 0;
        }
    }

    public int Index
    {
        get
        {
            return Game.Instance.gameData.botDatas.IndexOf(this);
        }
    }

    public BotData(BotType type)
    {
        this.type = type;
    }

    public void SpendCost(SecuredDouble value)
    {
        if (this.cost > 0)
        {
            this.cost -= value;
            Game.Instance.Save();
        }
    }

    public void Upgrade()
    {
        level++;
        Game.Instance.Save();
        EventDispatcher.Instance.Dispatch(EventKey.UPGRADE);
    }
}

public enum IngredientType { Box, Chip, Card }
public enum BotType { Machine, Factory }