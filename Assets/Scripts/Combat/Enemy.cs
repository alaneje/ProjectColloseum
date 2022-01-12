using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CombatProfile EnemyProfile;
    public CombatantTurnSubmission MyTargetData;
    public Vector2Int Health;

    [Header("LinkData")]
    public CombatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
       // Health = EnemyProfile.Health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndLife()
    {
        if(EnemyProfile.Health.x < 1)
        {
           // this.gameObject.tag = "NOENEMY";
            this.gameObject.SetActive(false);
        }
    }

    public void GenerateTurnDecisions()
    {
        int Limit = combatManager.PlayerCombatants.Length;
        MyTargetData.Target = combatManager.PlayerCombatants[Random.Range(0,Limit)].gameObject;//temporary, will need to change but default targets Maya.
    }

    public void ApplyDamage(int Damage)
    {
        EnemyProfile.Health.x -= Damage;
    }
}
