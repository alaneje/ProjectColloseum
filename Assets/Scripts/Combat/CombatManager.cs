using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CombatManager : MonoBehaviour
{
    public enum TurnPahses {PreTurn,TurnAbility, PostTurn}
    public TurnPahses CurrentTurnPhase;
    public Grid TheGrid;
    public Tilemap tilemap;
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
    public Button[] MovementButtons;

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
        /*
        CombatUIPannel.SetActive(!InTurnGameplay);

        if (InTurnGameplay) { TurnUpdates(); }

        if (!InTurnGameplay)
        {
            TargettingArrowPositionUpdate();
        }*/

        CombatUI();
        MovementButtonUpdates();
    }

    void CombatUI()
    {
        string Name = PlayerCombatants[CurrentPlayer].MyProfile.Name;
        PlayerHealthText.text = Name + PlayerCombatants[CurrentPlayer].MyProfile.MyStats.Health.x + "/" + PlayerCombatants[CurrentPlayer].MyProfile.MyStats.Health.y;


        int i = 0;
        while(i != AbilityButtons.Length)
        {
            if(i !<= Ark.WeaponList[PlayerCombatants[CurrentPlayer].MyProfile.Weapon].SkillList.Length - 1)
            {
                int A = Ark.WeaponList[PlayerCombatants[CurrentPlayer].MyProfile.Weapon].SkillList[i];//Get ability number from the list
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

    void MovementButtonUpdates()
    {
        GridActor Selected = PlayerCombatants[CurrentPlayer].gameObject.GetComponent<GridActor>();
        bool CanMove = Selected.GetInPosition();
        int i = 0;
        while(i != MovementButtons.Length)
        {
            if(CanMove && Selected.GetCanMove(i))
            {
                MovementButtons[i].interactable = true;
            }
            else
            {
                MovementButtons[i].interactable = false;

                
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
        A.PhysicalAttack = TurnCombatants[CurrentTurn].gameObject.GetComponent<CombatProfile>().MyStats.Attack;
        A.MagicAttack = TurnCombatants[CurrentTurn].gameObject.GetComponent<CombatProfile>().MyStats.Resonance;
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
                //Set Primarily stats
                CX.Name = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Name;
                CX.MyStats.Health = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].MyStats.Health;
                CX.MyStats.Magic = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].MyStats.Magic;
                CX.MyStats.Attack = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].MyStats.Attack;
                CX.MyStats.Resonance = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].MyStats.Resonance;
                CX.MyStats.Defence = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].MyStats.Defence;
                CX.MyStats.Constitution = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].MyStats.Constitution;
                CX.MyStats.Evasion = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].MyStats.Evasion;
                CX.MyStats.Accuracy = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].MyStats.Accuracy;
                CX.PositiveAttribute = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].PositiveAttribute;
                CX.NegativeAttribute = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].NegativeAttribute;
                CX.Weapon = Ark.EnemyCombatants[CS.EnemyCombatants[EnemiesBuilt]].Weapon;

                //Add Weapon Stats
                CX.MyStats.Health += Ark.WeaponList[CX.Weapon].WeaponStats.Health;
                CX.MyStats.Magic += Ark.WeaponList[CX.Weapon].WeaponStats.Magic;
                CX.MyStats.Attack += Ark.WeaponList[CX.Weapon].WeaponStats.Attack;
                CX.MyStats.Resonance += Ark.WeaponList[CX.Weapon].WeaponStats.Resonance;
                CX.MyStats.Defence += Ark.WeaponList[CX.Weapon].WeaponStats.Defence;
                CX.MyStats.Constitution += Ark.WeaponList[CX.Weapon].WeaponStats.Constitution;
                CX.MyStats.Evasion += Ark.WeaponList[CX.Weapon].WeaponStats.Evasion;
                CX.MyStats.Accuracy += Ark.WeaponList[CX.Weapon].WeaponStats.Accuracy;

                //End Building enemy, increment values. 
                CX.MyStats.StatsSafetyCheck();//Safety nets the stat values to ensure they don't break boundaries. 
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
                CX.MyStats.Health = Ark.Combatants[CS.PlayerCombatants[i]].MyStats.Health;
                CX.MyStats.Magic = Ark.Combatants[CS.PlayerCombatants[i]].MyStats.Magic;
                CX.MyStats.Attack = Ark.Combatants[CS.PlayerCombatants[i]].MyStats.Attack;
                CX.MyStats.Resonance = Ark.Combatants[CS.PlayerCombatants[i]].MyStats.Resonance;
                CX.MyStats.Defence = Ark.Combatants[CS.PlayerCombatants[i]].MyStats.Defence;
                CX.MyStats.Constitution = Ark.Combatants[CS.PlayerCombatants[i]].MyStats.Constitution;
                CX.MyStats.Evasion = Ark.Combatants[CS.PlayerCombatants[i]].MyStats.Evasion;
                CX.MyStats.Accuracy = Ark.Combatants[CS.PlayerCombatants[i]].MyStats.Accuracy;
                CX.PositiveAttribute = Ark.Combatants[CS.PlayerCombatants[i]].PositiveAttribute;
                CX.NegativeAttribute = Ark.Combatants[CS.PlayerCombatants[i]].NegativeAttribute;
                CX.Weapon = Ark.Combatants[CS.PlayerCombatants[i]].Weapon;

                //Add Weapon Stats
                CX.MyStats.Health += Ark.WeaponList[CX.Weapon].WeaponStats.Health;
                CX.MyStats.Magic += Ark.WeaponList[CX.Weapon].WeaponStats.Magic;
                CX.MyStats.Attack += Ark.WeaponList[CX.Weapon].WeaponStats.Attack;
                CX.MyStats.Resonance += Ark.WeaponList[CX.Weapon].WeaponStats.Resonance;
                CX.MyStats.Defence += Ark.WeaponList[CX.Weapon].WeaponStats.Defence;
                CX.MyStats.Constitution += Ark.WeaponList[CX.Weapon].WeaponStats.Constitution;
                CX.MyStats.Evasion += Ark.WeaponList[CX.Weapon].WeaponStats.Evasion;
                CX.MyStats.Accuracy += Ark.WeaponList[CX.Weapon].WeaponStats.Accuracy;

                //End Building enemy, increment values. 
                CX.MyStats.StatsSafetyCheck();//Safety nets the stat values to ensure they don't break boundaries. 

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

    public void ReturnToMenu()
    {
        CombatScenario CS = Ark.gameObject.GetComponent<CombatScenario>();
        SceneMaster.LoadScene(CS.Home);
        SceneMaster.UnloadScene(this.gameObject.scene.name);
    }//Return to Menu after gameplay. 

    public void MoveSelectedPlayer(int Pos)
    {
        Vector2Int[] Var = new Vector2Int[4];
        Var[0] = Vector2Int.up;
        Var[1] = Vector2Int.down;
        Var[2] = Vector2Int.left;
        Var[3] = Vector2Int.right;

        GridActor Selected = PlayerCombatants[CurrentPlayer].gameObject.GetComponent<GridActor>();
        if (Selected.GetInPosition() && Selected.GetCanMove(Pos))
        {
            Selected.Position = Selected.Position + Var[Pos];//Move character
        }
        

    }

}
