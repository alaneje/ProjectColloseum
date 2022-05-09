using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponNav : MonoBehaviour
{
    public enum Mode{View, Swap, Buy, Sell}
    public Mode CurrentMode;
    [Header("Current Weapon")]
    public GameObject CurrentWeaponBox;
    public Text CurrentWeaponText;
    [Header("WeaponListButtons")]
    public Button[] WeaponButtonList;
    public Text[] WeaponButtonNameList;
    public Text[] WeaponButtonQtyList;
    public Image[] WeaponButtonImgList;
    [Header("Weapon List Navigation")]
    public GameObject NavPannel;
    public Text NavigationPageNum;
    public Button NavigationPrevious;
    public Button NavigationNext;

    [Header("New Weapon")]
    public GameObject NewWeaponBox;
    public Text NewWeaponText;
    [Header("Core Actions")]
    public Button Close;
    public Button CTA;
    public Text CTAT;


    Archive Ark;
    bool ShowCurrentWeapon;
    int ShowCurrentCombatant;
    bool ShowNewWeapon;
    int NewWeapon;
    // Start is called before the first frame update
    void Start()
    {
        GameObject x = GameObject.Find("Archive");
        Ark = x.GetComponent<Archive>();

       

        NavPannel.SetActive(false);//Turns it off until needed
        
    }

    // Update is called once per frame
    void Update()
    {      
            ShowCurrentWUpdate();
            ShowNewWUpdate();

        RenderButtons();
        SetViewRules();

        
       
    }

    void SetViewRules()
    {
        switch(CurrentMode){
            case Mode.View: ShowNewWeapon = true; ShowCurrentWeapon = false; CTAT.text = ""; CTA.interactable = false; break;
            case Mode.Swap: ShowNewWeapon = true; ShowCurrentWeapon = true; CTAT.text = "Swap";CTA.interactable = true; break;
            case Mode.Buy: ShowNewWeapon = true; ShowCurrentWeapon = false;  break;
            case Mode.Sell: ShowNewWeapon = true; ShowCurrentWeapon = false;CTA.interactable = true; CTAT.text = "Sell"; break;
        }
    }

    void ShowCurrentWUpdate()
    {
        CurrentWeaponBox.SetActive(ShowCurrentWeapon);
        if(ShowCurrentWeapon)
        {
            CurrentWeaponText.text = "Current: " + Ark.WeaponList[Ark.Combatants[ShowCurrentCombatant].Weapon].WeaponName;
        }
    }

    void ShowNewWUpdate()
    {
        NewWeaponBox.SetActive(ShowNewWeapon);

        if(ShowNewWeapon){
            NewWeaponText.text = "New: " + Ark.WeaponList[NewWeapon].WeaponName;
        }
    }

    void RenderButtons()
    {
        int i = 0;
        if(CurrentMode == Mode.Buy || CurrentMode == Mode.Sell){i++;}//prevents basic knife showing up in buy and sell.
        while(i != WeaponButtonList.Length)
        {
            if(i < Ark.WeaponList.Length)
            {

                if(CurrentMode == Mode.Swap || CurrentMode == Mode.Sell)
                {
                    if(Ark.WeaponStorage[i] > 0)
                {
                    WeaponButtonList[i].interactable = true;
                }
                else
                {
                    WeaponButtonList[i].interactable = false;
                }
                }

                if(CurrentMode == Mode.View)
                {
                    WeaponButtonList[i].interactable = true;
                }
                
                
                WeaponButtonNameList[i].text = Ark.WeaponList[i].WeaponName;
                if(i !=0)
                {
                    WeaponButtonQtyList[i].text = "x" + Ark.WeaponStorage[i];
                }
                else
                {
                    WeaponButtonQtyList[i].text = "";
                }
                
            }
            else
            {
                WeaponButtonList[i].interactable = false;
                WeaponButtonNameList[i].text = "";
                WeaponButtonQtyList[i].text = "";
            }
            i++;
        }
    }

    public void SelectWeaponFromList(int WButton)
    {
        NewWeapon = WButton;
    }
    public void NavigateBetweenPageS(int Dir){

    }

    public void CallToAction()
    {
        switch(CurrentMode)
        {
            case Mode.Swap: SwapWeapon();CloseMenu(); break;
        }
    }

    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }//Close the menu. 

    public void ViewWeaponDetails()
    {

    }

    void SwapWeapon()
    {
        int Old = Ark.Combatants[ShowCurrentCombatant].Weapon;
        int New = NewWeapon;

        Ark.Combatants[ShowCurrentCombatant].Weapon = New;//Swap the combatants weapon to the new
        if(New != 0)
        {
            Ark.WeaponStorage[New]--;
        }//So long as the new weapon isn't basic knife, remove the new weapon from storage. 

        if(Old != 0)
        {
            Ark.WeaponStorage[Old]++;
        }//So long as the old weapon isn't basic knife, add the new weapon to storage. 
    }

    public void OpenSwapMenu(int CombatantInQuestion)
    {
        CurrentMode = Mode.Swap;
        ShowCurrentCombatant = CombatantInQuestion;
        OpenMenu();
    }

    void OpenMenu()
    {
        NewWeapon = 0;//Reset weapon. 
        this.gameObject.SetActive(true);
    }

}
