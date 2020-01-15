using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyTankController : MonoBehaviour {

    private NavMeshAgent navMeshAgent;

    private GameObject player;
    private GameObject world;

    private bool lineOfSight = false;

    private float health = 30.0f;

    private Quaternion turretRotation;

    private Text healthText;

    private const float followDistance = 10.0f;
    private const float fleeDistance = 8.0f;
    private const float projectileDamage = 10.0f;
    private const float baseExplosionDamage = 2.0f;

    private const string playerTankName = "TankHull";
    private const string worldControllerName = "World";
    private const string enemyTag = "Enemy";
    private const string projectileTag = "Projectile";
    private const string explosionTag = "Explosion";

	// Use this for initialization
	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.Find(playerTankName);

        world = GameObject.Find(worldControllerName);

        healthText = GetComponentInChildren<Text>();
        healthText.text = health + " HP";
    }
	
	// Update is called once per frame
	void Update () {
        //if the player still exists
        if (world.GetComponent<World>().GetGameState() == true)
        {
            //if the player is further than 10 units navigate towards the player
            if (Vector3.Distance(player.transform.position, this.transform.position) > followDistance)
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
            //if player is less than 8 units away move backwards
            else if(Vector3.Distance(player.transform.position, this.transform.position) < fleeDistance)
            {
                //sets destination to 20 units behind this tank
                turretRotation = GetComponentInChildren<EnemyTankTurret>().transform.rotation;
                navMeshAgent.SetDestination(player.transform.position + (turretRotation * new Vector3(0, 0, -20)));
            }
            //otherwise stay still
            else
            {
                navMeshAgent.SetDestination(this.transform.position);
            }
            //if the enemy doesnt have line of sight with the player navigate towards the player
            if (lineOfSight == false)
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
        }
        //if player doesn't exist stop moving
        else
        {
            navMeshAgent.SetDestination(this.transform.position);
        }

        //removes game object if health is less than or equal to 0 and spawn the next wave of enemies
        if (health <= 0)
        {
            world.GetComponent<World>().IncrementScore();
            GameObject[] numberOfTanks = GameObject.FindGameObjectsWithTag(enemyTag);
            if (numberOfTanks.Length == 1) //if this is the only tank in the scene
            {
                world.GetComponent<World>().SpawnNextStage();
            }
            Destroy(gameObject);
        }
    }

    //sets the value of lineOfSight from "EnemyTankGun" script
    public void SetLineOfSight(bool value)
    {
        lineOfSight = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if enemy collides with a projectile decrement health
        if(collision.collider.tag == projectileTag)
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

    //Decreases health of this tank
    public void DecrementHealth(float damage)
    {
        health = health - damage;
        healthText.text = health + " HP";
    }

    //Returns the tanks health
    public float GetHealth()
    {
        return health;
    }
}