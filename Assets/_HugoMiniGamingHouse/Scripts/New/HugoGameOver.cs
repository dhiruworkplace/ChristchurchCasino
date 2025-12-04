using TMPro;
using UnityEngine;

public class HugoGameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        if (AppHugo.isLevels)
            scoreText.text = Game.Instance.coins + "/" + ((AppHugo.selectedLevel) * 1000);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Game.Instance.coins = 0;
        HugoTimer.instance.StartTimer();
        gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        if (AppHugo.selectedLevel < 28)
        {
            AppHugo.selectedLevel++;
            FindAnyObjectByType<HomeHugo>().levelNo.text = "Level " + AppHugo.selectedLevel.ToString("00");
            Restart();
        }
        else
        {
            Home();
        }
    }
    public void Home()
    {
        Time.timeScale = 1f;
        GameManager.Instance.Reload();
        gameObject.SetActive(false);
    }
}