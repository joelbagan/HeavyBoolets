using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLeakController : MonoBehaviour {
    public enum State {
        Idle,
        WindUp,
        Moving,
        Attacking,
        Dying,
    }
    public State state;
    public float timer = 0.0f;

    private Transform playerTransform;
    private PlayerHealth playerHealth;
    

    public Vector3 toPlayer;
    private Vector3 toDestination;
    private AudioSource audioSource;
    private UnityEngine.AI.NavMeshAgent nav;
    private EnemyHealth health;

    RaycastHit pointHit;
    Vector3 targetPosition;

    public float windUpTime = 2.5f;
    public AudioClip onPlayerSightedClip;

    public float attackRange = 5.0f;    
    public float timeBetweenAttacks = 0.5f;
    public AudioClip attackClip;

    
    // Use this for initialization
    void Awake () {
        state = State.Idle;
        GetPlayerRef();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<EnemyHealth>();
    }
    
    // Update is called once per frame
    void Update () {
        toPlayer = playerTransform.position - transform.position;
        toDestination = targetPosition - transform.position;
        if(health.isDead) state = State.Dying;
        switch (state) 
        {
            case State.Idle:
                if(HaveLOS()){
                    state = State.WindUp;
                }
                else {
                }
                break;
            case State.WindUp:
                WindUp();
                break;
            case State.Moving:
                MoveToTarget();
                break;
            case State.Attacking:
                timer += Time.deltaTime;
                if(!HaveLOS()){
                    //lost LOS while attacking
                    timer = 0f;
                    state = State.Idle;
                }
                else if(timer >= timeBetweenAttacks && PlayerInRange()){
                    
                    Attack ();
                }
                else if(!PlayerInRange()){
                    state = State.Moving;
                }
                break;
            case State.Dying:
                break;
            default:
              
              break;
        }
    }

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

    bool HaveLOS(){
        if (playerTransform == null)
        {
            Debug.Log("Failed to get player ref on startup");
            GetPlayerRef();
        }
        Vector3 lookPoint = transform.position;
        lookPoint.y = 1f;
        if (Physics.Raycast(lookPoint, toPlayer, out pointHit)){
            Debug.DrawRay(lookPoint, toPlayer);
            return pointHit.transform == playerTransform;
        }
        else{
            return false;
        }
    }

    bool PlayerInRange(){
        return toPlayer.magnitude <= attackRange;
    }

    void Attack(){
        //timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && PlayerInRange())
        {
            timer = 0f;
            if(playerHealth.currentHealth > 0)
            {
                audioSource.clip = attackClip;
                audioSource.Play();
                playerHealth.TakeDamage ();
            }
        }
    }

    void WindUp(){
        if(timer == 0f){
            audioSource.clip = onPlayerSightedClip;
        audioSource.Play();
        }
        timer += Time.deltaTime;
        if(!HaveLOS()){
            //LOS broken on wind up
        }
        else if(timer >= windUpTime){
            //has LOS and timer finished
            targetPosition = playerTransform.position;
            state = State.Moving;
        }
    }

    void MoveToTarget(){
        if(PlayerInRange()){
            timer = 0f;
            state = State.Attacking;
            nav.isStopped = true;
        }
        else if(HaveLOS()){//update destination
            targetPosition = playerTransform.position;
            nav.SetDestination(targetPosition);
            nav.isStopped = false;
        }
        else if(!HaveLOS() && toDestination.magnitude < 1.0f){
            timer = 0f;
            state = State.Idle;
        }
    }
}
