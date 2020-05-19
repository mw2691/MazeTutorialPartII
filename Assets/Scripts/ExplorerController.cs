using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplorerController : MonoBehaviour {

    public bool UseCamera = true;

    public CameraFollow FollowerCameraPrefab;

    public SensorRig SensorRigPrefab;

    private CameraFollow cameraFollow;
    private SensorRig sensorRig;

	void Start () {
        if (this.UseCamera)
        {
            this.cameraFollow = GameObject.Instantiate(this.FollowerCameraPrefab, this.transform.position - this.transform.forward * .5f + new Vector3(0.0f, .5f, 0.0f), this.FollowerCameraPrefab.transform.rotation, null) as CameraFollow;
        }
        
        this.sensorRig = GameObject.Instantiate(this.SensorRigPrefab, this.transform.position, this.SensorRigPrefab.transform.rotation, null) as SensorRig;
	}
	
	void Update () {
        // the manual controller is just for testing
        if (Input.GetKeyUp(KeyCode.W))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10.0f);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.back * 10.0f);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.left * 10.0f);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.right * 10.0f);
        }

        this.act();
    }

    private void act()
    {
        // TODO: implement the autonomous movement based on the sensor data
    }

    void OnDestroy()
    {
        GameObject.Destroy(this.sensorRig.gameObject);
        if (this.UseCamera) GameObject.Destroy(this.cameraFollow.gameObject);
    }
}
