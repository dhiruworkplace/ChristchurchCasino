using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if  UNITY_IPHONE
using UnityEngine.iOS;
#endif

public class RateMePopup : MonoFrame
{
    public void Rate()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
#elif UNITY_IPHONE
        UnityEngine.iOS.Device.RequestStoreReview();
#endif
        OnCloseButtonClicked();
    }
}
