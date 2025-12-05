using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChipMachine : SlotBase
{
    public IngredientType type;
    public Transform point;
    public Transform playPoint;
    public MoneyPlace moneyPlace;
    public MachineAnimation chipMachineAnimation;
    public AudioClip leaveSound;
    public float timeRate = 0.1f;
    protected float timeCount;
    protected List<MachineResource.IngredientData> ingredientDatas = new List<MachineResource.IngredientData>();
    public List<MachineResource.IngredientData> IngredientDatas
    {
        get
        {
            return ingredientDatas;
        }
    }

    public bool IsDone
    {
        get
        {
            return ingredientDatas.Count == 0;
        }
    }

    private List<Character> characters = new List<Character>();
    public bool HasCharacter
    {
        get
        {
            return characters.Count > 0;
        }
    }

    public TextMeshProUGUI ingredientInfo;
    private SecuredDouble moneyValue;
    protected List<Character> collisionCharacters = new List<Character>();
    public Material mat;
    public Color[] colors;

    private void Start()
    {
        if (mat != null)
            mat.color = colors[AppChrist.selectedTheme];
    }

    public void Get(SortSlot ss)
    {
        foreach (var item in ingredientDatas.ToArray())
        {
            if (item.count == 0)
            {
                continue;
            }

            List<SortObject> sortObjects = ss.SortObjects;
            List<SortObject> sos = sortObjects.FindAll((SortObject a) => a.GetComponent<ObjectType>().ingredientType == item.ingredientType);

            if (sos == null || sos.Count == 0)
            {
                continue;
            }

            sos.Sort((SortObject a, SortObject b) => b.Position.y.CompareTo(a.Position.y));

            SortObject sortObject = sos[0];
            if (sortObject != null)
            {
                ss.RemoveObject(sortObject);
                float time = 0.3f;
                sortObject.MoveToWorldPosition(point, time);
                Destroy(sortObject.gameObject, time);

                item.count--;
                if (item.count == 0)
                {
                    ingredientDatas.Remove(item);
                }

                SoundManager.Instance.PlayMachineGet(transform.position);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            collisionCharacters.Add(character);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            collisionCharacters.Remove(character);
        }
    }

    protected virtual void OnTriggers()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= timeRate)
        {
            foreach (var character in collisionCharacters)
            {
                if (character != null)
                {
                    Get(character.sortSlot);
                }
            }

            timeCount = 0;
        }
    }

    public void GetIngredient()
    {
        ingredientDatas = new List<MachineResource.IngredientData>();
        MachineResource machineResource = slotView.SlotResource as MachineResource;
        foreach (var item in machineResource.levelDatas[slotView.slotData.level].ingredientDatas)
        {
            ingredientDatas.Add(item.Clone());
        }
    }

    public void UpdateText()
    {
        string s = "";
        foreach (var item in ingredientDatas)
        {
            s += GetIcon(item.ingredientType) + "  " + item.count + "  ";
        }
        ingredientInfo.text = s;
        ingredientInfo.gameObject.SetActive(ingredientDatas.Count != 0);
    }

    public string GetIcon(IngredientType ingredientType)
    {
        switch (ingredientType)
        {
            case IngredientType.Chip:
                return "<sprite=\"ChipLogo\" index=0>";
                break;
            case IngredientType.Card:
                return "<sprite=\"CardLogo\" index=0>";
                break;
        }

        return "";
    }

    public void AddCharacter(Character character)
    {
        this.characters.Add(character);
        GetIngredient();
    }

    public void RemoveCharacter(Character character)
    {
        this.characters.Remove(character);

        MachineResource machineResource = slotView.SlotResource as MachineResource;
        SecuredDouble bonus = machineResource.levelDatas[slotView.slotData.level].bonus;
        moneyValue = bonus / character.sortSlot.ObjectCount;

        for (int i = 0; i < character.sortSlot.ObjectCount; i++)
        {
            StartCoroutine(Put(character.sortSlot, moneyPlace.sortSlot));
        }

        if (chipMachineAnimation != null)
            chipMachineAnimation.Trigger();

        SoundController.Instance.PlaySFXClip(leaveSound, 1);
    }

    public IEnumerator Put(SortSlot character, SortSlot moneyPlace)
    {
        yield return new WaitForSeconds(1);
        while (character.ObjectCount > 0)
        {
            SortObject sortObject = character.EndObject;

            if (sortObject != null)
            {
                sortObject.GetComponent<MoneyValue>().value = moneyValue;
                character.RemoveObject(sortObject);
                moneyPlace.AddObject(sortObject);

                SoundManager.Instance.PlayGetMoneySound(transform.position);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Update()
    {
        UpdateText();
        OnTriggers();
    }
}
