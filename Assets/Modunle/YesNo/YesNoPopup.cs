using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class YesNoPopup : MonoFrame {

    [SerializeField] private GameObject buttonYes;
    [SerializeField] private GameObject buttonNo;

    private Action actionYes;
    private Action actionNo;
    public TextMeshProUGUI title;
    public TextMeshProUGUI yesText;

    public override void Show(object data)
    {
        base.Show(data);
    }

    public void Yes()
    {
        if (actionYes != null)
        {
            actionYes.Invoke();
        }
        OnCloseButtonClicked();
    }

    public void No()
    {
        if (actionNo != null)
        {
            actionNo.Invoke();
        }
        OnCloseButtonClicked();
    }

    public void SetContent(string yesText, string noText, string title, Action actionYes, Action actionNo)
    {
        buttonYes.SetActive(yesText != null);

        buttonNo.SetActive(noText != null);

        this.actionYes = actionYes;
        this.actionNo = actionNo;
        this.title.text = title;
        this.yesText.text = yesText;
    }

    public override void OnCloseButtonClicked()
    {
        base.OnCloseButtonClicked();
        actionYes = null;
        actionNo = null;
    }
}
