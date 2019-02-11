using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCol : MonoBehaviour
{

    [SerializeField] protected GameObject EnemyPrefab;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float shootDelay;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float moveSpeed;
    private List<List<GameObject>> enemies = new List<List<GameObject>>();
    private float timeDelay;
    private Camera cam;
    private float boundXL;
    private float boundXR;
    private float levelNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeDelay = 0f;
        BuildEnemies();
        cam = Camera.main;
        boundXL = cam.ScreenToWorldPoint(Vector3.zero).x;
        boundXR = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,0f,0f)).x;
        //Debug.Log(boundXR);
        this.transform.position = new Vector3(boundXL + 0.6f, 0 - levelNum, 0);
        this.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count != 0)
        {
            CleanTheMatrix();
            Move();
            timeDelay += Time.deltaTime;
            //Debug.Log(Enemies[0][0].transform.position.x);
            if (timeDelay > shootDelay)
            {
                int i = 0;
                i = Random.Range(0, enemies.Count);
                bool coinFlip = Random.value <= 0.5f;
                if (coinFlip && enemies[i] != null)
                {
                    Shoot(enemies[i]);
                }
                timeDelay = 0;
            }
        }
    }

    void Shoot(List<GameObject> gameObjects)
    {
        GameObject newBullet = Instantiate(bulletPrefab, gameObjects[0].transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * bulletSpeed;
        Physics2D.IgnoreCollision(gameObjects[0].GetComponent<Collider2D>(), newBullet.GetComponent<Collider2D>());
    }

    void BuildEnemies()
    {
        for (int x = 0; x < 11; x++)
        {
            enemies.Add(new List<GameObject>());
            for (int y = 0; y < 5; y++)
            {
                Vector3 buildPosition = new Vector3(x, y, 0);
                GameObject newEnemy = Instantiate(EnemyPrefab, this.transform);
                newEnemy.transform.localPosition = buildPosition;
                enemies[x].Add(newEnemy);
            }
        }
    }

    void CleanTheMatrix()
    {
        for(int x = 0; x < enemies.Count; x++)
        {
            for (int y = 0; y < enemies[x].Count; y++)
            {
                if (enemies[x][y] == null)
                {
                    enemies[x].RemoveAt(y);
                    y--;
                }
            }
            if (enemies[x].Count == 0)
            {
                enemies.RemoveAt(x);
            }
        }
        
        //Debug.Log(Enemies.Count);
    }

    void Move()
    {
        if (enemies.Count != 0)
        {
            if (enemies[0][0].transform.position.x <= boundXL + 0.5f)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;
                this.transform.position += Vector3.down * 0.25f;
            }
            if (enemies[enemies.Count - 1][0].transform.position.x >= boundXR - 0.5f)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed;
                this.transform.position += Vector3.down * 0.25f;
            }
        }
    }
}
