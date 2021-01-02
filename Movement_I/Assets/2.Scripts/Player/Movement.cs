using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;

    bool forward = true;
    
    public void Move(Rigidbody2D _rb)
    {
        float horizontal = Input.GetAxis("Horizontal");

        _rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, _rb.velocity.y);
        if (horizontal > 0f)
        {
            if(!forward)
                Flip();
        }
        else if (horizontal < 0f)
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

