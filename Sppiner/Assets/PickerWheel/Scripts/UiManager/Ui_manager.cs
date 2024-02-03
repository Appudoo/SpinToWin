using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StatePanel
{
    Home,
    Login,
    GamePlay,
    Splash,

}

public class Ui_manager : MonoBehaviour
{
    public static Ui_manager Instance;
    

    public List<PanelManage> Panel = new List<PanelManage>();
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

    }


    public void EnableSinglePanels(StatePanel CurrentPanel)
    {
        foreach (var item in Panel)
            item.Panel.SetActive(false);

        Panel.Find(x => x.Sp == CurrentPanel).Panel.SetActive(true);
       
    }
    public void DisableAllPanel()
    {
        foreach (var item in Panel)
            item.Panel.SetActive(false);
        //if (PlayerPrefs.GetInt("Scound") == 2)
        //{
        //    FindObjectOfType<Music_Controol>().PlaySound("Click");
        //}
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
[System.Serializable]
public class PanelManage
{
    public GameObject Panel;
    public StatePanel Sp;
}