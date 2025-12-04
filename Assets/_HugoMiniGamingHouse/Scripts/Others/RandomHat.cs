using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHat : MonoBehaviour
{
    public List<GameObject> hats = new List<GameObject>();

    private void Start()
    {
        foreach (var item in hats)
        {
            item.SetActive(false);
        }

        hats[Random.RandomRange(0, hats.Count)].SetActive(true);
    }
}
