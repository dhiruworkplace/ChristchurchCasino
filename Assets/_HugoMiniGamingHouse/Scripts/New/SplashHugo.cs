using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashHugo : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(ChangeScene), 2.5f);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Game");
    }
}