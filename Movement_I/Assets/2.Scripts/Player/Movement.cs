using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float horizontalInput;
    public float speed;

    bool forward = true;
    
    public void Move(Rigidbody2D _rb, Animator _anim)
    {
        horizontalInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(horizontalInput * Time.fixedDeltaTime * speed, _rb.velocity.y);

        _anim.SetInteger("Movement", (int)_rb.velocity.x);
        _anim.SetBool("isIdle", false);

        if (horizontalInput == 0 & Input.GetButtonDown("Horizontal") == false)
            _anim.SetBool("isIdle", true);

        checkFlip();
    }

    void checkFlip()
    {
        if (horizontalInput > 0f)
        {
            if(!forward)
                Flip();
        }
        else if (horizontalInput < 0f)
        {
            if(forward)
                Flip();
        }
    }
    void Flip()
    {
        forward = !forward;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

