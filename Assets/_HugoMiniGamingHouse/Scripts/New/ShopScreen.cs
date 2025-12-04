using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShopScreen : MonoBehaviour
{
    public GameObject[] themes;
    private List<int> prices = new List<int>() { 10000, 20000, 30000, 40000, 50000, 60000 };
    public GameObject noCoinPanel;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("p0", 1);
        PlayerPrefs.Save();

        CheckAllTheme();
    }

    private void CheckAllTheme()
    {
        for (int i = 0; i < themes.Length; i++)
        {
            GameObject m = themes[i];
            if (PlayerPrefs.GetInt("p" + i, 0) == 1)
            {
                m.transform.GetChild(1).gameObject.SetActive(true);
                m.transform.GetChild(2).gameObject.SetActive(false);
                m.transform.GetChild(3).gameObject.SetActive(false);

                if (AppHugo.selectedTheme.Equals(i))
                {
                    m.transform.GetChild(1).gameObject.SetActive(false);
                    m.transform.GetChild(2).gameObject.SetActive(true);
                }
            }
        }
    }

    public void SelectTheme(int index)
    {
        if (PlayerPrefs.GetInt("p" + index, 0) == 1)
        {
            //if (!Container.selectedBg.Equals(index))
            {
                AppHugo.selectedTheme = index;
                CheckAllTheme();
            }
        }
        else
        {
            int coins = (int)Game.Instance.gameData.money.Value;
            if (coins >= prices[index])
            {
                MoneyValue mv = new MoneyValue();
                mv.value = coins;
                Game.Instance.gameData.SpendCoin(mv.value);

                themes[index].transform.GetChild(1).gameObject.SetActive(true);
                themes[index].transform.GetChild(2).gameObject.SetActive(false);
                themes[index].transform.GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("p" + index, 1);
                PlayerPrefs.Save();

                FindAnyObjectByType<HomeHugo>().SetCoins();
            }
            else
                noCoinPanel.SetActive(true);
        }
        AudioHugo.instance.PlaySound(0);
    }
}