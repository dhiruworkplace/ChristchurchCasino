using TMPro;
using UnityEngine;

public class ChristTimer : MonoBehaviour
{
    public static ChristTimer instance;
    public TextMeshProUGUI timerText;
    public int totalSec;

    private int _time;
    public int time
    {
        get { return _time; }
        set
        {
            _time = value;
            System.TimeSpan ts = System.TimeSpan.FromSeconds(value);
            timerText.text = string.Format("{0:D2}:{1:D2}", (int)ts.Minutes, ts.Seconds);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartTimer();
    }

    public void StartTimer()
    {
        time = totalSec;
        CancelInvoke(nameof(MyTimer));
        InvokeRepeating(nameof(MyTimer), 1f, 1f);
    }

    public void StopTimer()
    {
        CancelInvoke(nameof(MyTimer));
    }

    private void MyTimer()
    {
        time--;

        if (time <= 0)
        {
            StopTimer();
            Invoke(nameof(ShowGameover), 1f);
        }
    }

    private void ShowGameover()
    {
        Game.Instance.ShowGameOver(false);
    }
}