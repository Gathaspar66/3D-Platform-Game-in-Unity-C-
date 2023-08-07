using Unity.VisualScripting;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Animator animator;

    public GameObject activateObject;

    private Collider swordCollider;

    private void Start()
    {
        swordCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.CrossFade("SwordAttack", 0.25f, -1, 0);
        }
    }


    public void AttackStart()
    {
        swordCollider.enabled = true;
    }

    public void AttackEnd()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IActivable>().Activate();
    }
}