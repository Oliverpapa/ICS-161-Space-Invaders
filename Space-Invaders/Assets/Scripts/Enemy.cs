using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntUnityEvent : UnityEvent<int> { }

public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    private Collider2D m_collider;
    public UnityEvent<int> getKill;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        m_collider = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.name);
        if (collider.CompareTag("Rocket"))
        {
            int score = 0;
            int rowNum = (int)this.transform.localPosition.y;
            switch (rowNum)
            {
                case 0:
                    score = 10;
                    break;
                case 1:
                    score = 10;
                    break;
                case 2:
                    score = 20;
                    break;
                case 3:
                    score = 20;
                    break;
                case 4:
                    score = 40;
                    break;
            }
            Destroy(this.gameObject);
            if (getKill != null)
            {
                getKill.Invoke(score);
            }
        }
    }
}
