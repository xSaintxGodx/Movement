using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Vefication a the jump", order = 1)]
    [SerializeField] float groundCheckDistance;

    bool isJumping;
    bool isGrounding;

    [Header("CoyotteTIme", order = 2)]
    float coyotteTime;
    public float coyotteTimeRef;
    public float jumpForce;

    [Header("BufferingJump", order = 3)]
    public int bufferCount = 0;

    void Jumping(Rigidbody2D _rb, float yValue)
    {
        _rb.AddForce(new Vector2(_rb.velocity.x, yValue), ForceMode2D.Impulse);
    }

    public void OnJump(Rigidbody2D _rb, Animator _anim)
    {
        bool jump = Input.GetButtonDown("Jump");

        if (!isGrounding && coyotteTime > 0 || !isJumping)
        {
            Jumping(_rb, Convert.ToInt16(jump) * Time.fixedDeltaTime * jumpForce);
            bufferCount = 0;
        }
        FlexibleJump(_rb);
    }
    public void CoyotteTime()
    {
        coyotteTime -= 0.1f;
        if (isGrounding)
            coyotteTime = coyotteTimeRef;
    }
    void FlexibleJump(Rigidbody2D _rb)
    {
        if (_rb.velocity.y > 0)
        {
            if (Input.GetButtonUp("Jump"))
            {
                Jumping(_rb, _rb.velocity.y * 0.25f);
            }
        }
    }

    public void JumpBuffering(Rigidbody2D _rb)
    {
        if (Input.GetButtonDown("Jump") && _rb.velocity.y < 0)
        {
            bufferCount = 1;
        }
    }
    IEnumerator Buffering(Rigidbody2D _rb)
    {
        _rb.velocity = Vector2.zero;
        Jumping(_rb, 230 * Time.fixedDeltaTime);
        yield return new WaitUntil(() => _rb.velocity.y <= 0);
        bufferCount = 0;
    }
    
    public void GroundCheck(Rigidbody2D _rb, Animator _anim)
    {
        RaycastHit2D raycast = Physics2D.Raycast(_rb.worldCenterOfMass, Vector2.down, groundCheckDistance, 1 << 8);
        Debug.DrawRay(_rb.worldCenterOfMass, Vector2.down * groundCheckDistance, Color.cyan);

        _anim.SetInteger("JumpIndex", (int)_rb.velocity.y);
        if (raycast.collider != null)
        {
            if (bufferCount == 1)
            {
                StartCoroutine(Buffering(_rb));
            }
            else
            {
                isJumping = false;
                isGrounding = true;
            }
            _anim.SetBool("Jump", false);
        }
        else
        {
            isJumping = true;
            isGrounding = false;
            _anim.SetBool("Jump", true);
        }
    }
}

