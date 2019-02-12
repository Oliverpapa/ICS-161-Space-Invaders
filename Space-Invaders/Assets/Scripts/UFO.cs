using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class UFO : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    public static UFO instance;
    public UnityEvent<int> bonus;
    private Camera cam;
    private float boundXL;
    private float boundXR;
    private bool startAtLeft;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        boundXL = cam.ScreenToWorldPoint(Vector3.zero).x;
        boundXR = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0f, 0f)).x;
        if (this.transform.position.x < 0)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;
            startAtLeft = true;
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed;
            startAtLeft = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.name);
        if (collider.CompareTag("Rocket"))
        {
            List<int> scores = new List<int> { 100, 150, 200, 250, 300 };
            int i = Random.Range(0, 5);
            Debug.Log(scores[i]);
            Destroy(this.gameObject);
//            if (bonus != null)
//            {
//                bonus.Invoke(scores[i]);
//            }
            LevelController.instance.UpdateScore(scores[i]);
        }
    }

    void Move()
    {
        if (this.transform.position.x <= boundXL + 1.5f && startAtLeft == false)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;
        }
        if (this.transform.position.x <= boundXL + 1.5f && startAtLeft == true)
        {
            Destroy(this.gameObject);
        }
        if (this.transform.position.x >= boundXR - 1.5f && startAtLeft == true)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed;
        }
        if (this.transform.position.x >= boundXR - 1.5f && startAtLeft == false)
        {
            Destroy(this.gameObject);
        }
    }
}
