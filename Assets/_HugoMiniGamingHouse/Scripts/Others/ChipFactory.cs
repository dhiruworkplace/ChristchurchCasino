using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ChipFactory : SlotBase
{
    public GetObjectPlace getObjectPlace;
    public PlaceFactory placeFactory;
    public GameObject chipPrefab;

    public Transform boxStart, boxEnd, chipStart, chipEnd;
    public TextMeshProUGUI inIngredient;
    public TextMeshProUGUI outIngredient;

    public float timeWork = 1;
    public int chipCount = 5;

    private float ChipGenTime
    {
        get
        {
            return 2f / chipCount;
        }
    }

    public override void UpdateInfo()
    {
        FactoryResource factoryResource = slotView.SlotResource as FactoryResource;
        getObjectPlace.slotMax = factoryResource.levelDatas[slotView.slotData.level].ingredientInCountMax;
        placeFactory.slotMax = factoryResource.levelDatas[slotView.slotData.level].ingredientOutCountMax;
        chipCount = factoryResource.levelDatas[slotView.slotData.level].ingredientCount;

        UpdateText();
    }

    public void UpdateText()
    {
        inIngredient.text = getObjectPlace.sortSlot.ObjectCount + "/" + getObjectPlace.slotMax;
        outIngredient.text = placeFactory.sortSlot.ObjectCount + "/" + placeFactory.slotMax;
    }

    protected IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeWork);
            SortObject sortObject = getObjectPlace.sortSlot.EndObject;
            if (sortObject != null)
            {
                getObjectPlace.sortSlot.RemoveObject(sortObject);
                float t1 = 0.3f;
                sortObject.MoveToWorldPosition(boxStart, t1);
                yield return new WaitForSeconds(t1);

                float t2 = 1f;
                sortObject.transform.DOMove(boxEnd.position, t2).onComplete += () => {
                    Destroy(sortObject.gameObject);
                };
                yield return new WaitForSeconds(t2);

                for (int i = 0; i < chipCount; i++)
                {
                    yield return new WaitUntil(() => !placeFactory.IsMax);
                    yield return new WaitForSeconds(ChipGenTime);
                    GameObject chip = Instantiate(chipPrefab);
                    chip.transform.eulerAngles = chipStart.eulerAngles;
                    chip.transform.position = chipStart.position;
                    SortObject so = chip.GetComponent<SortObject>();
                    so.transform.DOMove(chipEnd.position, t2).SetEase(Ease.Linear).onComplete += () => {
                        placeFactory.Add(so);
                    };
                }
            }
        }
    }

    private void Update()
    {
        UpdateText();
    }
}
