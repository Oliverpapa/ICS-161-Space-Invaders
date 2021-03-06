﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] public int life = 3;
    //public float fireRate = 0.5f;
    //private float nextFire = 0.0f;
    private Rigidbody2D p_rigibody;
    public GameObject rocketPrefab;
    private float boundXL;
    private float boundXR;
    private float boundXD;
    private Camera cam;
    public int score;
    private int rocketNum;

    public static Player instance;

    void Start() {
        instance = this;
        
        p_rigibody = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        boundXL = cam.ScreenToWorldPoint(Vector3.zero).x;
        boundXD = cam.ScreenToWorldPoint(Vector3.zero).y;
        boundXR = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0f, 0f)).x;
        score = 0;
        this.transform.position = new Vector3((boundXR + boundXL) / 2, boundXD + 0.5f, 0);
    }

    void Update()
    {
        Move();
        rocketNum = GameObject.FindGameObjectsWithTag("Rocket").Length;
        if (Input.GetKeyDown(KeyCode.Space) && rocketNum == 0)
        {
            Fire();
        }
        //if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        //{
        //    nextFire = Time.time + fireRate;
        //    Fire();
        //}
    }

    void Move()
    {
        float movementModifier = Input.GetAxis("Horizontal");
        Vector2 currentVelocity = p_rigibody.velocity;
        if (this.transform.position.x <= boundXL + 0.5f && movementModifier < 0 ||
            this.transform.position.x >= boundXR - 0.5f && movementModifier > 0)
        {
            p_rigibody.velocity = new Vector2(0, currentVelocity.y);
        }
        else
        {
            p_rigibody.velocity = new Vector2(movementModifier * speed, currentVelocity.y);
        }
    }

    void Fire()
    {
        //Debug.Log("Fire");
        GameObject newRocket = Instantiate(rocketPrefab, this.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            life--;
            this.transform.position = new Vector3((boundXR + boundXL) / 2, boundXD + 0.5f, 0);
            LevelController.instance.UpdateLives();
            //Debug.Log(life);
        }
    }

    public void scoreIncrement()
    {
        score += 100;
    }

    public void scoreBonus()
    {
        int bonus = Random.Range(100, 200);
        score += bonus;
    }
}
