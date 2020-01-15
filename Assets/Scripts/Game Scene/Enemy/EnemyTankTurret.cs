using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankTurret : MonoBehaviour {

    public GameObject player;
    public GameObject world;

    Vector3 playerPosition = new Vector3(0, 0, 0);

    private const string playerTankName = "TankHull";
    private const string worldControllerName = "World";
    private const string projectileTag = "Projectile";

    private const float projectileDamage = 10.0f;
    
	// Use this for initialization
	void Start () {
        player = GameObject.Find(playerTankName);
        world = GameObject.Find(worldControllerName);
	}
	
	// Update is called once per frame
	void Update () {
        //if the player exists set rotation to look at the player, excluding the y axis (this is set to look at its own transform.position so it will remain the same)
        if (world.GetComponent<World>().GetGameState() == true)
        {
            playerPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
            transform.LookAt(playerPosition);
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        //if enemy collides with a projectile decrement health
        if (collision.collider.tag == projectileTag)
        {
            GetComponentInParent<EnemyTankController>().DecrementHealth(projectileDamage);
        }
    }
}
