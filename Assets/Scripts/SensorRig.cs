using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorRig : MonoBehaviour {

    public List<SensorData> CurrentData { get; private set; }

    public float SensorRange = 1.0f;
    // we use the late update here, to make sure that the current movement execution has been done
    void LateUpdate()
    {
        // TODO: the rig should follow the player
        this.updateSensorData();
    }

    private void updateSensorData()
    {
        List<SensorData> data = new List<SensorData>();

        // TODO: add some sensor data, four data points should suffice, event three should be fine

        this.CurrentData = data;
    }
}
