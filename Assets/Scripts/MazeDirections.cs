using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeDirections
{

    public const int Count = 4;

    private static CellVector[] vectors = {
        new CellVector(0, 1), // NORTH
        new CellVector(1, 0), // EAST
        new CellVector(0, -1), // SOUTH
        new CellVector(-1, 0) // WEST
    };

    // Das ist eine so genannte 'extension method'. Das 'this' keyword sorgt dafuer, dass sich das ganze wie eine auf MazeDirection definierte Methode
    // verhaelt. Probiert was passiert, wenn man das this keyword entfernt.
    public static CellVector ToCellVector(this MazeDirection direction)
    {
        return MazeDirections.vectors[(int)direction];
    }

    private static MazeDirection[] opposites = {
        MazeDirection.SOUTH,
        MazeDirection.WEST,
        MazeDirection.NORTH,
        MazeDirection.EAST
    };

    public static MazeDirection GetOpposite(this MazeDirection direction)
    {
        return MazeDirections.opposites[(int) direction];
    }

    public static MazeDirection RandomValue
    {
        get
        {
            return (MazeDirection) UnityEngine.Random.Range(0, Count);
        }
    }

    private static Quaternion[] rotations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 90f, 0f),
        Quaternion.Euler(0f, 180f, 0f),
        Quaternion.Euler(0f, 270f, 0f)
    };

    public static Quaternion ToRotation(this MazeDirection direction)
    {
        return MazeDirections.rotations[(int) direction];
    }
}
