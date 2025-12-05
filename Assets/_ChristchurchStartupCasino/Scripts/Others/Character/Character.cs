using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    public CharacterData.Type type;
    public Transform parent;
    public CharacterAnimation characterAnimation;
    public SortSlot sortSlot;
    public int objectCountMax = 30;
    public bool IsObjectMax
    {
        get
        {
            return sortSlot.ObjectCount >= objectCountMax;
        }
    }

    protected CharacterData characterData;
    protected GameObject character;

    public CharacterData CharacterData
    {
        get
        {
            return characterData;
        }
    }

    protected virtual void Start()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        characterData = GameResource.Instance.CharacterData(type);

        character = Instantiate(characterData.model, parent);
        character.transform.localPosition = Vector3.zero;
        Animator animator = character.GetComponentInChildren<Animator>();
        characterAnimation.SetAnimator(animator);

        sortSlot.transform.parent = parent;
    }

    public bool AddObject(SortObject sortObject)
    {
        if (sortSlot.ObjectCount >= objectCountMax)
        {
            return false;
        }

        sortSlot.AddObject(sortObject);
        SoundManager.Instance.PlayGetObject(transform.position);
        return true;
    }

    private void Update()
    {
        characterAnimation.SetBlendIdle(sortSlot.SortObjects.Count > 0 ? 1 : 0);
        characterAnimation.SetBlendMove(sortSlot.SortObjects.Count > 0 ? 1 : 0);
    }

}
