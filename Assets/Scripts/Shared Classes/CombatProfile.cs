using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatProfile : MonoBehaviour
{
    [Header("Basic Information")]
    public string Name;
    public int StatGroup;
    public Stats MyStats;
    public Archive.Attribute[] PositiveAttribute;
    public Archive.Attribute[] NegativeAttribute;
    [Header("Equipment")]
    public int Weapon;
  //  [Header("Depricated")]
  //  public int[] SkillList;
   
    
    
  

    public void TakeDamage(int Damage, bool Magic, Archive.Attribute[] Attributes)
    {
        if (Magic)
        {
            MyStats.Health.x -= Damage - (MyStats.Resonance + 1);
        }
        else
        {
            MyStats.Health.x -= Damage - (MyStats.Defence + 1);
        }
        
    }

}
