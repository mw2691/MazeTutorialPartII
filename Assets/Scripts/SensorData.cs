using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SensorData {
    // feel free to add varaibles
    public float distance { get; private set; }
    public Vector3 direction { get; private set; }

    public SensorData(Vector3 direction, float distance)
    {
        this.direction = direction;
        this.distance = distance;
    }

    public static int CompareDataByDistance(SensorData x, SensorData y)
    {
        // TODO: implement a sort method
        return -1;
    }
}
