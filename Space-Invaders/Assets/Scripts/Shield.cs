using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    private int life;

    void Start()
    {
        life = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnemyBullet") || collider.CompareTag("Rocket"))
        {
            this.transform.localScale -= new Vector3(0, 0.2f, 0);
            life--;
        }
    }
}
