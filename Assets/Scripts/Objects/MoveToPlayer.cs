using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour {
    public float activationDistance = 7.5f;
    public float forceConstant = 9.8f;

    private Transform playerTransform;
    private Transform myTransform;
    private Rigidbody myRigidBody;
    private Vector3 toPlayer;
    public bool playerInRange = false;

	// Use this for initialization
	void Start () {
        myTransform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody>();

        GameObject player = GameObject.FindWithTag("Player");
		playerTransform = player.GetComponent<Transform>();
	}

	// Update is called once per frame
	void FixedUpdate () {
        toPlayer = playerTransform.position - myTransform.position;
		playerInRange = (toPlayer.magnitude < activationDistance ? true : false);
        if(playerInRange){
            myRigidBody.AddForce(toPlayer.normalized * forceConstant / (toPlayer.magnitude * toPlayer.magnitude), ForceMode.Impulse);
        }
	}
}
