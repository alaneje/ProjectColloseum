using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archive : MonoBehaviour
{
    public enum Attribute {Null, Fire, Water, Earth, Air, Ether, Corrupted, Pierce, Slice, Blunt}
    [Header("Party")]
    public CombatProfile[] Party;

    [Header("General")]
    public CombatAbilityProfile[] CombatAbilities;
  
    //[Header("Party")]
   // public FaeCompaion Fae;
}
