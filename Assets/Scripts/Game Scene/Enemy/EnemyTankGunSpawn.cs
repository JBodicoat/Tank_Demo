using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankGunSpawn : MonoBehaviour {

    private GameObject TankShell;
    private GameObject world;

    private float timePassed = 0.0f;
    private float fireTime = 0.0f;
    private const float fireDelay = 5.0f;

    private bool lineOfSight = false;

    private AudioSource audioSource;

    private const string tankShellPrefabDirectory = "Prefabs/TankShell";
    private const string worldControllerName = "World";
    private const string playerTag = "Player";

    // Use this for initialization
    void Start()
    {
        TankShell = (GameObject)Resources.Load(tankShellPrefabDirectory, typeof(GameObject));
        world = GameObject.Find(worldControllerName);
        audioSource = GetComponent<AudioSource>();
        fireTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (world.GetComponent<World>().GetGameState() == true)
        {
            //creates a raycast from the current position pointing forwards and assigns information from first object hit to "hit"
            //raycast ignores projectiles. if player is hit then lineOfSight is set to true
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider.tag == playerTag)
                {
                    lineOfSight = true;

                    //fires a projectile at the player after the fire delay
                    if (timePassed >= fireDelay)
                    {
                        SpawnShell();
                    }
                }
                else
                {
                    lineOfSight = false;
                }
            }
            timePassed = Time.time - fireTime;
            //sends value of "lineOfSight" to "EnemyTankController" script
            GetComponentInParent<EnemyTankController>().SetLineOfSight(lineOfSight);       
        }
    }

    //creates a tank shell in front of the gun
    private void SpawnShell()
    {
        Instantiate(TankShell, transform.position, transform.rotation);
        fireTime = Time.time;

        audioSource.PlayOneShot(audioSource.clip, 1);
    }
}
