using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooletHop : MonoBehaviour {

    
    public float minTimeBetweenHops;
    public float maxTimeBetweenHops;
    public float minTimeBeforeHops;
    public float maxTimeBeforeHops;
    public float hopForce;
    private Rigidbody BooletRigidBody;
    private AudioSource BooletAudioSource;

    private void Awake()
    {
        BooletRigidBody = GetComponent<Rigidbody>();
        BooletAudioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        float timeBetweenHops = Random.Range(minTimeBetweenHops, maxTimeBetweenHops);
        float timeBeforeHops = Random.Range(minTimeBeforeHops, maxTimeBeforeHops);
        InvokeRepeating("Hop", timeBeforeHops, timeBetweenHops);
	}
	
	void Hop()
    {
        BooletRigidBody.AddForce(new Vector3(0f, hopForce, 0), ForceMode.VelocityChange);
        BooletRigidBody.AddRelativeTorque(new Vector3(Random.Range(1, 10), Random.Range(1, 10), 0f));
        BooletAudioSource.Play();
    }
}
