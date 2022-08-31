using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public bool activateOnAwake = true;

    bool _ragdollActive;
    public bool ragdollActive
    {
        get
        {
            return _ragdollActive;
        }
        set
        {
            _ragdollActive = value;
            //toggle animator
            if (animator != null)
            {
                animator.enabled = !_ragdollActive;
            }

            //togle physics components for ragdoll
            foreach (var col in colliders)
            {
                col.enabled = _ragdollActive;
            }
            foreach (var rb in rigidBodies)
            {
                rb.isKinematic = !_ragdollActive;
            }
        }
    }

    [Header("Animator")]
    public Animator animator;

    [Header("Ragdoll")]
    public List<Collider> colliders = new List<Collider>();
    public List<Rigidbody> rigidBodies = new List<Rigidbody>();

    void Awake()
    {
        ragdollActive = activateOnAwake;
    }

    void Reset()
    {
        //try to cache ragdol stuff and animator

        animator = GetComponent<Animator>();

        var childTransforms = GetComponentsInChildren<Transform>();

        colliders = new List<Collider>();
        rigidBodies = new List<Rigidbody>();

        foreach (Transform child in childTransforms)
        {
            var rb = child.GetComponent<Rigidbody>();
            var col = child.GetComponent<Collider>();

            if (rb != null && col != null)
            {
                colliders.Add(col);
                rigidBodies.Add(rb);
            }
        }

    }
}
