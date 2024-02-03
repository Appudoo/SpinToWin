using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class AddSpin_Manager : MonoBehaviour
{
    public List<packagedetail> Packagedetails;
    public List<GameObject> buttons;

    public int HowmanySpin;
    public int price;
    public int off;

    public Text submitbutton;
    public Color darkwhite;

    public GameObject ChoicePaymentPanel;

    public static AddSpin_Manager instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        //OnClickSpinPlan(1);
    }
    public async  void OnclickBtn(int Index)
    {
        AdsController.instance.ShowRewardedAd();
        buttons[Index].GetComponentInChildren<Slider>().value +=1;
        await Task.Delay(1500);

        if (buttons[Index].GetComponentInChildren<Slider>().value == buttons[Index].GetComponentInChildren<Slider>().maxValue)
        {     
            GameManager.Instance.Spincount += int.Parse(buttons[Index].GetComponentInChildren<Slider>().value.ToString());
            GameManager.Instance.Spincountxt.text = GameManager.Instance.Spincount.ToString();
            PlayerPrefs.SetInt("spin", GameManager.Instance.Spincount);
            
            buttons[Index].GetComponentInChildren<Slider>().value = 0;
        }
        FireBaseSetting.fb.CustomEventManager("Get Spin Button", "GetSpin", Index);

    }
    public void OnClickSpinPlan(int Index,int amount)
    {


        //foreach (var packagebutton in buttons)
        //{
        //    packagebutton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
        //    packagebutton.transform.GetComponent<Image>().color = darkwhite;
        //}
        //buttons[Index].transform.DOScale(new Vector3(1.1f,1.1f,1.1f),0.2f);
        //buttons[Index].transform.GetComponent<Image>().color = Color.yellow;

        // HowmanySpin =  Packagedetails[Index].HowmanySpin;
        // price =  Packagedetails[Index].price;
        // off =  Packagedetails[Index].off;

        //submitbutton.text = "BUY " + HowmanySpin + " SPIN";
    }

    public void OnClickSubmit()
    {
        DataManager.Spinadd = HowmanySpin;
        DataManager.Amount = price;
        ChoicePaymentPanel.SetActive(true);
    }
    public void OnClickClose()
    {
        AdsController.instance.OnShowInterstitialAd();
        GameManager.Instance.AddSpinPanel.SetActive(false);
        FireBaseSetting.fb.CustomEventManager("OnClickClose", "close", 0);

    }


    public void OnclickOpenAddScreenPanel()
    {
        ChoicePaymentPanel.SetActive(false);
    }

    //public void OnClickChoiceUpi(int number)
    //{ 
    //    if(number==0)
    //    {
    //        DataManager.Upi = DataManager.upiId1;
    //    }
    //    else
    //    {
    //        DataManager.Upi = DataManager.upiId2;
    //    }
    //    print("Upi:::"+DataManager.Upi);
    //    print("Amount:::"+DataManager.Amount);

    //    PaymentManager.Instance.payyyy();
       
    //}


    public void OnSuccesPayment()
    {
        //StartCoroutine(GameManager.Instance.OnUpdateSpin(DataManager.Spinadd));
        OnclickOpenAddScreenPanel();
        OnClickClose();
    }
}

[System.Serializable]
public class packagedetail
{
    public int HowmanySpin;
    public int price;
    public int off;
}
