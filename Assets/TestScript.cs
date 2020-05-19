using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public GameObject rightSensor;
    public GameObject leftSensor;


    // Update is called once per frame
    void Update()
    {
        Ray rightSensorRay = new Ray(rightSensor.transform.position, this.transform.forward);
        Ray leftSensorRay = new Ray(leftSensor.transform.position, this.transform.forward);

        Debug.DrawRay(this.rightSensor.transform.position, this.transform.forward, Color.green);
        Debug.DrawRay(this.leftSensor.transform.position, this.transform.forward, Color.green);

        RaycastHit leftHit;
        RaycastHit rightHit;

        Physics.Raycast(rightSensorRay, out rightHit,2f);
        Physics.Raycast(leftSensorRay, out leftHit, 2f);


        if (leftHit.collider == null && rightHit.collider != null)
        {
            this.transform.Rotate(new Vector3(0f, 4.5f, 0f));
        }

        if (leftHit.collider != null && rightHit.collider == null)
        {
            this.transform.Rotate(new Vector3(0f, -4.5f, 0f));
        }

        if (leftHit.collider == null && rightHit.collider == null)
        {
            this.transform.Translate(new Vector3(0F, 0f, 1f * Time.deltaTime));
        }

    }
}
