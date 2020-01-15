using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankGunRotation : MonoBehaviour {

    private GameObject player;
    private GameObject world;

    private const string playerTankName = "TankHull";
    private const string worldControllerName = "World";

    // Use this for initialization
    void Start () {
        player = GameObject.Find(playerTankName);
        world = GameObject.Find(worldControllerName);
    }
	
	// Update is called once per frame
	void Update () {
        //if player exists set rotation to look at the player
        if (world.GetComponent<World>().GetGameState() == true)
        {
            transform.LookAt(player.transform);
        }
	}
}
