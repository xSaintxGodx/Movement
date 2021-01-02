using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Movement movement;

    private Rigidbody2D _rb;
    void Awake()
    {
        movement = FindObjectOfType<Movement>();

        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.Move(_rb);
    }
}
