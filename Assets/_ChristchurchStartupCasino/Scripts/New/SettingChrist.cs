using UnityEngine;

public class SettingChrist : MonoBehaviour
{
    [Space(5)]
    public GameObject musicOn, musicOff;
    public GameObject soundOn, soundOff;

    private void Start()
    {
        soundOn.SetActive(AppChrist.sound.Equals(1));
        musicOn.SetActive(AppChrist.music.Equals(1));
        soundOff.SetActive(AppChrist.sound.Equals(0));
        musicOff.SetActive(AppChrist.music.Equals(0));
    }

    public void SetMusic()
    {
        if (AppChrist.music.Equals(1))
        {
            AppChrist.music = 0;
            AudioChrist.instance.PauseMusic();
        }
        else
        {
            AppChrist.music = 1;
            AudioChrist.instance.PlayMusic();
        }
        musicOn.SetActive(AppChrist.music.Equals(1));
        musicOff.SetActive(AppChrist.music.Equals(0));
        AudioChrist.instance.PlaySound(0);
    }

    public void SetSound()
    {
        if (AppChrist.sound.Equals(1))
        {
            AppChrist.sound = 0;
            SoundController.SfxEnable = false;
        }
        else
        {
            AppChrist.sound = 1;
            SoundController.SfxEnable = true;
        }
        soundOn.SetActive(AppChrist.sound.Equals(1));
        soundOff.SetActive(AppChrist.sound.Equals(0));
        AudioChrist.instance.PlaySound(0);
    }
}