using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilityController : MonoBehaviour
{
    public GameObject Target;

    CombatManager CM;
    // Start is called before the first frame update
    void Start()
    {
        CM = GameObject.FindObjectOfType<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        CM.EndAbilitySegment(0);
    }
}
