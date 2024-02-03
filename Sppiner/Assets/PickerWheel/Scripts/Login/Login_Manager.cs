using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using SimpleJSON;

using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class Login_Manager : MonoBehaviour
{
    public InputField EmailInp;
    public GameObject WarningPopup;

    public static Login_Manager instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {

    }
    public void OnClickSubmit()
    {

        if (EmailInp.text.Length <= 0)
        {
            WarningPopup.transform.localScale = new Vector2(0, 0);
            WarningPopup.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
            WarningPopup.SetActive(true);
            DOTween.Sequence().AppendInterval(2f).OnComplete(() =>
            {
                WarningPopup.SetActive(false);
            });
        }
        else
        {
            PlayerPrefs.SetString("Email", EmailInp.text);
            //Ui_manager.Instance.EnableSinglePanels(StatePanel.GamePlay);
            SceneManager.LoadScene("Gameplay");

        }
    }

}

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
//            WarningPopup.SetActive(true);
//            WarningPopup.transform.GetChild(0).transform.GetComponent<Text>().text = www.error;
//            Debug.Log(www.error);
//            DOTween.Sequence().AppendInterval(2f).OnComplete(() => {
//                WarningPopup.SetActive(false);
//            });

//        }
//        else
//        {
//            print("Finished Uploading Screenshot" + www.downloadHandler.text);
//            JSONNode data = JSONNode.Parse(www.downloadHandler.text);
//            PlayerPrefs.SetString("data",www.downloadHandler.text);
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

//            print("Id================>"+DataManager.id+"<===============");
//            print("Id================>"+DataManager.mail+"<===============");
//            print("Id================>"+DataManager.device_token+"<===============");
//            print("Id================>"+DataManager.tot_spin+"<===============");
//            print("Id================>"+DataManager.tot_withdraw+"<===============");


//            StartCoroutine(HomeManager.instance.OnGetUpi());
//            Ui_manager.Instance.EnableSinglePanels(StatePanel.Home);

//        }
//    }
//}




