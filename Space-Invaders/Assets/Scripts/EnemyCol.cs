using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class EnemyCol : MonoBehaviour
{

    [SerializeField] protected GameObject EnemyPrefab;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected GameObject UFOPrefab;
    [SerializeField] protected float shootDelay;
    [SerializeField] protected float spawnDelay;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float UFOdistanceFromTop = 1.2f;

    public UnityEvent win;
    public UnityEvent loss;

    private List<List<GameObject>> enemies = new List<List<GameObject>>();
    private float timeDelay1;
    private float timeDelay2;
    private Camera cam;
    private float boundXL;
    private float boundXR;
    private float boundYU;
    private float levelNum = 0;
    private int countDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeDelay1 = 0f;
        timeDelay2 = 0f;
        BuildEnemies();
        cam = Camera.main;
        boundXL = cam.ScreenToWorldPoint(Vector3.zero).x;
        boundXR = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,0f,0f)).x;
        boundYU = cam.ScreenToWorldPoint(new Vector3(0f,cam.pixelHeight,0f)).y;
        //Debug.Log(boundXR);
        this.transform.position = new Vector3(boundXL + 0.6f, -0.5f - levelNum, 0);
        this.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count != 0)
        {
            CleanTheMatrix();
            Move();
            if (getLowestY() < boundYU + 3.5f)
            {
                if (loss != null)
                {
                    loss.Invoke();
                }
            }
            timeDelay1 += Time.deltaTime;
            timeDelay2 += Time.deltaTime;
            if (timeDelay1 > shootDelay)
            {
                int i = 0;
                i = Random.Range(0, enemies.Count);
                bool coinFlip = Random.value <= 0.5f;
                if (coinFlip && enemies[i] != null)
                {
                    Shoot(enemies[i]);
                }
                timeDelay1 = 0;
            }
            if (timeDelay2 > spawnDelay)
            {
                spawnUFO();
                timeDelay2 = 0;
            }
        }
        else
        {
            if (win != null)
            {
                win.Invoke();
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
                countDown++;
            }
            if (enemies[enemies.Count - 1][0].transform.position.x >= boundXR - 0.5f)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed;
                this.transform.position += Vector3.down * 0.25f;
                countDown++;
            }
        }
    }

    void spawnUFO()
    {
        bool coin = Random.value <= 0.5f;
        if (coin && countDown >= 2)
        {
            bool leftRight = Random.value <= 0.5f;
            if (leftRight)
            {
                GameObject newUFO = Instantiate(UFOPrefab, new Vector3(boundXL + 1.55f, boundYU - UFOdistanceFromTop, 0), Quaternion.identity);
            }
            else
            {
                GameObject newUFO = Instantiate(UFOPrefab, new Vector3(boundXR - 1.55f, boundYU - UFOdistanceFromTop, 0), Quaternion.identity);
            }
        }
    }

    float getLowestY()
    {
        List<float> Ys = new List<float>();
        for (int x = 0; x < enemies.Count; x++)
        {
            Ys.Add(enemies[x][0].transform.position.y);
        }
        return Ys.Min();
    }
}
