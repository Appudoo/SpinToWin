using UnityEngine;
//using UnityEngine.iOS;        // TODO uncomment this while making build for ios device

public class RateAndShare : MonoBehaviour
{

    public void RateButton()    
    {
        //AudioManager.Instance.PlayBtnClickSound();

#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
#elif UNITY_IOS
        Device.RequestStoreReview();
#endif
    }



    public void ShareButton()
    {
        //AudioManager.Instance.PlayBtnClickSound();
        new NativeShare().SetTitle("Share");
        new NativeShare().SetSubject("Share It").SetText("Share app & support us").Share();    // Download & Import Native Share Package from Assets Store
    }


}   // Class End
