using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackPlayer : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 toPlayer;
    private bool playerSeen = false;
    private RaycastHit pointHit;

    public float timeBetweenAttacks = 0.5f;
    private float timer = 0.0f;

    public Transform boltSpawn;
    public GameObject bolt;

    private AudioSource audioSource;

    void GetPlayerRef()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.Log("Could not find player by tag");
        }
        else
        {
            playerTransform = playerObj.GetComponent<Transform>();
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetPlayerRef();
    }

    bool HaveLOS()
    {
        if (playerTransform == null)
        {
            Debug.Log("Failed to get player ref on startup");
            GetPlayerRef();
        }
        toPlayer = playerTransform.position - transform.position;
        Vector3 lookPoint = transform.position;
        lookPoint.y = 1f;
        if (Physics.Raycast(lookPoint, toPlayer, out pointHit))
        {
            Debug.DrawRay(lookPoint, toPlayer);
            if (pointHit.transform == playerTransform)
            {
                playerSeen = true;
                //play sound
            }
            return pointHit.transform == playerTransform;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSeen || HaveLOS())
        {
            //move head to look at player
            transform.LookAt(playerTransform);
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks)
            {
                timer = 0f;
                Fire();
            }
        }
    }

    void Fire()
    {
        Instantiate(bolt, boltSpawn.position, boltSpawn.rotation);
        audioSource.Play();
    }
}
