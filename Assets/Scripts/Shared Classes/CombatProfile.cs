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
    [Header("Abilities")]
    public int[] SkillList;
    [Header("Depricated")]

    public Vector2Int Health;
    public Vector2Int Magic;
    public int Attack;
    public int Resonance;
    public int Defence;
    public int Constitution;
    public int Speed;
    public int Accuracy;
    public int Evasion;
    
    
  

    public void TakeDamage(int Damage, bool Magic, Archive.Attribute[] Attributes)
    {
        if (Magic)
        {
            Health.x -= Damage - (Resonance + 1);
        }
        else
        {
            Health.x -= Damage - (Defence + 1);
        }
        
    }

}
