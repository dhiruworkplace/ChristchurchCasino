using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollect : MonoBehaviour
{

    [SerializeField] private Image image;

    private Collector collector;
    private Sprite icon;
    private Vector3 target;

    public void Initialize(Collector collector, Sprite icon, Vector3 target)
    {
        this.collector = collector;
        this.icon = icon;
        this.target = target;

        image.sprite = icon != null ? icon : collector.iconDefault;
        image.SetNativeSize();

        StartCoroutine(MoveTo(target));
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        yield return new WaitForSeconds(Random.RandomRange(0, 0.5f));

        while(Vector3.Distance(transform.position, target) > 0.1f)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * collector.moveSpeed);
        }

        collector.Remove(this);
    }
}
