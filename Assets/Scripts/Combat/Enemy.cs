using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CombatProfile EnemyProfile;
    public CombatantTurnSubmission MyTargetData;
    public GridActor MyGridActor;
    public GameObject Tracking;
  
    [Header("LinkData")]
    public CombatManager combatManager;
    public float Range = 0.3f;
    public float MagF;

    int selectedattack;
    int ActionPoints;
    float RangefromEnemy;
    bool notinRangeOfSelectedAttack;
    bool canMove;
    // Start is called before the first frame update
    void Start()
    {
       // Health = EnemyProfile.Health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (combatManager.GetTime() && canMove)
        {
            CheckActionPoints();
            if (Tracking == null)
            {
                RandomMovement();
            }
            else
            {
                CheckAndUpdateRange();//Checks and updates the range from the Player we are tracking.
                CheckIfInRangeForSelectedAttack();//Check if we're in the range of the selected attack
                TrackingMovement();//activate tracking movement
                AttackPlayerWithSelectedAttack();//attack the player
            }
        }
        
    }
    void AttackPlayerWithSelectedAttack()
    {
        if(Tracking != null && !notinRangeOfSelectedAttack && canMove)
        {
            CombatAbilityProfile A = Instantiate(combatManager.Ark.CombatAbilities[combatManager.Ark.WeaponList[EnemyProfile.Weapon].SkillList[selectedattack]].gameObject, this.gameObject.transform.position, Quaternion.identity).GetComponent<CombatAbilityProfile>();
            ActionPoints--;//use an action point.
            A.SetTeam = CombatAbilityProfile.Team.Enemy;//set the correct team
            A.PhysicalAttack = EnemyProfile.MyStats.Attack;
            A.MagicAttack = EnemyProfile.MyStats.Resonance;
            A.AttributesCarried = EnemyProfile.PositiveAttribute;
        }
    }
    void CheckAndUpdateRange()
    {
        if (Tracking != null)
        {
            Vector3 dir = (this.transform.position - Tracking.transform.position);
            float Mag = Vector3.Magnitude(dir);                    
            RangefromEnemy = Mag;
            

        }
    }
    void CheckIfInRangeForSelectedAttack()
    {
        if (RangefromEnemy > combatManager.Ark.CombatAbilities[combatManager.Ark.WeaponList[EnemyProfile.Weapon].SkillList[selectedattack]].Range)
        {
            notinRangeOfSelectedAttack = true;
        }
        else
        {
            notinRangeOfSelectedAttack = false;
        }
    }
    void TrackingMovement()
    {
        int i = 0;
        bool w = false;
        if (Tracking != null)
        {
            Vector3 dir = (this.transform.position - Tracking.transform.position);
            float Mag = Vector3.Magnitude(dir);
            Vector3 Diri = dir / Mag;
            Diri = Vector3.Normalize(Diri);
            MagF = Mag;
           
            Debug.Log(Mag);
            if (Diri.x > 0.1) { i = 2; }
            if (Diri.x < -0.1) { i = 3; }
            if (Diri.z > 0.1) { i = 1; }
            if (Diri.z < -0.1) { i = 0; }

        }

        bool b = MyGridActor.GetCanMove(i);
        //Debug.Log("Checking " + i + " can move is: " + b);

        if (b && MyGridActor.GetInPosition() && canMove && notinRangeOfSelectedAttack)
        {
            switch (i)
            {
                case 0: MyGridActor.Position += Vector2Int.up; break;
                case 1: MyGridActor.Position += Vector2Int.down; break;
                case 2: MyGridActor.Position += Vector2Int.left; break;
                case 3: MyGridActor.Position += Vector2Int.right; break;

            }
            ActionPoints--;
        }
    }
    void RandomMovement()
    {
        int i = 0;
        
            i = Random.Range(0, 4);
          
       

        bool b = MyGridActor.GetCanMove(i);
        //Debug.Log("Checking " + i + " can move is: " + b);

        if (b && MyGridActor.GetInPosition() && canMove)
        {
            switch (i)
            {
                case 0: MyGridActor.Position += Vector2Int.up; break;
                case 1: MyGridActor.Position += Vector2Int.down; break;
                case 2: MyGridActor.Position += Vector2Int.left; break;
                case 3: MyGridActor.Position += Vector2Int.right; break;

            }
            ActionPoints--;
        }
    }//move randomly around the field
    void CheckActionPoints()
    {
        if(ActionPoints < 1)
        {
            if (canMove)
            {
                combatManager.AnnounceTurnEnd();//announce the end of the turn to the combat manager.

            }//Ensures it only fires once. 
            canMove = false;
        }
    }
    public void MovementAI()
    {
        int i = 0;
        bool w = false;
        if(Tracking == null)
        {
            i = Random.Range(0,4);
            w = true;
        }
        else
        {
            Vector3 dir = (this.transform.position - Tracking.transform.position);
            float Mag = Vector3.Magnitude(dir);
            Vector3 Diri = dir / Mag;
            Diri = Vector3.Normalize(Diri);
            MagF = Mag;
            if(Mag > combatManager.Ark.CombatAbilities[combatManager.Ark.WeaponList[EnemyProfile.Weapon].SkillList[selectedattack]].Range)
            {
                w = true;
            }
            else
            {
                w = false;
            }
            Debug.Log(Mag);
            if(Diri.x > 0.1) { i = 2; }
            if(Diri.x < -0.1) { i = 3; }
            if(Diri.z > 0.1) { i = 1; }
            if(Diri.z < -0.1) { i = 0; }

        }
        
        bool b = MyGridActor.GetCanMove(i);
        //Debug.Log("Checking " + i + " can move is: " + b);

        if(b && MyGridActor.GetInPosition() && w)
        {
            switch(i)
            {
                case 0: MyGridActor.Position += Vector2Int.up;break;
                 case 1: MyGridActor.Position += Vector2Int.down;break;
                  case 2: MyGridActor.Position += Vector2Int.left;break;
                   case 3: MyGridActor.Position += Vector2Int.right;break;
                
            }
        }
    }

    public void EndLife()
    {
        if(EnemyProfile.MyStats.Health.x < 1)
        {
            // this.gameObject.tag = "NOENEMY";
            HandleDeath();
        }
    }

    public void GenerateTurnDecisions()
    {
        ActionPoints = EnemyProfile.MyStats.ActionPoints;
        if (EnemyProfile.HuntsPlayers)
        {
            if(Tracking != null)
            {
                CombatProfile cp = Tracking.GetComponent<CombatProfile>();
                if(cp.MyStats.Health.x < 1)
                {
                    Tracking = null;
                }
            }
            if(Tracking == null)
            {
                int p = Random.Range(0, combatManager.PlayerCombatants.Length);
                Tracking = combatManager.PlayerCombatants[p].gameObject;
            }
            canMove = true;
            selectedattack = Random.Range(0, combatManager.Ark.WeaponList[EnemyProfile.Weapon].SkillList.Length);

        }//if there's no enemy set or the enemy set is dead, change to a random set enemy. Then select a random attack from the list and go forward with it. 
    }

    public void ApplyDamage(int Damage)
    {
        EnemyProfile.MyStats.Health.x -= Damage;
    }

    void HandleDeath()
    {
        this.gameObject.SetActive(false);

    }
}
