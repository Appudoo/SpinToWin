using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnClickHome()
    {
        AdsController.instance.OnShowInterstitialAd();
        SceneManager.LoadScene("Home");
    }
    public void OnClickBack()
    { 
        this.gameObject.SetActive(false);
    }

    public void OnClickPrivacy()
    {
        Application.OpenURL("https://sites.google.com/view/spintowintt/home");
    }
}
