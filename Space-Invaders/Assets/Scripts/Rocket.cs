using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    void Update()
    {
        if (this.transform.position.y > 6)
        {
            Destroy(gameObject);
        }
        this.transform.Translate(new Vector2(0, speed * Time.deltaTime));
    }
}
