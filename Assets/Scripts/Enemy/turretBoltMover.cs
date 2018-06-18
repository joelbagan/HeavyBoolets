using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBoltMover: MonoBehaviour {
    public float moveSpeed = 5.0f;

	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(Vector3.forward * moveSpeed);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Turret Bolt: Entered Player Trigger");
            other.GetComponent<PlayerHealth>().TakeDamage();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Turret Bolt Collision");
        Destroy(gameObject);
    }
}
