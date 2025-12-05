using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameResource", fileName = "GameResource")]
public class GameResource : ScriptableObject
{
    private static GameResource instance;

    public static GameResource Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResource>("GameResource");
            }
            return instance;
        }
    }

    public GameObject upgradeEffect;
    public GameObject textEffect;
    public GameObject customerPrefab;
    public GameData gameDataBase;

    public List<CharacterData> characterDatas = new List<CharacterData>();
    public CharacterData CharacterData(CharacterData.Type type)
    {
        return characterDatas.Find((CharacterData cd) => cd.type == type);
    }

    public List<BotResource> botDatas = new List<BotResource>();
    public BotResource BotResource(BotType type)
    {
        return botDatas.Find((BotResource bd) => bd.type == type);
    }

    public CharacterData RandomCharacter
    {
        get
        {
            return characterDatas[Random.RandomRange(0, characterDatas.Count)];
        }
    }

    public List<SlotResource> slotResources = new List<SlotResource>();

    public SlotResource SlotResourceWithType(SlotData.Type type)
    {
        return slotResources.Find((SlotResource sr) => sr.type == type);
    }

    public void OnAwake()
    {

    }
}

[System.Serializable]
public class CharacterData
{
    public enum Type { Player, Customer, Bot }

    public Type type;
    public GameObject model;
}

[System.Serializable]
public class BotResource
{
    public BotType type;
    public GameObject model;
}

[System.Serializable]
public class MapData
{
    public GameObject model;
}