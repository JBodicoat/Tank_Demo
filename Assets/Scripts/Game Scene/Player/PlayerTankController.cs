using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTankController : MonoBehaviour {

    private string keyForwards = "w";
    private string keyBackwards = "s";
    private string keyRight = "d";
    private string keyLeft = "a";

    public InputField keyBindingForwards;
    public InputField keyBindingBackwards;
    public InputField keyBindingRight;
    public InputField keyBindingLeft;

    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public GameObject crosshair;

    public WheelCollider wheel1;
    public WheelCollider wheel2;
    public WheelCollider wheel3;
    public WheelCollider wheel4;
    public WheelCollider wheel5;
    public WheelCollider wheel6;

    Vector3 centreOfMassDisposition = new Vector3(0, -1, 0);

    private float health = 1000;

    private const float force = 1000.0f;
    private const float projectileDamage = 10.0f;
    private const float baseExplosionDamage = 2.0f;

    private const int numberOfWheels = 6;

    private Rigidbody rigidBody;

    private bool playerState = true;

    private const string projectileTag = "Projectile";
    private const string explosionTag = "Explosion";

    private const string changeCamerasButton = "y";

    // Use this for initialization
    void Start () {
        keyBindingForwards.text = keyForwards;
        keyBindingBackwards.text = keyBackwards;
        keyBindingRight.text = keyRight;
        keyBindingLeft.text = keyLeft;

        wheel1 = GameObject.Find("LeftWheel1").GetComponent<WheelCollider>();
        wheel2 = GameObject.Find("LeftWheel2").GetComponent<WheelCollider>();
        wheel3 = GameObject.Find("LeftWheel3").GetComponent<WheelCollider>();
        wheel4 = GameObject.Find("RightWheel1").GetComponent<WheelCollider>();
        wheel5 = GameObject.Find("RightWheel2").GetComponent<WheelCollider>();
        wheel6 = GameObject.Find("RightWheel3").GetComponent<WheelCollider>();

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass += centreOfMassDisposition; //lowers centre of mass so tank better rotates

        crosshair.SetActive(false);
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //removes gameobject if health is less than or equal to 0
        if (health <= 0)
        {
            gameObject.SetActive(false);
            playerState = false;
        }
        
        //retrieves key bindings
        keyForwards = keyBindingForwards.text;
        keyBackwards = keyBindingBackwards.text;
        keyRight = keyBindingRight.text;
        keyLeft = keyBindingLeft.text;

        if (Input.GetKey(keyForwards))
        {
            if (Input.GetKey(keyLeft))
            {
                //move forwards and left
                wheel1.motorTorque = 0;
                wheel2.motorTorque = 0;
                wheel3.motorTorque = 0;
                wheel4.motorTorque = force;
                wheel5.motorTorque = force;
                wheel6.motorTorque = force;
            }
            else if (Input.GetKey(keyRight))
            {
                //move forwards and right
                wheel1.motorTorque = force;
                wheel2.motorTorque = force;
                wheel3.motorTorque = force;
                wheel4.motorTorque = 0;
                wheel5.motorTorque = 0;
                wheel6.motorTorque = 0;
            }
            else
            {
                //move forwards
                wheel1.motorTorque = force;
                wheel2.motorTorque = force;
                wheel3.motorTorque = force;
                wheel4.motorTorque = force;
                wheel5.motorTorque = force;
                wheel6.motorTorque = force;
            }
        }
        else if (Input.GetKey(keyBackwards))
        {
            if (Input.GetKey(keyLeft))
            {
                //move backwards and left
                wheel1.motorTorque = 0;
                wheel2.motorTorque = 0;
                wheel3.motorTorque = 0;
                wheel4.motorTorque = -force;
                wheel5.motorTorque = -force;
                wheel6.motorTorque = -force;
            }
            else if (Input.GetKey(keyRight))
            {
                //move backwards and right
                wheel1.motorTorque = -force;
                wheel2.motorTorque = -force;
                wheel3.motorTorque = -force;
                wheel4.motorTorque = 0;
                wheel5.motorTorque = 0;
                wheel6.motorTorque = 0;
            }
            else
            {
                //move backwards
                wheel1.motorTorque = -force;
                wheel2.motorTorque = -force;
                wheel3.motorTorque = -force;
                wheel4.motorTorque = -force;
                wheel5.motorTorque = -force;
                wheel6.motorTorque = -force;
            }
        }
        else if (Input.GetKey(keyLeft))
        {
            //rotate left
            wheel1.motorTorque = -force;
            wheel2.motorTorque = -force;
            wheel3.motorTorque = -force;
            wheel4.motorTorque = force;
            wheel5.motorTorque = force;
            wheel6.motorTorque = force;
        }
        else if (Input.GetKey(keyRight))
        {
            //rotate right
            wheel1.motorTorque = force;
            wheel2.motorTorque = force;
            wheel3.motorTorque = force;
            wheel4.motorTorque = -force;
            wheel5.motorTorque = -force;
            wheel6.motorTorque = -force;
        }
        else
        {
            //stop moving
            wheel1.motorTorque = 0;
            wheel2.motorTorque = 0;
            wheel3.motorTorque = 0;
            wheel4.motorTorque = 0;
            wheel5.motorTorque = 0;
            wheel6.motorTorque = 0;
        }
        
        transform.position = (wheel1.transform.position + wheel2.transform.position + wheel3.transform.position + wheel4.transform.position + wheel5.transform.position + wheel6.transform.position) / numberOfWheels; //sets tanks position to average of wheels positions

        if (Input.GetKeyDown(changeCamerasButton))
        {
            thirdPersonCamera.SetActive(!thirdPersonCamera.activeSelf);
            firstPersonCamera.SetActive(!firstPersonCamera.activeSelf);
            crosshair.SetActive(!crosshair.activeSelf);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if collides with a projectile decrement health
        if (collision.collider.tag == projectileTag)
        {
            DecrementHealth(projectileDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if enemy collides with explosion decrement health based on distance from the explosions centre
        if (other.tag == explosionTag)
        {
            float distance = Vector3.Distance(other.transform.position, this.transform.position);
            DecrementHealth(baseExplosionDamage / distance);
        }
    }

    //returns players health for score counter
    public float GetHealth()
    {
        return health;
    }

    //decreases health based  on where tank was hit
    public void DecrementHealth(float damage)
    {
        health = health - damage;
    }

    //returns the bool playerState
    public bool getPlayerState()
    {
        return playerState;
    }
}