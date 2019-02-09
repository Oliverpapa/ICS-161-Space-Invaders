using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private int life = 3;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    private Rigidbody2D p_rigibody;
    public GameObject rocketPrefab;

    void Start()
    {
        p_rigibody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
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
        GameObject newRocket = Instantiate(rocketPrefab, this.transform.position, Quaternion.identity);
    }
}
