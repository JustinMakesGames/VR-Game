using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PhysicsHand : MonoBehaviour
{
    public Transform target;
    private Rigidbody _rb;
    private Rigidbody _heldItem;
    private ActionBasedController _actionBasedController;

    private void Start()
    {
        _actionBasedController = target.GetComponent<ActionBasedController>();
        _rb = GetComponent<Rigidbody>();
        _rb.maxAngularVelocity = 100f;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;
        Quaternion rotationDif = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDif.ToAngleAxis(out float angle, out Vector3 axis);
        Vector3 rotationDifDegree = angle * axis;
        _rb.angularVelocity = rotationDifDegree * Mathf.Deg2Rad / Time.fixedDeltaTime;
        if (Vector3.Distance(transform.position, target.position) > 2f)
        {
            transform.position = target.position;
        }
    }
    
    
}
