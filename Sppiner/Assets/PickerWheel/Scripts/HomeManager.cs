using DG.Tweening;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class HomeManager : MonoBehaviour
{
    //public GameObject homeScreen;
    public static HomeManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(OnGetUpi());
        if (!PlayerPrefs.HasKey("Email"))
        {
            Ui_manager.Instance.EnableSinglePanels(StatePanel.Splash);
        }
        else
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    public IEnumerator OnGetUpi()
    {

        WWWForm form = new WWWForm();

        form.AddField("request_for_ludoking_wenbyjalpa121", "get_upi_detail");


        using (var www = UnityWebRequest.Post(DataManager.baseurl, form))
        {
            print("Hello");
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                print("Error:::::::" + www.error);
            }
            else
            {
                print("Finished Uploading Screenshot" + www.downloadHandler.text);
                JSONNode data = JSON.Parse(www.downloadHandler.text);
                string upiId1 = data["Model_Upi"][0]["upi_id"];
                string upiId2 = data["Model_Upi"][1]["upi_id"];

                DataManager.upiId1 = upiId1;
                DataManager.upiId2 = upiId2;

                PlayerPrefs.SetString("UpiId", DataManager.upiId1);
            }
        }
    }
}
