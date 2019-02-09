using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Rigidbody2D p_rigibody;

    void Start()
    {
        p_rigibody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Move()
    {
        float movementModifier = Input.GetAxis("Horizontal");
        Vector2 currentVelocity = p_rigibody.velocity;
        p_rigibody.velocity = new Vector2(movementModifier * speed, currentVelocity.y);
    }

    void Fire()
    {
        Debug.Log("Fire");
    }
}
