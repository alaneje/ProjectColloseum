using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildManager : MonoBehaviour
{
    [Header("UI Elements")]
    public WeaponNav WeaponsMenu;
    [Header("OnScreen Card")]
    public Text CardName;
    public Text CardAV;
    public Text CardHealth;
    public Text CardWeapon;
    public Text CardMana;
    public Image CardIMG;




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
    }

    void CardUIGenerator()
    {
        CardName.text = Ark.Combatants[CurrentCard].Name;
        CardAV.text = "AV" + Ark.Combatants[CurrentCard].MyStats.ReturnStatAverage();
        CardHealth.text = "" + Ark.Combatants[CurrentCard].MyStats.Health.y;
        CardMana.text = "" + Ark.Combatants[CurrentCard].MyStats.Magic.y;
        CardWeapon.text = "Weapon: " + Ark.WeaponList[Ark.Combatants[CurrentCard].Weapon].WeaponName;
    }

    public void OpenSwapsMenu()
    {
        WeaponsMenu.OpenSwapMenu(CurrentCard);
    }
}
