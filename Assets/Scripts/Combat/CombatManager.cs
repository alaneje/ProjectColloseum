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
    public GameObject VictoryScreen;

    [Header("UI Elements")]
   // public Text StacksRemainingCountText;
    public Text PlayerHealthText;
    public Button[] AbilityButtons;
    public Text[] AbilityButtonText;

    bool InTurnGameplay;
    bool InTurnAbilityLock;

    int EnemiesBuilt;
    GameObject[] StoredList;
    int Wavecount;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneMaster.SetActiveScene(this.gameObject.scene);
        GameObject x = GameObject.Find("Archive");
        Ark = x.GetComponent<Archive>();
        BuildPlayerCombatants();
        GeneratePlayerList();
        EnemyCreator();
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
        string Name = PlayerCombatants[CurrentPlayer].MyProfile.Name;
        PlayerHealthText.text = Name + PlayerCombatants[CurrentPlayer].MyProfile.Health.x + "/" + PlayerCombatants[CurrentPlayer].MyProfile.Health.y;


        int i = 0;
        while(i != AbilityButtons.Length)
        {
            if(i !<= PlayerCombatants[CurrentPlayer].MyProfile.SkillList.Length - 1)
            {
                int A = PlayerCombatants[CurrentPlayer].MyProfile.SkillList[i];//Get ability number from the list
                AbilityButtons[i].interactable = true;
                AbilityButtonText[i].text = Ark.CombatAbilities[A].AbilityName;
            }
            else
            {
                AbilityButtons[i].interactable = false;
                AbilityButtonText[i].text = "";
            }
            i++;
        }
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
        GameObject[] P = GameObject.FindGameObjectsWithTag("Enemy");
        if (E.Length < 1)
        {
            
            GameOverScreen.SetActive(true);
        }
        else if (P.Length < 1)
        {
            CombatScenario CS = Ark.gameObject.GetComponent<CombatScenario>();
            if (EnemiesBuilt > CS.EnemyCombatants.Length - 1)
            {
                VictoryScreen.SetActive(true);
            }
            else
            {
                Debug.Log("HITTING THE NEXT ENEMY WAVE BUILDING PROCESS.");
                int ii = 0;
                while(ii != StoredList.Length)
                {
                    StoredList[ii].SetActive(true);
                    ii++;
                }//Revive all old gameobjects.
                EnemyCreator();
                EndOfTurn();
            }
            
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

    void EnemyCreator()
    {
        BuildEnemyCombatants();
        GenerateEnemyList();
        GenerateTurnOrder();
    }

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
        A.AttributesCarried = TurnCombatants[CurrentTurn].gameObject.GetComponent<CombatProfile>().PositiveAttribute;

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

    void BuildEnemyCombatants()
    {
        Wavecount++;//Increase Wave count. 
        CombatScenario CS = Ark.gameObject.GetComponent<CombatScenario>();
        GameObject[] E = GameObject.FindGameObjectsWithTag("Enemy");
        if(EnemiesBuilt == 0)
        {
            StoredList = E;
            Debug.Log("All Enemy objects dynamically stored in backup list. ");
        }
        Debug.Log("Total Enemy Objects: " + E.Length);
        int i = 0;
        while(i != E.Length)
        {
           // if (EnemiesBuilt == CS.EnemyCombatants.Length) { EnemiesBuilt = CS.EnemyCombatants.Length; break; }//error fix
            if (EnemiesBuilt + 1 !<= CS.EnemyCombatants.Length)
            {
                E[i].gameObject.SetActive(true);
                CombatProfile CX = E[i].GetComponent<CombatProfile>();
                CX.Name = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Name;
                CX.Health = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Health;
                CX.Magic = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Magic;
                CX.Attack = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Attack;
                CX.Resonance = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Resonance;
                CX.Defence = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Defence;
                CX.Constitution = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Constitution;
                CX.Evasion = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Evasion;
                CX.Accuracy = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Accuracy;
                CX.PositiveAttribute = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].PositiveAttribute;
                CX.NegativeAttribute = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].NegativeAttribute;
                CX.SkillList = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].SkillList;
                EnemiesBuilt++;

            }
            else
            {
                E[i].gameObject.SetActive(false);
            }
            
            
            i++;
        }
    }//Build the enemy combatants from the scenario

    void BuildPlayerCombatants()
    {
        CombatScenario CS = Ark.gameObject.GetComponent<CombatScenario>();
        GameObject[] E = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;
        while (i != E.Length)
        {
            if (i + 1! <= CS.PlayerCombatants.Length)
            {
                E[i].gameObject.SetActive(true);
                CombatProfile CX = E[i].GetComponent<CombatProfile>();
                CX.Name = Ark.Combatants[CS.PlayerCombatants[i]].Name;
                CX.Health = Ark.Combatants[CS.PlayerCombatants[i]].Health;
                CX.Magic = Ark.Combatants[CS.PlayerCombatants[i]].Magic;
                CX.Attack = Ark.Combatants[CS.PlayerCombatants[i]].Attack;
                CX.Resonance = Ark.Combatants[CS.PlayerCombatants[i]].Resonance;
                CX.Defence = Ark.Combatants[CS.PlayerCombatants[i]].Defence;
                CX.Constitution = Ark.Combatants[CS.PlayerCombatants[i]].Constitution;
                CX.Evasion = Ark.Combatants[CS.PlayerCombatants[i]].Evasion;
                CX.Accuracy = Ark.Combatants[CS.PlayerCombatants[i]].Accuracy;
                CX.PositiveAttribute = Ark.Combatants[CS.PlayerCombatants[i]].PositiveAttribute;
                CX.NegativeAttribute = Ark.Combatants[CS.PlayerCombatants[i]].NegativeAttribute;
                CX.SkillList = Ark.Combatants[CS.PlayerCombatants[i]].SkillList;

            }
            else
            {
                E[i].gameObject.SetActive(false);
            }
            i++;
        }
    }//Build the player combatants from the scenario


    public void EndAbilitySegment(int Delay)
    {
        Debug.Log("Pinging end of ability segment with a delay of " + Delay);

        InTurnAbilityLock = false;
        BeginPostTurn();
    }

    public int GetEnemiesBuilt()
    {
        return EnemiesBuilt;
    }

}
