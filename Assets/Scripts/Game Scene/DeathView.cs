using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathView : MonoBehaviour {

    public GameObject world;
    public GameObject player;

    private const float speed = 2.0f;
	
	// Update is called once per frame
	void Update () {
        //if player does not exist then move backwards
        if (world.GetComponent<World>().GetGameState() == false)
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
	}
}
