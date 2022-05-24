using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer : MonoBehaviour
{
    public Vector2Int Position;
    public CombatantTurnSubmission Submission;
    public CombatProfile MyProfile;
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
        this.gameObject.transform.position = Pos;
    }

    // Update is called once per frame
    void Update()
    {
        EndLife();
      //  UpdatePositionToGrid();
    }

    public void EndLife()
    {
        if (MyProfile.MyStats.Health.x < 1)
        {
            // this.gameObject.tag = "NOENEMY";
            this.gameObject.SetActive(false);
        }
    }

}
