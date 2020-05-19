using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct CellVector {
    public int x;
    public int z;

    public CellVector(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static CellVector operator +(CellVector a, CellVector b)
    {
        a.x += b.x;
        a.z += b.z;
        return a;
    }
}