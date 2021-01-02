using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Movement movement;

    private Rigidbody2D _rb;
    Animator _anim;
    void Awake()
    {
        movement = FindObjectOfType<Movement>();

        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        movement.Move(_rb, _anim);
    }
}
