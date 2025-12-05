using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    public void OnPause(bool pause)
    {
        if (pause)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void Back()
    {
        Time.timeScale = 1f;
        GameManager.Instance.Reload();
        AppChrist.restart = true;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        AudioChrist.instance.PlaySound(0);
        GameManager.SetState(GameState.None);
        ChristTimer.instance.StopTimer();
        Game.Instance.uiCanvas.SetActive(true);
        Game.Instance.winPanel.SetActive(false);
    }
}