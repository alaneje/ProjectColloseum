using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : MonoBehaviour
{
   // public Vector2Int Position;
    public CombatantTurnSubmission Submission;
    public CombatProfile MyProfile;
   // public int StackCount;
    CombatManager CombatManager;
    bool canmove;
    int ActionPoints;
    // Start is called before the first frame update
    void Start()
    {
        GameObject X = GameObject.Find("CombatManager");
        CombatManager = X.GetComponent<CombatManager>();
    }

 

    // Update is called once per frame
    void Update()
    {
        EndLife();

        if(ActionPoints < 1)
        {
            if (canmove)
            {
                CombatManager.AnnounceTurnEnd();
            }
            canmove = false;
        }
      //  UpdatePositionToGrid();
    }
    
    public void EndLife()
    {
        if (MyProfile.MyStats.Health.x < 1)
        {
            // this.gameObject.tag = "NOENEMY";
            this.gameObject.SetActive(false);
        }
    }

    public void GenerateAttack(int buttonNumber)
    {
        ActionPoints--;
        int Ab = CombatManager.Ark.WeaponList[MyProfile.Weapon].SkillList[buttonNumber];
        CombatAbilityProfile A = Instantiate(CombatManager.Ark.CombatAbilities[Ab].gameObject, this.gameObject.transform.position, Quaternion.identity).GetComponent<CombatAbilityProfile>();
        A.SetTeam = CombatAbilityProfile.Team.Player;
        A.PhysicalAttack = MyProfile.MyStats.Attack;
        A.MagicAttack = MyProfile.MyStats.Resonance;
        A.AttributesCarried = MyProfile.PositiveAttribute;
    }

    public void StartTurn()
    {
        ActionPoints = MyProfile.MyStats.ActionPoints;
        canmove = true;
    }

    public bool GetCanMove()
    {
        return canmove;
    }

    public void SetCanMove(bool set)
    {
        canmove = set;

    }

    public int GetActionPoints()
    {
        return ActionPoints;
    }

    public void UseActionPoint()
    {
        ActionPoints--;
    }

}
