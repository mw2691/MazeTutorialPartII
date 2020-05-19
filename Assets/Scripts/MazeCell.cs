using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

    public CellVector coordinates;

    private int initializedEdgeCount;

    public MazeCellEdge GetEdge(MazeDirection direction)
    {
        return this.edges[(int) direction];
    }

    public bool IsFullyInitialized
    {
        get
        {
            return initializedEdgeCount == MazeDirections.Count;
        }
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge)
    {
        this.edges[(int)direction] = edge;
        this.initializedEdgeCount += 1;
    }

    public MazeDirection RandomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, MazeDirections.Count - this.initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++)
            {
                if (this.edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection) i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
        }
    }
}
