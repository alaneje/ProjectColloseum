using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CombatantListPannel : MonoBehaviour
{

    Archive Ark;
    [Header("Public Details")]
    public GameObject MyScreen;
    public UnityEvent Activate;
    public UnityEvent OnClose;
    public bool CloseScreenOnActivate;
    public bool Canbeclosed;
    public int SelectedCombatant;
    [Header("UI Elements")]
    public Button[] Buttons;
    public Text[] ButtonText;
    public Image[] ButtonImage;
    public Button[] NavigationOptions;
    public Button Close;

    int Page;
    int Pagecount = 10;
    // Start is called before the first frame update
    void Start()
    {
        GameObject x = GameObject.Find("Archive");
        Ark = x.GetComponent<Archive>();
    }

    // Update is called once per frame
    void Update()
    {
            RenderButtons();
            Close.gameObject.SetActive(Canbeclosed);
    }

    public void ClickCombatant(int ButtonNumber)
    {
        int p = Pagecount * Page;
        SelectedCombatant = ButtonNumber + p;
        Activate.Invoke();//any script added to Activate will ... ya know, activate. 
        if(CloseScreenOnActivate)
        {
            CloseScreen();
        }
    }

    void RenderButtons()
    {
        int i = 0;
        int p = Pagecount * Page;
        while(i != Buttons.Length)
        {
            if(Ark.Combatants.Length >= i + p && Ark.Combatants[i + p].Name != "")
            {
                
                Buttons[i].interactable = true;
                ButtonImage[i].gameObject.SetActive(true);
                ButtonText[i].text = Ark.Combatants[i + p].Name;
            }
            else
            {
                ButtonImage[i].gameObject.SetActive(false);
                ButtonText[i].text = "";
                Buttons[i].interactable = false;
            }
            i++;
        }
    }

    public void Navigation(int Direction)
    {
        
    }
    public void CloseScreen()
    {
        OnClose.Invoke();
        MyScreen.SetActive(false);
    }
}
