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
    public InputField Name;

    int CurrentCombatant;
    // Start is called before the first frame update
    void Start()
    {
        GameObject x = GameObject.Find("Archive");
        Ark = x.GetComponent<Archive>();
        CurrentCombatant = 0;
        LoadCharacter();
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

    void LoadCharacter()
    {
        Name.text = Ark.Combatants[CurrentCombatant].Name;
    }

    void SaveCharacter()
    {

    }

    public void StartLoadedGame()
    {
        SceneMaster.LoadScene("Combat");
        SceneMaster.UnloadScene(this.gameObject.scene.name);
    }
}
