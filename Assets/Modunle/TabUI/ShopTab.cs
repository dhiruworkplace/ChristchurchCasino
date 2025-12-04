using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTab : Tab {

    public Image tabButton;
    public Color activeColor = Color.white;
    public Color disableColor = Color.white;

    public override void Show()
    {
        base.Show();
        tabButton.color = activeColor;
    }

    public override void Dismiss()
    {
        base.Dismiss();
        tabButton.color = disableColor;
    }

}
