using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankEngine : MonoBehaviour {

    const float projectileDamage = 10.0f;
    const float multiplier = 2.0f;

    const string projectileTag = "Projectile";

    private void OnTriggerEnter(Collider other)
    {
        //decrease health by multiplying the damage done by the shell when engine hit by a shell
        if (other.tag == projectileTag)
        {
            GetComponentInParent<PlayerTankController>().DecrementHealth(projectileDamage * multiplier);
        }
    }
}
