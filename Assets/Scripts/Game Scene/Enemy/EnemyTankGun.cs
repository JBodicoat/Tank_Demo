using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankGun : MonoBehaviour {

    private const string projectileTag = "Projectile";

    private const float projectileDamage = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        //if enemy collides with a projectile decrement health by 10
        if (collision.collider.tag == projectileTag)
        {
            GetComponentInParent<EnemyTankController>().DecrementHealth(projectileDamage);
        }
    }
}
