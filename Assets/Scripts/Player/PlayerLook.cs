using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity;
    private float xAxisClamp = 0.0f;

    // Object interaction variables
    public bool lookingAtDoorButton = false;
    private Text interactText;
    private PlayerMove playerMove;
    private Camera playerCamera;
    private Ray lookRay = new Ray();
    private RaycastHit lookHit;
    private DoorOpener doorOpener;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMove = GetComponentInParent<PlayerMove>();
        playerCamera = GetComponent<Camera>();
        GameObject interactTextObj = GameObject.FindGameObjectWithTag("Interact Text");
        if(interactTextObj == null)
        {
            Debug.Log("Could not find Interact Text by tag");
        }
        else
        {
            interactText = interactTextObj.GetComponent<Text>();
        }
    }

    void LateUpdate()
    {
        RotateCamera();
        DoorCheck();
    }

    //Handles door open prompt display and interaction
    void DoorCheck()
    {
        //TODO reduce complexity
        if (lookingAtDoorButton && Input.GetKeyDown(KeyCode.E))
        {
            doorOpener.Open();
            playerMove.inRangeOfDoor = false;
            lookingAtDoorButton = false;
            interactText.enabled = false;
        }
        else if (playerMove.inRangeOfDoor)
        {
            // only RayCast if near a door for performance
            lookRay.origin = playerCamera.transform.position;
            lookRay.direction = playerCamera.transform.forward;

            if (Physics.Raycast(lookRay, out lookHit))
            {
                if (lookHit.collider.CompareTag("Open Button"))
                {
                    lookingAtDoorButton = true;
                    interactText.enabled = true;
                    interactText.text = "Press 'E' To Open";
                    doorOpener = lookHit.collider.GetComponentInParent<DoorOpener>();
                }
                else
                {
                    lookingAtDoorButton = false;
                    interactText.enabled = false;
                }

            }
        }
        else if (interactText.enabled)
        {
            interactText.enabled = false;
        }
    }

    /* Rotates the parent "Player" transform left/right
     * and the the Camera component of this object up/down
     * Since this object is a child of the "Player", it's
     * left/right rotation is based on that of the parent
     */
    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotBody = playerBody.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotBody.y += rotAmountX;

        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }

        transform.rotation = Quaternion.Euler(targetRotCam);
        playerBody.rotation = Quaternion.Euler(targetRotBody);
    }



}
