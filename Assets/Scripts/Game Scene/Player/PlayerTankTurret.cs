using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankTurret : MonoBehaviour {

    private const float rotateSpeed = 100.00f;

    private const string mouseXAxis = "Mouse X";

    float yRotate = 0.0f;
    
	// Update is called once per frame
	void Update () {
        //rotates the tank turret based on the movement of the mouse in the x axis
        yRotate = Input.GetAxis(mouseXAxis) * Time.deltaTime;
        transform.Rotate(0, yRotate * rotateSpeed, 0);
    }
}
