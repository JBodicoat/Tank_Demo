using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShellExplosion : MonoBehaviour {

    private float timePassed = 0.0f;
    private float startTime = 0.0f;

    private const float lifeTime = 0.2f;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        timePassed = Time.time - startTime;
        if (timePassed >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}