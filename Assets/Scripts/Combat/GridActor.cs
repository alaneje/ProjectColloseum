using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridActor : MonoBehaviour
{
       public Vector2Int Position;
       bool InPosition;
   
   // public int StackCount;
    CombatManager CombatManager;
    // Start is called before the first frame update
    void Start()
    {
         GameObject X = GameObject.Find("CombatManager");
        CombatManager = X.GetComponent<CombatManager>();
    }

 void UpdatePositionToGrid()
    {
        Vector3 Pos = CombatManager.TheGrid.GetCellCenterWorld(new Vector3Int(Position.x,Position.y,0));
        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position,Pos,Time.fixedDeltaTime);

    }

    void IsInPosition()
    {
         Vector3 Pos = CombatManager.TheGrid.GetCellCenterWorld(new Vector3Int(Position.x,Position.y,0));
        if(Vector3.Distance(Pos,this.gameObject.transform.position) < 0.1f)
        {
            InPosition = true;
        }
        else
        {
            InPosition = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositionToGrid();
        IsInPosition();
    }
}
