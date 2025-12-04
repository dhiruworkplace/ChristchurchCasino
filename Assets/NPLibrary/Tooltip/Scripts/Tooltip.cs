using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tooltip : MonoBehaviour {
    
	public RectTransform tooltip;
	public Text content;
    public RectTransform point;
	public delegate void Complete();

	private static Tooltip instance;
	public static Tooltip Instance{
		get {
			if (instance == null)
				instance = FindObjectOfType<Tooltip>();
			return instance;
		}
	}

	private Vector2 tooltipLeft
    {
        get
        {
			Vector2 p = new Vector2(GetComponent<RectTransform>().anchoredPosition.x - tooltip.rect.width / 2, GetComponent<RectTransform>().anchoredPosition.y);
            return p;
        }
    }

    private Vector2 tooltipRight
    {
        get
        {
			Vector2 p = new Vector2(GetComponent<RectTransform>().anchoredPosition.x + tooltip.rect.width / 2, GetComponent<RectTransform>().anchoredPosition.y);
            return p;
        }
    }

	public void Show(string content, Vector3 target, float time, Complete complete){
		if (tooltip.gameObject.activeSelf) return;
		StartCoroutine(Active(content, target, time, complete));
	}

	private IEnumerator Active(string content, Vector3 target, float time, Complete complete){
		this.content.text = content;
        point.gameObject.SetActive(true);
        tooltip.gameObject.SetActive(true);
		transform.position = target;

        //point.anchoredPosition = new Vector2(point.anchoredPosition.x, tooltip.anchoredPosition.y);
		yield return new WaitForEndOfFrame();
		tooltip.gameObject.SetActive(false);
        tooltip.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        float width = this.content.canvas.GetComponent<RectTransform>().rect.width;

        float offsetLeftX = tooltipLeft.x + width / 2;
        float offsetRightX = tooltipRight.x - width / 2;
        float offsetX = 0;
        if (offsetLeftX < 0) offsetX = Mathf.Abs(offsetLeftX);
        if (offsetRightX > 0) offsetX = -offsetRightX;
        GetComponent<RectTransform>().anchoredPosition += new Vector2(offsetX, 0);
        point.transform.position = new Vector3(target.x, point.transform.position.y, point.transform.position.z);

        yield return new WaitForSeconds(time);
        tooltip.gameObject.SetActive(false);
        point.gameObject.SetActive(false);
 
        if (complete != null) complete.Invoke();
	}
}
