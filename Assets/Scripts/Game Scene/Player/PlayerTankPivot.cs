using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankPivot : MonoBehaviour {

    public GameObject TankTurret;

    private const float rotateSpeed = 120.0f;
    private const float maximumAngleUp = 320.0f;
    private const float maximumAngleDown = 5.0f;

    private const string mouseYAxis = "Mouse Y";

    private float xRotate = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //rotates the tank gun based on mouse movement about the x axis
        xRotate = Input.GetAxis(mouseYAxis);
        transform.Rotate(xRotate * rotateSpeed * Time.deltaTime, 0, 0);

        //limits the guns potential degrees of rotation to the difference between angleUp and angleDown
        if (transform.localEulerAngles.x > maximumAngleDown && transform.localEulerAngles.x < maximumAngleUp)
        {
            if (xRotate > 0)
            {
                transform.localRotation = Quaternion.Euler(maximumAngleDown, TankTurret.transform.localRotation.y, TankTurret.transform.localRotation.z);
            }
            else if (xRotate < 0)
            {
                transform.localRotation = Quaternion.Euler(maximumAngleUp, TankTurret.transform.localRotation.y, TankTurret.transform.localRotation.z);
            }
        }
    }
}