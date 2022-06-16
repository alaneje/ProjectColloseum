using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridActor : MonoBehaviour
{
       public Vector2Int Position;
       bool InPosition;
    bool LeftClear;
    bool RightClear;
    bool UpClear;
    bool DownClear;
   
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
        CheckCanMove();
        Vector3Int lPos = new Vector3Int(Position.x, Position.y, 0);
        if (CombatManager.tilemap.GetTile(lPos) != null)
        {
            Debug.Log("name : " + CombatManager.tilemap.GetTile(lPos).name + " & position : " + lPos);
        }
        else
        {
            Debug.Log("No Tile");
        }
        

    }

    void CheckCanMove()
    {
        Vector3Int lPos = new Vector3Int(Position.x, Position.y, 0);
        if (CombatManager.tilemap.GetTile(lPos + Vector3Int.up) != null)
        {
            UpClear = true;
        }
        else
        {
            UpClear = false;
        }
        if (CombatManager.tilemap.GetTile(lPos + Vector3Int.down) != null)
        {
            DownClear = true;
        }
        else
        {
            DownClear = false;
        }
        if (CombatManager.tilemap.GetTile(lPos + Vector3Int.left) != null)
        {
            LeftClear = true;
        }
        else
        {
            LeftClear = false;
        }
        if (CombatManager.tilemap.GetTile(lPos + Vector3Int.right) != null)
        {
            RightClear = true;
        }
        else
        {
            RightClear = false;
        }
    }

    public bool GetInPosition()
    {
        return InPosition;
    }

    public bool GetCanMove(int Button)
    {
        switch (Button)
        {
            case 0:return UpClear;
            case 1:return DownClear;
            case 2: return LeftClear;
            case 3: return RightClear;
            default: return false;
        }
        
    }
}
