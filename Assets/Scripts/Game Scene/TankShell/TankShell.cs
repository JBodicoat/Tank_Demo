using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShell : MonoBehaviour {

    private const float force = 100.0f;

    private GameObject wheel1;
    private GameObject wheel2;
    private GameObject wheel3;
    private GameObject wheel4;
    private GameObject wheel5;
    private GameObject wheel6;
    private GameObject explosion;
    private GameObject tankShell;

    private Rigidbody rigidBody;

    private const string projectileTag = "Projectile";
    private const string wheelTag = "Wheels";
    private const string tankShellPrefabDirectory = "Prefabs/TankShell";
    private const string shellExplosionPrefabDirectory = "Prefabs/TankShellExplosion";

    // Use this for initialization
    void Start()
    {
        wheel1 = GameObject.Find("LeftWheel1");
        wheel2 = GameObject.Find("LeftWheel2");
        wheel3 = GameObject.Find("LeftWheel3");
        wheel4 = GameObject.Find("RightWheel1");
        wheel5 = GameObject.Find("RightWheel2");
        wheel6 = GameObject.Find("RightWheel3");
        Physics.IgnoreCollision(this.GetComponent<Collider>(), wheel1.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), wheel2.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), wheel3.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), wheel4.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), wheel5.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), wheel6.GetComponent<Collider>());

        explosion = (GameObject)Resources.Load(shellExplosionPrefabDirectory, typeof(GameObject));

        tankShell = (GameObject)Resources.Load(tankShellPrefabDirectory, typeof(GameObject));
        Physics.IgnoreCollision(this.GetComponent<Collider>(), tankShell.GetComponent<Collider>());

        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        rigidBody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    //explodes the shell when collision with something other than another projectile or a wheel
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != projectileTag && collision.collider.tag != wheelTag)
        {
            Explode();
        }
    }

    //explodes the shell when entering a trigger
    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    //destroys the projectile and spawns an explosion
    private void Explode()
    {
        Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }

}
