using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTestAbilityScript : MonoBehaviour
{

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
        Enemy enex = other.gameObject.GetComponent<Enemy>();

        if(enex != null)
        {
            enex.Health.x -= 10;
        }
        else //means it's attacking the player
        {
            CombatManager CM = GameObject.FindObjectOfType<CombatManager>();
            CM.Ark.Party[0].Health.x -= 10;
        }
    }
}
