using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private float boundXU;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        boundXU = cam.ScreenToWorldPoint(new Vector3(0f, cam.pixelHeight, 0f)).y;
    }

    void Update()
    {
        if (this.transform.position.y > boundXU + 0.5f)
        {
            Destroy(gameObject);
        }
        this.transform.Translate(new Vector2(0, speed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
