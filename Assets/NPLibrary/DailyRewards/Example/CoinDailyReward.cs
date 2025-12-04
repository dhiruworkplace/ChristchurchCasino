using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinDailyReward", menuName = "CoinDailyReward")]
public class CoinDailyReward : DailyReward {

    public int coin;

    protected override void GetReward() {
        Debug.Log("Got ok");
    }
}
