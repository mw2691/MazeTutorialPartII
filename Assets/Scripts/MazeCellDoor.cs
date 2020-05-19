using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellDoor : MazeCellPassage {
    public Transform hinge;

    private MazeCellDoor OtherSideOfDoor
    {
        get
        {
            return otherCell.GetEdge(direction.GetOpposite()) as MazeCellDoor;
        }
    }

    public override void Initialize(MazeCell primary, MazeCell other, MazeDirection direction)
    {
        base.Initialize(primary, other, direction);
        if (OtherSideOfDoor != null)
        {
            this.hinge.localScale = new Vector3(-1f, 1f, 1f);
            Vector3 p = this.hinge.localPosition;
            p.x = -p.x;
            this.hinge.localPosition = p;
        }
    }
}
