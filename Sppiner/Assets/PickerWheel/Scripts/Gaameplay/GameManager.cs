using DG.Tweening;
using EasyUI.PickerWheelUI;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button uiSpinButton;
    [SerializeField] private Text uiSpinButtonText;

    [SerializeField] private PickerWheel pickerWheel;

    public  int Spincount=10;
    public  int Withdrawaltotal;

    public Text Spincountxt;
    public Text Withdrawaltxt;

    public GameObject WarningPopup,CheckInternet;


    public GameObject MenuPanel;
    public GameObject AddSpinPanel;
    public GameObject WithdrawPanel;
   
    public static GameManager Instance;

    public GameObject WinScreen;
    public GameObject LossScreen;
    public Text Wintxt;
        
    public GameObject AddMoneybtn, Withdrawalbtn;
    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        //AdsController.instance.AdsSetup();
        if(!PlayerPrefs.HasKey("spin"))
            PlayerPrefs.SetInt("spin",5);
        Spincount = PlayerPrefs.GetInt("spin");
        Debug.Log("--------" + Spincount);
        Spincountxt.text = Spincount.ToString();
    }

    private void OnEnable()
    {
        if(DataManager.islive!=0)
        {
            //AddMoneybtn.SetActive(true);
            //Withdrawalbtn.SetActive(true);
        }
        else
        {
            //AddMoneybtn.SetActive(false);
            //Withdrawalbtn.SetActive(false);
        }

        Spincount = PlayerPrefs.GetInt("spin");
        Withdrawaltotal = PlayerPrefs.GetInt("Withdrawal",0);

        Spincountxt.text = Spincount.ToString();
        Debug.Log("------------------" + PlayerPrefs.GetInt("spin"));

        Withdrawaltxt.text = Withdrawaltotal.ToString()+ "₹";
    }
    public void OnClickSpinNow()
    {
        Time.timeScale = 1; 
        if(Spincount<=0)
        {
            WarningPopup.transform.localScale = new Vector2(0, 0);
            WarningPopup.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
            WarningPopup.SetActive(true);
            DOTween.Sequence().AppendInterval(2f).OnComplete(() => {
                WarningPopup.SetActive(false);
            });
        }
        else
        {
            Spincount--;
            PlayerPrefs.SetInt("spin", Spincount);

            OnUpdateSpin(-1);
            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "Spinning";

            pickerWheel.OnSpinEnd(wheelPiece => {
                Debug.Log(

                   @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
                   + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
                );
                WinAndLossManage(wheelPiece.Amount);
                Withdrawaltotal += wheelPiece.Amount;
                PlayerPrefs.SetInt("Withdrawal",Withdrawaltotal);
                Withdrawaltxt.text = Withdrawaltotal.ToString()+ "₹";

                uiSpinButton.interactable = true;
                uiSpinButtonText.text = "Spin";
            });

            pickerWheel.Spin();
        }
        FireBaseSetting.fb.CustomEventManager("Spin Button", "SpinNow", Spincount);
        Debug.Log("spin button");

    }

    void WinAndLossManage(int Amount)
    {
        if (Amount > 0)
        {
            WinScreen.transform.GetChild(0).transform.localScale = new Vector3(0,0,0);
            WinScreen.transform.GetChild(0).transform.DOScale(new Vector3(1f,1f,1f),0.5f);
            WinScreen.SetActive(true);
            Wintxt.text = "Hurry you won "+Amount+"Rs.";
        }
        else
        {
            LossScreen.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 0);
            LossScreen.transform.GetChild(0).transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
            LossScreen.SetActive(true);
        }

        DOTween.Sequence().AppendInterval(2f).OnComplete(() => {
            WinScreen.SetActive(false);
            LossScreen.SetActive(false);
        });
    }

    public void OnClickMenu()
    {
        MenuPanel.SetActive(true);
    }

    public void OnClickAddSpin()
    { 
        AddSpinPanel.SetActive(true);
        FireBaseSetting.fb.CustomEventManager("OnClickAddSpin", "Add Spin", 0);
        Debug.Log("onclickaddspin");


    }
    public void OnClickWithdrawSpin()
    {
        WithdrawPanel.SetActive(true);
        FireBaseSetting.fb.CustomEventManager("OnClickWithdrawSpin","withdrow",0 );
        Debug.Log("OnClickWithdrawSpin");


    }
    public void OnClickWithdrawtoHome()
    {
        WithdrawPanel.SetActive(false);
    }


    public void OnUpdateSpin(int addspin)
    {
        Spincountxt.text = Spincount.ToString();

        //WWWForm form = new WWWForm();

        //form.AddField("request_for_ludoking_wenbyjalpa121", "add_totspin");
        //form.AddField("tot_spin", addspin.ToString());
        //form.AddField("userid", DataManager.id);

        //print("Id::::::::::::::::"+DataManager.id);
        //using (var www = UnityWebRequest.Post(DataManager.baseurl, form))
        //{
        //yield return www.SendWebRequest();
        //    if (www.isNetworkError || www.isHttpError)
        //    {
        //        CheckInternet.SetActive(true);
        //        CheckInternet.transform.GetChild(0).transform.GetComponent<Text>().text = www.error;
        //        Debug.Log(www.error);
        //        DOTween.Sequence().AppendInterval(2f).OnComplete(() => {
        //            CheckInternet.SetActive(false);
        //        });

        //    }
        //    else
        //    {
        //        print("Finished Uploading Screenshot" + www.downloadHandler.text);
        //        JSONNode data = JSONNode.Parse(www.downloadHandler.text);
        //        PlayerPrefs.SetString("data", www.downloadHandler.text);

        //        string tot_spin = data["tot_spin"];


        //        DataManager.tot_spin = int.Parse(tot_spin);
        //        print("Spintot:::::" + DataManager.tot_spin);

        //        Spincountxt.text = DataManager.tot_spin.ToString();

        //    }
        //}
    }
}
