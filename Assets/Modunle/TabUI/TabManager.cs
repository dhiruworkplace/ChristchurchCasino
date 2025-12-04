using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour {

    public List<Tab> tabs = new List<Tab>();
    private Tab currentTab;

    private void OnEnable()
    {
        if (tabs == null || tabs.Count == 0) return;

        SetUp();
        HideAllTab();
        if (currentTab != null) OpenTab(currentTab.id);
        else OpenTab(tabs[0].id);
    }

    public void OpenTab(string id)
    {
        if (currentTab != null) currentTab.Dismiss();

        currentTab = tabs.Find((Tab a) => a.id == id);

        if (currentTab != null)
        {
            currentTab.Show();
        }
    }

    private void HideAllTab()
    {
        foreach (var tab in tabs)
        {
            tab.OnHide();
        }
    }

    private void SetUp()
    {
        foreach (var tab in tabs)
        {
            tab.SetUp(this);
        }
    }

}

public class Tab : TabMonoView
{

    public string id;

    public TabMonoView tabView;

    private TabManager tabManager;


    public virtual void SetUp(TabManager tabManager)
    {
        this.tabManager = tabManager;
    }

    public override void Dismiss()
    {
        tabView.Dismiss();
        tabView.gameObject.SetActive(false);
    }

    public override void OnHide()
    {
        tabView.gameObject.SetActive(false);
    }

    public override void Show()
    {
        tabView.gameObject.SetActive(true);
        tabView.Show();
    }

    public virtual void OnClick()
    {
        tabManager.OpenTab(id);
    }
}

public abstract class TabMonoView : MonoBehaviour
{

    public abstract void Show();

    public abstract void Dismiss();

    public abstract void OnHide();
}