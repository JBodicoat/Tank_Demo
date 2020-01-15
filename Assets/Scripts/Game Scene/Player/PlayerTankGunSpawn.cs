using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTankGunSpawn : MonoBehaviour {
  
    public GameObject TankShell;

    private const float fireDelay = 3.0f;

    private float timePassed = 0.0f;
    private float fireTime = -fireDelay; //so tank can immediately fire

    private int keyBindingFireInt = 0;

    public InputField keyBindingFireString;

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //fires a projectile using mouse buttons
        if (keyBindingFireString.text.Contains("mouse"))
        {
            if (keyBindingFireString.text.Contains("left"))
            {
                keyBindingFireInt = 0;
            }
            else if (keyBindingFireString.text.Contains("right"))
            {
                keyBindingFireInt = 1;
            }
            else if (keyBindingFireString.text.Contains("middle"))
            {
                keyBindingFireInt = 2;
            }
            //creates a copy of TankShell in front of the gun if mouse button is clicked and it has been 3 seconds or more since last firing
            if (timePassed >= fireDelay && Input.GetMouseButtonDown(keyBindingFireInt))
            {
                SpawnShell();
            }
        }
        //fires a projectile using key buttons
        else
        {
            //spawns shell when user presses fire button and appropriate amount of time has passed since last firing
            if (timePassed >= fireDelay && Input.GetKeyDown(keyBindingFireString.text))
            {
                SpawnShell();
            }
        }

        timePassed = Time.time - fireTime;
    }

    //creates a new tank shell infront of the gun
    private void SpawnShell()
    {
        Instantiate(TankShell, transform.position, transform.rotation);
        fireTime = Time.time;
        
        audioSource.PlayOneShot(audioSource.clip, 1);
    }
}
