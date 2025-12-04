using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardItem : Item
{
    public Text text;
    public Image image;

    private DailyReward dailyReward;

    public override void UpdateUI(View view, object data) {
        base.UpdateUI(view, data);

        dailyReward = (DailyReward)data;
        UpdateUI();
    }

    private void UpdateUI() {
        text.text = dailyReward.Index + "";
        image.color = dailyReward.Active ? Color.red : Color.white;
    }

    public void Get() {
        dailyReward.Get();
        UpdateUI();
    }
}
