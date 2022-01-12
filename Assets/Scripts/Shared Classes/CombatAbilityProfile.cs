using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAbilityProfile : MonoBehaviour
{
    public enum AbilityType {Passive,Active}
    public string AbilityName;
    public bool Magic;//If false physical.
    public Archive.Attribute AbilityAttribute;
    public AbilityType abilityType;
    public int Power;

    [Header("Carry Storage")]
    public int MagicAttack;
    public int PhysicalAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
