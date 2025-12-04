using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    public TextMeshProUGUI value;

    private IEnumerator Start()
    {
        yield return null;
        OnMoneyChanged();
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.AddListener(EventKey.OnCoinChanged, OnMoneyChanged);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventKey.OnCoinChanged, OnMoneyChanged);
    }

    private void OnMoneyChanged()
    {
        value.text = Game.Instance.gameData.money.Round().FormattedString;

        FindAnyObjectByType<HomeHugo>().SetCoins();
    }
}