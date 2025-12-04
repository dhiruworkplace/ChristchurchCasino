using UnityEngine;

public class SettingHugo : MonoBehaviour
{
    [Space(5)]
    public GameObject musicOn, musicOff;
    public GameObject soundOn, soundOff;

    private void Start()
    {
        soundOn.SetActive(AppHugo.sound.Equals(1));
        musicOn.SetActive(AppHugo.music.Equals(1));
        soundOff.SetActive(AppHugo.sound.Equals(0));
        musicOff.SetActive(AppHugo.music.Equals(0));
    }

    public void SetMusic()
    {
        if (AppHugo.music.Equals(1))
        {
            AppHugo.music = 0;
            AudioHugo.instance.PauseMusic();
        }
        else
        {
            AppHugo.music = 1;
            AudioHugo.instance.PlayMusic();
        }
        musicOn.SetActive(AppHugo.music.Equals(1));
        musicOff.SetActive(AppHugo.music.Equals(0));
        AudioHugo.instance.PlaySound(0);
    }

    public void SetSound()
    {
        if (AppHugo.sound.Equals(1))
        {
            AppHugo.sound = 0;
            SoundController.SfxEnable = false;
        }
        else
        {
            AppHugo.sound = 1;
            SoundController.SfxEnable = true;
        }
        soundOn.SetActive(AppHugo.sound.Equals(1));
        soundOff.SetActive(AppHugo.sound.Equals(0));
        AudioHugo.instance.PlaySound(0);
    }
}