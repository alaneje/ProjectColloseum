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
        Health = EnemyProfile.Health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndLife()
    {
        if(Health.x < 1)
        {
           // this.gameObject.tag = "NOENEMY";
            this.gameObject.SetActive(false);
        }
    }

    public void GenerateTurnDecisions()
    {
        MyTargetData.Target = combatManager.Maya.gameObject;//temporary, will need to change but default targets Maya.
    }
}
