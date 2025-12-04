using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectMaxView : MonoBehaviour
{
    public Character character;
    public TextMeshPro text;

    private void Update()
    {
        //text.text = character.IsObjectMax ? "MAX" : character.sortSlot.ObjectCount + "/" + character.objectCountMax;
        text.text = character.IsObjectMax ? "MAX" : "";
    }
}
