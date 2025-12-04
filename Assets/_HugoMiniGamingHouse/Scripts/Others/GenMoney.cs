using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenMoney : MonoBehaviour
{
    public GameObject moneyPrefab;
    public int count = 6;
    public Character character;

    private void Start()
    {
        if (character == null)
        {
            character = GetComponent<Character>();
        }

        for (int i = 0; i < count; i++)
        {
            GameObject g = Instantiate(moneyPrefab);
            SortObject sortObject = g.GetComponent<SortObject>();
            character.sortSlot.AddObjectNotEffect(sortObject);
        }
    }
}
