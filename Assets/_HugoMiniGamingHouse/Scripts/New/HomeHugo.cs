using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HomeHugo : MonoBehaviour
{
    public GameObject agreementPanel;
    public GameObject tcTick;
    public GameObject ppTick;
    public Button acceptBtn;

    public GameObject levelsPanel;
    public TextMeshProUGUI coinText;

    public GameObject claimBtn;
    public GameObject claimBtn1;
    public GameObject timerImg;
    public TextMeshProUGUI questTimer;
    public TextMeshProUGUI questTimer1;
    public TextMeshProUGUI rewardText;

    public TextMeshProUGUI levelNo;
    private List<int> rewards = new List<int>() { 1000, 2000, 3000, 4000, 5000 };
    int reward = 1000;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SetCoins), 0.5f);
        InvokeRepeating(nameof(CheckQuest), 0f, 1f);

        if (!PlayerPrefs.HasKey("agreement"))
        {
            PlayerPrefs.SetInt("agreement", 1);
            PlayerPrefs.Save();
            agreementPanel.SetActive(true);
        }
    }

    public void TickAgreement(bool isTc)
    {
        if (isTc)
        {
            tcTick.SetActive(!tcTick.activeSelf);
        }
        else
        {
            ppTick.SetActive(!ppTick.activeSelf);
        }
        acceptBtn.interactable = (tcTick.activeSelf && ppTick.activeSelf);
        Click();
    }

    public void Play()
    {
        AppHugo.isLevels = false;
        Game.Instance.coins = 0;
        //levelNo.gameObject.SetActive(false);
        GameManager.Instance.StartGame();        
    }

    public void PlayLevel()
    {
        Game.Instance.coins = 0;
        levelNo.text = "Level " + AppHugo.selectedLevel.ToString("00");
        levelsPanel.SetActive(false);
        AppHugo.isLevels = true;
        GameManager.Instance.StartGame();
        //levelNo.gameObject.SetActive(true);
    }

    public void SetCoins()
    {
        coinText.text = Game.Instance.gameData.money.Value.ToString("00");
    }

    private void StopTimer()
    {
        CancelInvoke(nameof(CheckQuest));
    }

    private void CheckQuest()
    {
        System.DateTime lastDT = new System.DateTime();
        if (!PlayerPrefs.HasKey("lastSpin"))
        {
            //PlayerPrefs.SetString("lastSpin", DateTime.Now.AddHours(24).ToString());
            //PlayerPrefs.Save();

            claimBtn.SetActive(true);
            claimBtn1.SetActive(true);
            timerImg.SetActive(false);
            questTimer.text = "Claim";
            questTimer1.text = "Claim";
            StopTimer();
            return;
        }
        lastDT = System.DateTime.Parse(PlayerPrefs.GetString("lastSpin"));

        System.TimeSpan diff = (lastDT - System.DateTime.Now);
        questTimer.text = string.Format("{0:D2}:{1:D2}:{2:D2}", diff.Hours, diff.Minutes, diff.Seconds);
        questTimer1.text = string.Format("{0:D2}:{1:D2}:{2:D2}", diff.Hours, diff.Minutes, diff.Seconds);

        if (diff.TotalSeconds <= 0)
        {
            StopTimer();
            claimBtn.SetActive(true);
            timerImg.SetActive(false);
            questTimer.text = "Claim";
            claimBtn1.SetActive(true);
            questTimer1.text = "Claim";

            int day = PlayerPrefs.GetInt("DailyReward", 0);
            reward = (day * 1000);
            if (day >= 7)
                reward = rewards[Random.Range(0, rewards.Count)];

            rewardText.text = reward.ToString();
        }
    }

    private void StartTimer()
    {
        PlayerPrefs.SetString("lastSpin", System.DateTime.Now.AddHours(24).ToString());
        PlayerPrefs.Save();
        claimBtn.SetActive(false);
        timerImg.SetActive(true);
        claimBtn1.SetActive(false);

        InvokeRepeating(nameof(CheckQuest), 0f, 1f);
    }

    public void ClaimBtn()
    {
        int day = PlayerPrefs.GetInt("DailyReward", 0);
        if (day < 7)
        {
            day++;
            PlayerPrefs.SetInt("DailyReward", day);
            Game.Instance.gameData.AddMoney(reward);

            Invoke(nameof(StartTimer), 0f);
            SetCoins();
        }
    }

    public void Click()
    {
        AudioHugo.instance.PlaySound(0);
    }
}