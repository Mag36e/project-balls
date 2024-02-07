using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float force;

    private Vector2 _closestHook;
    void Update()
    {
        if (gameObject.GetComponent<GrappleHook>().hooked)
        {
            _closestHook = gameObject.GetComponent<GrappleHook>()._closestHook;
            if (Input.GetKey(KeyCode.D) && (_closestHook.y / 2 > transform.position.y || _closestHook.x > transform.position.x))
            {
                rb.AddForce(transform.right * force);
                StartCoroutine(Wait(0.05f));
            }
            if (Input.GetKey(KeyCode.A) && (_closestHook.y / 2 > transform.position.y || _closestHook.x < transform.position.x))
            {
                rb.AddForce(transform.right * -force);
                StartCoroutine(Wait(0.05f));
            }
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
