using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrivacyStatement : MonoBehaviour
{
    public GameObject popup;
    public TextMeshProUGUI contentUI;
    public string content = "We use device identifiers and information related to your use of this game to personalize content and ads, to provide social media features, and to analyze our traffic. We also share such identifiers and other information from your device with our social media, advertising, and analytics partners. Please read our privacy policy for more information and be sure you agree to all policies before continuing.";

    public string link = "https://sites.google.com/view/biboprivacypolicy";

    private bool Accepted
    {
        get
        {
            return IPlayerPrefs.GetBool("PrivacyStatement");
        }

        set
        {
            IPlayerPrefs.SetBool("PrivacyStatement", value);
        }
    }

    private void Start()
    {
        if (!Accepted)
        {
            contentUI.text = content;
            popup.SetActive(true);
        }
    }

    public void OpenLink()
    {
        Application.OpenURL(link);
    }

    public void Accept()
    {
        Accepted = true;
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.1f);
        popup.SetActive(false);
    }
}
