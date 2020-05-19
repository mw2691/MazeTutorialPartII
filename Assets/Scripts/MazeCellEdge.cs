﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour {

    public MazeCell cell;
    public MazeCell otherCell;

    public MazeDirection direction;

    public virtual void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;
        cell.SetEdge(direction, this);
        this.transform.parent = this.cell.transform;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = direction.ToRotation();
    }
}
