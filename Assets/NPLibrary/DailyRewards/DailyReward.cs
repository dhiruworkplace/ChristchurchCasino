using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class DailyReward : ScriptableObject
{
    private const string DAILY_REWARD = "DAILY_REWARD";

    public string id;
    public Sprite sprite;

    [SerializeField] private string description;
    public virtual string Description() {
        return description;
    }

    public int Index {
        get; private set;
    }

    private DailyRewards dailyRewards {
        get; set;
    }

    private string Key {
        get {
            return DAILY_REWARD + Index;
        }
    }

    public bool GotReward {
        get {
            return GetBool(Key);
        }

        private set {
            SetBool(Key, value);
        }
    }

    public bool Active {
        get {
            return !GotReward && dailyRewards.DayOffset == Index;
        }
    }

    public virtual void Initialize(DailyRewards dailyRewards, int index) {
        Index = index;
        this.dailyRewards = dailyRewards;
    }

    public virtual void Get() {
        if (!Active) {
            return;
        }

        GotReward = true;
        GetReward();
    }

    protected abstract void GetReward();

    public void Reset() {
        PlayerPrefs.DeleteKey(Key);
    }

    private void SetBool(string key, bool value) {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    private bool GetBool(string key, bool defaultValue = false) {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }
}
