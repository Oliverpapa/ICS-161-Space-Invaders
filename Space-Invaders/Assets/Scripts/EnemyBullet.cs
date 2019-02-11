using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    private float boundXD;

    void Start()
    {
        cam = Camera.main;
        boundXD = cam.ScreenToWorldPoint(Vector3.zero).y;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < boundXD - 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
