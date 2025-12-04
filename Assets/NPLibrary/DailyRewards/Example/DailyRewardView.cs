using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardView : View {

    public Text text;

    protected override List<object> GetData() {
        List<object> data = new List<object>();
        data.AddRange(DailyRewards.Instance.dailyRewards);
        return data;
    }

    private void Update() {
        text.text = DailyRewards.Instance.TimeOffset.ToString();
    }
}
