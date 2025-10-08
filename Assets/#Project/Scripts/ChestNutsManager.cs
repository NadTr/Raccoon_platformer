using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChestNutsManager : MonoBehaviour
{
    // [SerializeField] private Animation youranimation;// = GetComponent<Animation> ();
    [SerializeField] private float delay = 0.15f;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChestnutCaught(delay));
        }
    }
    private IEnumerator ChestnutCaught(float delay = 0.2f)
    {
        animator.SetBool("is caught", true);
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
