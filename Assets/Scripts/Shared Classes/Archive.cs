using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archive : MonoBehaviour
{
    public enum Attribute {Null, Fire, Water, Earth, Air, Ether, Corrupted, Pierce, Slice, Blunt}
    [Header("Player Objects")]
    public CombatProfile[] Combatants;

    [Header("General")]
    public CombatAbilityProfile[] CombatAbilities;
    public CombatProfile[] CombatantsList;
    public CombatProfile[] EnemyCombatants;
    public Weapon[] WeaponList;
    //[Header("Party")]
   // public FaeCompaion Fae;
}
