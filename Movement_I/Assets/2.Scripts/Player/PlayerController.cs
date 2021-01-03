using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Mechanics
    Movement movement;
    Jump jump;

    #endregion

    Rigidbody2D _rb;
    Animator _anim;

    void Awake()
    {
        movement = FindObjectOfType<Movement>();
        jump = FindObjectOfType<Jump>();

        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

    }

    void Update()
    {
        movement.Move(_rb, _anim);

        #region Jump
        jump.OnJump(_rb, _anim);
        jump.CoyotteTime();
        jump.JumpBuffering(_rb);
        #endregion
        jump.GroundCheck(_rb, _anim);
        if (_rb.velocity.y > 0 || _rb.velocity.y < 0 || _rb.velocity.x > 0 || _rb.velocity.x < 0)
            _anim.SetBool("isIdle", false);
        else
        {
            _anim.SetBool("isIdle", true);
        }
    }

}
