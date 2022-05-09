using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : MonoBehaviour
{

    public CombatantTurnSubmission Submission;
    public CombatProfile MyProfile;
   // public int StackCount;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EndLife();
    }

    public void EndLife()
    {
        if (MyProfile.MyStats.Health.x < 1)
        {
            // this.gameObject.tag = "NOENEMY";
            this.gameObject.SetActive(false);
        }
    }

}
