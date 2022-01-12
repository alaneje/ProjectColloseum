using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public enum TurnPahses {PreTurn,TurnAbility, PostTurn}
    public TurnPahses CurrentTurnPhase;
    public Archive Ark;
    public CombatPlayer[] PlayerCombatants;
    public Enemy[] Enemies;
    public CombatantTurnSubmission[] TurnCombatants;
    public int CurrentPlayer;
    public int CurrentTarget;
   // public int CurrentStackPos;
    public int CurrentTurn;

    [Header("MiscObjects")]
    public GameObject TargetingArrow;
    public GameObject CombatUIPannel;
    public GameObject GameOverScreen;

    [Header("UI Elements")]
   // public Text StacksRemainingCountText;
    public Text PlayerHealthText;

    bool InTurnGameplay;
    bool InTurnAbilityLock;
    // Start is called before the first frame update
    void Start()
    {
        GenerateEnemyList();
        GeneratePlayerList();
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
        PlayerHealthText.text = "Maya: " + PlayerCombatants[CurrentPlayer].MyProfile.Health.x + "/" + PlayerCombatants[CurrentPlayer].MyProfile.Health.y;
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

        GameObject[] E = GameObject.FindGameObjectsWithTag("Player");

        if (E.Length < 1)
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
        PlayerCombatants[CurrentPlayer].Submission.Target = Enemies[CurrentTarget].gameObject; 
     

      
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

    void GeneratePlayerList()
    {
        GameObject[] E = GameObject.FindGameObjectsWithTag("Player");
        PlayerCombatants = new CombatPlayer[E.Length];
        int i = 0;
        while (i != PlayerCombatants.Length)
        {
            PlayerCombatants[i] = E[i].GetComponent<CombatPlayer>();
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
        PlayerCombatants[CurrentPlayer].Submission.AbilityNumber = 0;
        PlayerCombatants[CurrentPlayer].Submission.Target = Enemies[CurrentTarget].gameObject;

        if((CurrentPlayer + 1) == PlayerCombatants.Length)
        {
            StartTurns();
        }
        else
        {
            CurrentPlayer++;
        }
      //  Maya.Stacks[CurrentStackPos].Selected = true;
        

    }

    void GenerateTurnAbility()
    {       
        CombatAbilityProfile A = Instantiate(Ark.CombatAbilities[TurnCombatants[CurrentTurn].AbilityNumber].gameObject, TurnCombatants[CurrentTurn].Target.transform.position,Quaternion.identity).GetComponent<CombatAbilityProfile>();
        A.PhysicalAttack = TurnCombatants[CurrentTurn].gameObject.GetComponent<CombatProfile>().Attack;
        A.MagicAttack = TurnCombatants[CurrentTurn].gameObject.GetComponent<CombatProfile>().Resonance;

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
            
           
            CurrentTurn = 0;
            CurrentPlayer = 0;
            GenerateEnemyList();
            GeneratePlayerList();
            CurrentTarget = 0;
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
