using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float force;

    private Vector2 _closestHook;
    private Vector2 _direction;
    private bool _notWaiting = true;
    void Update()
    {
        if (gameObject.GetComponent<GrappleHook>().hooked)
        {
            _closestHook = gameObject.GetComponent<GrappleHook>()._closestHook;
            if (_notWaiting)
            {
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
    }
    

    IEnumerator Wait(float time)
    {
        _notWaiting = false;
        yield return new WaitForSeconds(time);
        _notWaiting = true;
    }
}
