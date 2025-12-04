using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    private void OnEnable()
    {
        AudioHugo.instance.PlaySound(0);
        Time.timeScale = 0f;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
        AudioHugo.instance.PlaySound(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
        AudioHugo.instance.PlaySound(0);
    }

    public void Continue()
    {
        AudioHugo.instance.PlaySound(0);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}