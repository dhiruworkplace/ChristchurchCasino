using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HugoLevel : MonoBehaviour
{
    public int levelNo;
    public TextMeshProUGUI levelNoText;
    public GameObject currLvl;
    public GameObject lockObj;

    // Start is called before the first frame update
    void Start()
    {
        levelNo = transform.GetSiblingIndex() + 1;
        levelNoText.text = levelNo.ToString("00");

        currLvl.SetActive(AppHugo.saveLevel.Equals(levelNo));
        lockObj.SetActive(AppHugo.saveLevel < levelNo);
    }

    public void OnClick()
    {
        if (lockObj.activeSelf)
            return;

        AudioHugo.instance.PlaySound(0);
        AppHugo.selectedLevel = levelNo;
        FindAnyObjectByType<HomeHugo>().PlayLevel();
    }
}