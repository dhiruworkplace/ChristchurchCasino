using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Ir;

public class Collector : MonoBehaviour
{
    private static Collector instance;
    public static Collector Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Collector>();
            }
            return instance;
        }
    }

    public Action OnComplete;
    public Action OnStep;

    public int CountMax = 50;
    public float moveSpeed = 10;
    public Sprite iconDefault;

    private Vector3 BottomLeft
    {
        get
        {
            return Constants.BottomLeft;
        }
    }

    private Vector3 UpRight
    {
        get
        {
            return Constants.UpRight;
        }
    }

    [SerializeField] private GameObject itemCollectPrefab;
    private List<ItemCollect> itemCollects = new List<ItemCollect>();

    public void Create(Vector3 target, Sprite icon, int count)
    {
        count = Mathf.Clamp(count, 0, CountMax);

        for (int i = 0; i < count; i++)
        {
            GameObject o = Instantiate(itemCollectPrefab, transform);
            o.transform.position = new Vector2(UnityEngine.Random.Range(BottomLeft.x, UpRight.y), UnityEngine.Random.Range(BottomLeft.y, UpRight.y));
            o.SetActive(true);
            ItemCollect itemCollect = o.GetComponent<ItemCollect>();
            itemCollect.Initialize(this, icon, target);
            itemCollects.Add(itemCollect);
        }
    }

    public void Create(Vector3 start, Vector3 target, Sprite icon)
    {
        GameObject o = Instantiate(itemCollectPrefab, transform);
        o.transform.position = start;
        o.SetActive(true);
        ItemCollect itemCollect = o.GetComponent<ItemCollect>();
        itemCollect.Initialize(this, icon, target);
        itemCollects.Add(itemCollect);
    }

    public void Remove(ItemCollect itemCollect)
    {
        itemCollects.Remove(itemCollect);
        Destroy(itemCollect.gameObject);
        if (OnStep != null)
        {
            OnStep.Invoke();
            //SoundManager.Instance.PlayBonus();
        }

        if (itemCollects.Count == 0)
        {
            if (OnComplete != null)
            {
                OnComplete.Invoke();
            }
        }
    }
}
