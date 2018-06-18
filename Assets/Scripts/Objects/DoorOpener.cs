using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Open()
    {
        animator.SetBool("Opened", true);
        GetComponent<BoxCollider>().enabled = false;
        audioSource.Play();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //    }
    //}
}
