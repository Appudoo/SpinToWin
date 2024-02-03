using DG.Tweening;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class Splashscreen : MonoBehaviour
{
    public GameObject Updatepoup;

    private void Start()
    {
        //StartCoroutine(OnSendRegistion());

    }
    public void OnClickGuestLogin()
    {
        if (PlayerPrefs.HasKey("Email"))
        {
            Ui_manager.Instance.EnableSinglePanels(StatePanel.Home);

        }
        else
        {
            Ui_manager.Instance.EnableSinglePanels(StatePanel.Login);
        }
        FireBaseSetting.fb.CustomEventManager("Quick Button", "QuickLogin", 12);
    }


    //public IEnumerator OnSendRegistion()
    //{
    //    WWWForm form = new WWWForm();

    //    form.AddField("request_for_ludoking_wenbyjalpa121", "api_splash");

    //    using (var www = UnityWebRequest.Post(DataManager.baseurl, form))
    //    {
    //        yield return www.SendWebRequest();
    //        if (www.isNetworkError || www.isHttpError)
    //        {
    //            print("Eror::::::::::"+www.error);
    //            if (PlayerPrefs.GetString("Email") != "")
    //            {
    //                StartCoroutine(OnSendRegistion(PlayerPrefs.GetString("Email")));
    //            }
    //        }
    //        else
    //        {
    //            print("Finished Uploading Screenshot" + www.downloadHandler.text);

    //            JSONNode datas = JSON.Parse(www.downloadHandler.text);
    //            string idlive = datas["id"];
    //            string live = datas["version"];

    //            DataManager.islive = int.Parse(idlive);

    //            if(Application.version==live)
    //            {
    //                if (PlayerPrefs.GetString("Email") != "")
    //                {

    //                    StartCoroutine(OnSendRegistion(PlayerPrefs.GetString("Email")));

    //                }
    //            }

    //            else
    //            {
    //                Updatepoup.SetActive(true);
    //            }
    //        }


    //    }
    //}


    //public IEnumerator OnSendRegistion(string email)
    //{
    //    WWWForm form = new WWWForm();

    //    form.AddField("request_for_ludoking_wenbyjalpa121", "register_login");
    //    form.AddField("mail", email);
    //    form.AddField("device_token", SystemInfo.deviceUniqueIdentifier);


    //    using (var www = UnityWebRequest.Post(DataManager.baseurl, form))
    //    {
    //        yield return www.SendWebRequest();
    //        if (www.isNetworkError || www.isHttpError)
    //        {

    //        }
    //        else
    //        {
    //            StartCoroutine(HomeManager.instance.OnGetUpi());
    //            Ui_manager.Instance.EnableSinglePanels(StatePanel.Home);
    //            print("Finished Uploading Screenshot" + www.downloadHandler.text);
    //            JSONNode data = JSONNode.Parse(www.downloadHandler.text);
    //            PlayerPrefs.SetString("data", www.downloadHandler.text);
    //            string id = data["id"];
    //            string mail = data["mail"];
    //            string device_token = data["device_token"];
    //            string tot_spin = data["tot_spin"];
    //            string tot_withdraw = data["tot_withdraw"];


    //            DataManager.id = id;
    //            DataManager.mail = mail;
    //            DataManager.device_token = device_token;
    //            DataManager.tot_spin = int.Parse(tot_spin);
    //            DataManager.tot_withdraw = tot_withdraw;

    //            //StartCoroutine(HomeManager.instance.OnGetUpi());

    //            Ui_manager.Instance.EnableSinglePanels(StatePanel.Home);
    //        }
    //    }
    //}


    public void OnClickPlaystore()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }

}
