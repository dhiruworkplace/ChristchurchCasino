using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPopup : MonoFrame
{
	public Text title;
	public Text status;

	public RippleButton confirmButton;

	public void UpdateUI(string title, string status)
	{
		this.title.text = title;
		this.status.text = status;
	}

	public override void Show(object data)
	{
		confirmButton.onClick.AddListener(() =>
			{
				
				base.OnCloseButtonClicked();
			});
		base.Show(data);
	}

	public override void Dismiss()
	{
		confirmButton.onClick.RemoveAllListeners();
		base.Dismiss();
	}

	public override void OnCloseButtonClicked()
	{
		base.OnCloseButtonClicked();
	}
}
