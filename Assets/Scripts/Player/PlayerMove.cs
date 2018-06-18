using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController charControl;
    public float walkSpeed;
    public bool inRangeOfDoor = false;

    private Vector3 moveDirection;

    void Awake()
    {
        charControl = GetComponent<CharacterController>();
        charControl.minMoveDistance = 0.1f;
        moveDirection = new Vector3(0,0,0); 
    }

    void Update()
    {
        MovePlayer();
    }

    /* TODO make player stopping slightly less abrupt */
    void MovePlayer()
    {
        //initially move direction can be (1,0,1)
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //convert local direction to worldspace direction
        moveDirection = transform.TransformDirection(moveDirection);
        //normalized so as not to allow speed > walkSpeed when direction is (1,0,1)
        charControl.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            inRangeOfDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            inRangeOfDoor = false;
        }
    }
}
