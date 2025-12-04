using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoneyPlace : MonoBehaviour
{
    public SortSlot sortSlot;

    public float timeRate = 0.1f;
    protected float timeCount;
    protected SecuredDouble profit;

    public void Add(SortObject so)
    {
        sortSlot.AddObject(so);
    }

    public void Put(Transform character)
    {
        SortObject sortObject = sortSlot.EndObject;

        if (sortObject != null)
        {
            sortSlot.RemoveObject(sortObject);
            sortObject.MoveToWorldPosition(character);
            sortObject.transform.DOScale(Vector3.zero, 0.6f);
            Destroy(sortObject.gameObject, 0.3f);
            MoneyValue moneyValue = sortObject.GetComponent<MoneyValue>();
            if (moneyValue != null)
            {
                Game.Instance.gameData.AddMoney(moneyValue.value);
                if (AppHugo.isLevels)
                {
                    Game.Instance.coins += (int)moneyValue.value;
                    if (Game.Instance.coins >= (AppHugo.selectedLevel) * 1000)
                    {
                        Game.Instance.ShowGameOver(true);
                    }
                }
                AppHugo.collectCoin += (int)moneyValue.value;

                GameObject text = Instantiate(GameResource.Instance.textEffect);
                text.transform.position = character.transform.position + Vector3.up;
                text.GetComponent<TextEffect>().Initialize("+" + moneyValue.value.Round().FormattedString);

                SoundManager.Instance.PlayGetMoneySound(transform.position);
            }
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        timeCount += Time.deltaTime;
        if (timeCount >= timeRate)
        {
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                Put(character.sortSlot.transform);
            }

            timeCount = 0;
        }
    }
}
