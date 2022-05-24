using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildManager : MonoBehaviour
{
    [Header("UI Elements")]
    public WeaponNav WeaponsMenu;
    public CombatantListPannel combatantListPannel;
    [Header("Screens")]
    public GameObject CardCurrentCombatantScreen;
    [Header("OnScreen Card")]
    public Text CardName;
    public Text CardAV;
    public Text CardHealth;
    public Text CardWeapon;
    public Text CardMana;
    public Image CardIMG;
    public Button PreviousCombatant;
    public Button NextCombatant;
    




    int CurrentCard;
    Archive Ark;
    // Start is called before the first frame update
    void Start()
    {
        GameObject x = GameObject.Find("Archive");
        Ark = x.GetComponent<Archive>();
    }

    // Update is called once per frame
    void Update()
    {
    CardUIGenerator();   
    CombatUIDirectionArrowUpdate(); 
    }

    void CardUIGenerator()
    {
        CardName.text = Ark.Combatants[CurrentCard].Name;
        CardAV.text = "AV" + Ark.Combatants[CurrentCard].MyStats.ReturnStatAverage();
        CardHealth.text = "" + Ark.Combatants[CurrentCard].MyStats.Health.y;
        CardMana.text = "" + Ark.Combatants[CurrentCard].MyStats.Magic.y;
        CardWeapon.text = "Weapon: " + Ark.WeaponList[Ark.Combatants[CurrentCard].Weapon].WeaponName;
    }

    void CombatUIDirectionArrowUpdate()
    {
        if(CurrentCard == 0){PreviousCombatant.interactable = false; NextCombatant.interactable = true;}
        if(CurrentCard == (Ark.Combatants.Length -1)){PreviousCombatant.interactable = true; NextCombatant.interactable = false;}
        if(CurrentCard > 0 && CurrentCard < (Ark.Combatants.Length - 2)){PreviousCombatant.interactable = true; NextCombatant.interactable = true;}

    }//conditions for buttons in the list

    public void ChangeCombatant(int Direction)
    {
            CurrentCard += Direction;
    }

    public void OpenSwapsMenu()
    {
        WeaponsMenu.OpenSwapMenu(CurrentCard);
    }
    public void GetCombatantFromScrren()
    {
        CurrentCard = combatantListPannel.SelectedCombatant;
    }
    public void ChangeCurrentCombatantMenuState(bool State)
    {
        CardCurrentCombatantScreen.SetActive(State);
    }
}
