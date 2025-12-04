using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    [ContextMenu("OnClick")]
	public void OnClick()
    {
        Game.Instance.gameData.AddMoney(100000);
    }

    //private void Start()
    //{
    //    OnClick();
    //}
}
