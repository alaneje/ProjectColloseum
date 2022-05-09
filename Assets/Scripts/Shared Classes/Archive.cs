using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archive : MonoBehaviour
{
    public enum Attribute {Null, Fire, Water, Earth, Air, Ether, Corrupted, Pierce, Slice, Blunt}
    [Header("Player Objects")]
    public CombatProfile[] Combatants;
    public int[] WeaponStorage;//Denotes the amount of each weapon from weapon list in storage. (position 0 is irrelevnant)

    [Header("General")]
    public CombatAbilityProfile[] CombatAbilities;
    public CombatProfile[] CombatantsList;
    public CombatProfile[] EnemyCombatants;
    public Weapon[] WeaponList;
    //[Header("Party")]
   // public FaeCompaion Fae;
}
