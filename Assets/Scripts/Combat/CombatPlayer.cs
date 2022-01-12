using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : MonoBehaviour
{

    public CombatantTurnSubmission[] Stacks;
    public int StackCount;
    // Start is called before the first frame update
    void Start()
    {
        StackState();
    }

    // Update is called once per frame
    void Update()
    {
        StackState();
    }

    void StackState()
    {
        int i = 0;
        Debug.Log("Receiving");
        while(i != Stacks.Length)
        {
            if(i <= StackCount)
            {
                Stacks[i].gameObject.SetActive(true);
            }
            else
            {
                Stacks[i].gameObject.SetActive(false);
            }

            i++;
        }
    }
}
