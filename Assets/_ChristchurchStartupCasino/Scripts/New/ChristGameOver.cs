using TMPro;
using UnityEngine;

public class ChristGameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        if (AppChrist.isLevels)
            scoreText.text = Game.Instance.coins + "/" + ((AppChrist.selectedLevel) * 1000);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Game.Instance.coins = 0;
        ChristTimer.instance.StartTimer();
        gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        if (AppChrist.selectedLevel < 28)
        {
            AppChrist.selectedLevel++;
            FindAnyObjectByType<HomeChrist>().levelNo.text = "Level " + AppChrist.selectedLevel.ToString("00");
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