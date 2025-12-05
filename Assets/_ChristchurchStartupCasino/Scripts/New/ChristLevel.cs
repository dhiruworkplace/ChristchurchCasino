using TMPro;
using UnityEngine;

public class ChristLevel : MonoBehaviour
{
    public int levelNo;
    public TextMeshProUGUI levelNoText;
    public TextMeshProUGUI dataText;
    public GameObject currLvl;
    public GameObject lockObj;

    // Start is called before the first frame update
    void Start()
    {
        levelNo = transform.GetSiblingIndex() + 1;
        levelNoText.text = "Level " + levelNo.ToString();

        currLvl.SetActive(AppChrist.saveLevel.Equals(levelNo));
        lockObj.SetActive(AppChrist.saveLevel < levelNo);
        levelNoText.color = (AppChrist.saveLevel < levelNo) ? new Color32(182, 182, 182, 255) : new Color32(49, 29, 8, 255);

        dataText.text = (levelNo * 1000).ToString();
    }

    public void OnClick()
    {
        if (lockObj.activeSelf)
            return;

        AudioChrist.instance.PlaySound(0);
        AppChrist.selectedLevel = levelNo;
        FindAnyObjectByType<HomeChrist>().PlayLevel();
    }
}