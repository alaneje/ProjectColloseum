using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTestAbilityScript : MonoBehaviour
{
    public CombatAbilityProfile MyProfile;
    float Timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Timer -= Time.deltaTime;

        if(Timer < 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
        CombatProfile enex = other.gameObject.GetComponent<CombatProfile>();

        if(enex != null)
        {
            if (MyProfile.Magic)
            {
                enex.TakeDamage(MyProfile.Power + MyProfile.MagicAttack);
            }
            else
            {
                enex.TakeDamage(MyProfile.Power + MyProfile.PhysicalAttack);
            }
        }
        
    }
}
