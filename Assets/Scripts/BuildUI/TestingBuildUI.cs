using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingBuildUI : MonoBehaviour
{
    Archive Ark;
    [Header("Team Section - Navigation")]
    public Text CurrentCombatantNumberDisplayText;
    public Button[] CombatantNavigationButtons;
    public Button[] NewNavButtons;
    public Text NewNavText;
    public Text CurrentNewText;
    public InputField Name;
    public Text StatsT;
    public Text OtherDetailsT;
    public GameObject NewPannel;

    int CurrentCombatant;
    int CurrentNew;

    // Start is called before the first frame update
    void Start()
    {
        GameObject x = GameObject.Find("Archive");
        Ark = x.GetComponent<Archive>();
        CurrentCombatant = 0;
        CurrentNew = 0;
        LoadCharacter();
        LoadType();
    }

    // Update is called once per frame
    void Update()
    {
        NavigationDisplayUpdate();
    }

    void NavigationDisplayUpdate()
    {
        if(CurrentCombatant == 0) { CombatantNavigationButtons[1].interactable = false; } else { CombatantNavigationButtons[1].interactable = true; }
        if(CurrentCombatant == (Ark.Combatants.Length - 1)) { CombatantNavigationButtons[0].interactable = false; } else { CombatantNavigationButtons[0].interactable = true; }
        CurrentCombatantNumberDisplayText.text = "" + CurrentCombatant;
    }

    

    public void ChangeNavigation(bool Forward)
    {
        SaveCharacter();
        if (Forward) { CurrentCombatant++; } else
        {
            CurrentCombatant--;
        }
        LoadCharacter();
    }

    public void ChangeNavNew(bool Forward)
    {
        if (Forward) { CurrentNew++; }
        else
        {
            CurrentNew--;
        }
        LoadType();
    }

    void LoadType()
    {
            if (CurrentNew == 0) { NewNavButtons[1].interactable = false; } else { NewNavButtons[1].interactable = true; }
            if (CurrentNew == (Ark.CombatantsList.Length - 1)) { NewNavButtons[0].interactable = false; } else { NewNavButtons[0].interactable = true; }
            if(CurrentNew != 0)
        {
            NewNavText.text = "" + CurrentNew + " | " + Ark.CombatantsList[CurrentNew].Name;

            string T = "";
            T += Ark.CombatantsList[CurrentNew].Name + "\n";
            T +="Stat group: " +  Ark.CombatantsList[CurrentNew].StatGroup;
            CurrentNewText.text = T;
        }
        else
        {
            NewNavText.text = "NONE.";
            CurrentNewText.text = "NONE";
        }
           

            
        
    }

    void LoadCharacter()
    {
        Name.text = Ark.Combatants[CurrentCombatant].Name;

        string x = "";
        x += "Health: " + Ark.Combatants[CurrentCombatant].MyStats.Health.y + "\n";
        x += "Magic: " + Ark.Combatants[CurrentCombatant].MyStats.Magic.y + "\n";
        x += "Attack: " + Ark.Combatants[CurrentCombatant].MyStats.Attack + "\n";
        x += "Defence: " + Ark.Combatants[CurrentCombatant].MyStats.Defence + "\n";
        x += "Resonance: " + Ark.Combatants[CurrentCombatant].MyStats.Resonance + "\n";
        x += "Constitution: " + Ark.Combatants[CurrentCombatant].MyStats.Constitution + "\n";
        x += "Speed: " + Ark.Combatants[CurrentCombatant].MyStats.Speed + "\n";
        x += "Accurancy: " + Ark.Combatants[CurrentCombatant].MyStats.Accuracy + "\n";
        x += "Evasion: " + Ark.Combatants[CurrentCombatant].MyStats.Evasion;

        StatsT.text = x;

        string Y = "";
        int i = 0;
        while(i != Ark.Combatants[CurrentCombatant].PositiveAttribute.Length)
        {
            Y += "Positive Attribute: " + Ark.Combatants[CurrentCombatant].PositiveAttribute[i].ToString() + "\n";
            i++;
        }
        i = 0;
        while (i != Ark.Combatants[CurrentCombatant].NegativeAttribute.Length)
        {
            Y += "Negative Attribute: " + Ark.Combatants[CurrentCombatant].NegativeAttribute[i].ToString() + "\n";
            i++;
        }
        i = 0;
        while (i != Ark.Combatants[CurrentCombatant].SkillList.Length)
        {
            Y +=  "[SKILL] " + Ark.CombatAbilities[Ark.Combatants[CurrentCombatant].SkillList[i]].AbilityName + "\n";
            i++;
        }

        OtherDetailsT.text = Y;

    }

    void SaveCharacter()
    {

    }

    public void StartLoadedGame()
    {
        SceneMaster.LoadScene("Combat");
        SceneMaster.UnloadScene(this.gameObject.scene.name);
    }

    public void GenerateNew(bool Generate)
    {
        if(Generate) { GenerateNewChar(); }
        NewPannel.SetActive(false);
    }

    public void OpenGenerate()
    {
        NewPannel.SetActive(true);
    }

    void GenerateNewChar()
    {
        Ark.Combatants[CurrentCombatant].PositiveAttribute = Ark.CombatantsList[CurrentNew].PositiveAttribute;
        Ark.Combatants[CurrentCombatant].NegativeAttribute = Ark.CombatantsList[CurrentNew].NegativeAttribute;
        Ark.Combatants[CurrentCombatant].Name = Ark.CombatantsList[CurrentNew].Name;
        Stats X = new Stats();
        Ark.Combatants[CurrentCombatant].MyStats = X.ReturnRandomisedStats(Ark.CombatantsList[CurrentNew].StatGroup);

        LoadCharacter();
        
    }
}
