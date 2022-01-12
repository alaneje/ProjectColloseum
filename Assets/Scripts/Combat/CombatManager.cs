using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public enum TurnPahses {PreTurn,TurnAbility, PostTurn}
    public TurnPahses CurrentTurnPhase;
    public Archive Ark;
    public CombatPlayer Maya;
    public Enemy[] Enemies;
    public CombatantTurnSubmission[] TurnCombatants;
    public int CurrentTarget;
    public int CurrentStackPos;
    public int CurrentTurn;

    [Header("MiscObjects")]
    public GameObject TargetingArrow;
    public GameObject CombatUIPannel;
    public GameObject GameOverScreen;

    [Header("UI Elements")]
    public Text StacksRemainingCountText;
    public Text PlayerHealthText;

    bool InTurnGameplay;
    bool InTurnAbilityLock;
    // Start is called before the first frame update
    void Start()
    {
        GenerateEnemyList();
        GenerateTurnOrder();
        GameObject x = GameObject.Find("Archive");
        Ark = x.GetComponent<Archive>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        CombatUIPannel.SetActive(!InTurnGameplay);

        if (InTurnGameplay) { TurnUpdates(); }

        if (!InTurnGameplay)
        {
            TargettingArrowPositionUpdate();
        }

        CombatUI();
    }

    void CombatUI()
    {
       StacksRemainingCountText.text = "" + (CurrentStackPos + 1) + "/" + (Maya.StackCount + 1);
        PlayerHealthText.text = "Maya: " + Ark.Party[0].Health.x + "/" + Ark.Party[0].Health.y;
    }

    void TurnUpdates()
    {
        if(CurrentTurnPhase == TurnPahses.PreTurn) { EndPreTurnPhase(); }
        if(CurrentTurnPhase == TurnPahses.TurnAbility && !InTurnAbilityLock) { GenerateTurnAbility(); }
    }

    void StartTurns()
    {
        SetEnemyDecisions();//Generate enemy decisions
        CurrentTurn = 0; //Set the current turn to 0.
        GenerateTurnOrder();
        CurrentTurnPhase = TurnPahses.PreTurn;//Set the current turn phase to preturn.
        InTurnGameplay = true;
    }

    void SetEnemyDecisions()
    {
        int i = 0;

        while (i != Enemies.Length)
        {
            Enemies[i].GenerateTurnDecisions();
            i++;
        }//Genereate the turn decisions.
    }

    void EndPreTurnPhase()
    {
        CurrentTurnPhase = TurnPahses.TurnAbility;
    }

    void BeginPostTurn()
    {
        CurrentTurnPhase = TurnPahses.PostTurn;
        int i = 0;
        while (i != Enemies.Length)
        {
            Enemies[i].EndLife();
            i++;
        }//Check if any enemies died this turn. 
        if(Ark.Party[0].Health.x < 1)
        {
            GameOverScreen.SetActive(true);
        }
        else
        {
            EndOfTurn();
        }
        
    }

    void TargettingArrowPositionUpdate()
    {
        TargetingArrow.transform.position = new Vector3(Enemies[CurrentTarget].gameObject.transform.position.x, TargetingArrow.transform.position.y, Enemies[CurrentTarget].gameObject.transform.position.z);

        int i = 0;
        while(i != Maya.Stacks.Length)
        {
            if (!Maya.Stacks[i].Selected)
            {
                Maya.Stacks[i].Target = Enemies[CurrentTarget].gameObject; 
            }
            i++;
        }

      
    }//Updates where the targetting arrow is positioned based on the current target.

    void GenerateEnemyList()
    {
        GameObject[] E = GameObject.FindGameObjectsWithTag("Enemy");
        Enemies = new Enemy[E.Length];
        int i = 0;
        while (i != Enemies.Length)
        {
            Enemies[i] = E[i].GetComponent<Enemy>();
            i++;
        }
    }

    void GenerateTurnOrder()
    {
        TurnCombatants = GameObject.FindObjectsOfType<CombatantTurnSubmission>();
    }//Generates the turn order from the current list of combatants. 

    //ButtonCommands
    public void ChangeTarget(int Direction)
    {
        int x = CurrentTarget + Direction;

        if (x < 0) { x = 0; }
        if (x >= Enemies.Length) { x = Enemies.Length - 1; }

        CurrentTarget = x;
    }

    public void UseAbility(int ButtonNumber)
    {
        Maya.Stacks[CurrentStackPos].AbilityNumber = 0;
        Maya.Stacks[CurrentStackPos].Target = Enemies[CurrentTarget].gameObject;
        Maya.Stacks[CurrentStackPos].Selected = true;

        if (CurrentStackPos == Maya.StackCount)
        {
            StartTurns();
        }
       

        CurrentStackPos++;

        

        



    }

    void GenerateTurnAbility()
    {       
        Instantiate(Ark.CombatAbilities[TurnCombatants[CurrentTurn].AbilityNumber].gameObject, TurnCombatants[CurrentTurn].Target.transform.position,Quaternion.identity);
        InTurnAbilityLock = true;
    }//spawns the ability object on the current target of whoevers turn it is.

    void EndOfTurn()
    {
        if(CurrentTurn != TurnCombatants.Length-1)
        {
            CurrentTurnPhase = TurnPahses.PreTurn;
            CurrentTurn++;
        }
        else
        {
            int i = 0;
            while (i != Maya.Stacks.Length)
            {

                Maya.Stacks[i].Selected = false;
                
                i++;
            }
            CurrentStackPos = 0;
            CurrentTurn = 0;
            GenerateEnemyList();
            InTurnGameplay = false;
        }
    }//At the end of each Turn operates final turn cleanup protocals. 

    public void EndAbilitySegment(int Delay)
    {
        Debug.Log("Pinging end of ability segment with a delay of " + Delay);

        InTurnAbilityLock = false;
        BeginPostTurn();
    }
}
