using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : Manager
{
    private static Game instance;

    public GameLoader gameLoader;
    public GameObject uiCanvas;
    public TextMeshProUGUI targetText;

    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (Game)GameManager.Instance.GetManager<Game>();
            }

            return instance;
        }
    }

    public List<SlotView> slotViews = new List<SlotView>();
    public List<BotSlotView> botSlotViews = new List<BotSlotView>();
    public GameData gameData;
    public Transform getBoxPos;

    private float timeCount;

    private int _coins = 0;
    public int coins
    {
        get { return _coins; }
        set
        {
            _coins = value;
            targetText.text = ((AppChrist.selectedLevel) * 1000).ToString();
            if (value >= (AppChrist.selectedLevel) * 1000)
            {
                ShowGameOver(true);
            }
        }
    }

    public GameObject winPanel;
    public GameObject losePanel;

    private void Start()
    {
        if (AppChrist.restart)
        {
            AppChrist.restart = false;
            GameManager.Instance.StartGame();
        }
    }

    public override void Init()
    {
        if (Settings.GameData == null)
        {
            Settings.GameData = GameResource.Instance.gameDataBase;
        }
        gameData = Settings.GameData;
    }

    public override void StartGame()
    {

        for (int i = 0; i < slotViews.Count; i++)
        {
            if (i >= Settings.GameData.SlotCount)
            {
                slotViews[i].gameObject.SetActive(false);
                continue;
            }

            slotViews[i].Initialize(gameData.slotDatas[i]);
        }

        for (int i = 0; i < botSlotViews.Count; i++)
        {
            if (i >= Settings.GameData.BotCount)
            {
                botSlotViews[i].gameObject.SetActive(false);
                continue;
            }

            botSlotViews[i].Initialize(gameData.botDatas[i]);
        }
    }

    private void Update()
    {
        timeCount += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameData.AddMoney(1000);
        }
    }

    public void Save()
    {
        Settings.GameData = gameData;
    }

    public void ShowGameOver(bool isWin)
    {
        ChristTimer.instance.StopTimer();
        if (winPanel.activeSelf || losePanel.activeSelf)
            return;

        if (isWin)
        {
            winPanel.SetActive(true);
            gameData.AddMoney(1000);
            Time.timeScale = 0;
            if ((AppChrist.selectedLevel + 1) > AppChrist.saveLevel && AppChrist.selectedLevel <= 28)
                AppChrist.saveLevel = (AppChrist.selectedLevel + 1);
            coins = 0;
        }
        else
        {
            losePanel.SetActive(true);
        }
    }
}