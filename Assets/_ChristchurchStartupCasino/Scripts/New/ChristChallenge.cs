using UnityEngine;
using UnityEngine.UI;

public class ChristChallenge : MonoBehaviour
{
    public int ID = 1;
    public Button claimBtn;
    public int target = 0;
    public int reward = 0;
    public bool isCollected = false;

    private void OnEnable()
    {
        CheckChallenge();
    }

    private void CheckChallenge()
    {
        claimBtn.gameObject.SetActive(AppChrist.collectCoin >= target);
    }

    public void ClaimReward()
    {
        isCollected = true;
        claimBtn.interactable = false;
        Game.Instance.gameData.AddMoney(reward);
    }
}