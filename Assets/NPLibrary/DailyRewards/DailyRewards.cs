using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

[CreateAssetMenu(fileName = "DailyRewards", menuName = "DailyRewardsResource")]
public class DailyRewards : ScriptableObject
{
    private const string FIRST_DAY = "FIRST_DAY";
    private const string FMT = "O";

    private static DailyRewards instance;
    public static DailyRewards Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<DailyRewards>("DailyRewards");

                if (instance != null) {
                    instance.Initialize();
                }
            }
            return instance;
        }
    }

    public List<DailyReward> dailyRewards = new List<DailyReward>();

    public int RewardCount {
        get {
            return dailyRewards.Count;
        }
    }

    public DateTime Now {
        get {
            return DateTime.Now;
        }
    }

    public int DayOffset {
        get {
            TimeSpan timeSpan = Now - FirstDay;
            return (int)timeSpan.TotalHours / 24;
        }
    }

    public TimeSpan TimeOffset {
        get {
            TimeSpan timeSpan = Now - FirstDay;
            return new TimeSpan(1,0,0,0) - (timeSpan - new TimeSpan(DayOffset,0,0,0));
        }
    }

    public DateTime FirstDay {
        get {
            string s = PlayerPrefs.GetString(FIRST_DAY);

            if (string.IsNullOrEmpty(s)) {
                return Now;
            }

            DateTime dateTime = DateTime.ParseExact(s, FMT, CultureInfo.InvariantCulture);

            return dateTime;
        }

        private set {
            string s = value.ToString(FMT);
            PlayerPrefs.SetString(FIRST_DAY, s);
        }
    }

    public void Initialize() {
        if (!PlayerPrefs.HasKey(FIRST_DAY)) {
            FirstDay = Now;
        }

        for (int i = 0; i < dailyRewards.Count; i++) {
            dailyRewards[i].Initialize(this, i);
        }

        if (DayOffset >= RewardCount - 1) {
            Reset();
        }
    }

    public void Reset() {
        PlayerPrefs.DeleteKey(FIRST_DAY);
        foreach (var item in dailyRewards) {
            item.Reset();
        }
    }
}
