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
    private List<List<GameObject>> Enemies = new List<List<GameObject>>();
    private float timeDelay;

    // Start is called before the first frame update
    void Start()
    {
        timeDelay = 0f;
        BuildEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        CleanTheMatrix();
        timeDelay += Time.deltaTime;

        if (timeDelay > shootDelay)
        {
            int i = 0;
            i = Random.Range(0, Enemies.Count);
            bool coinFlip = Random.value <= 0.5f;
            if (coinFlip && Enemies[i].Count != 0)
            {
                Shoot(Enemies[i]);
            }
            timeDelay = 0;
        }
    }

    void Shoot(List<GameObject> gameObjects)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i] == null)
            {
                gameObjects.RemoveAt(i);
                i--;
            }
        }
        GameObject newBullet = Instantiate(bulletPrefab, gameObjects[0].transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * bulletSpeed;
        Physics2D.IgnoreCollision(gameObjects[0].GetComponent<Collider2D>(), newBullet.GetComponent<Collider2D>());
    }

    void BuildEnemies()
    {
        for (int x = 0; x < 11; x++)
        {
            Enemies.Add(new List<GameObject>());
            for (int y = 0; y < 5; y++)
            {
                Vector3 buildPosition = new Vector3(x, y, 0);
                GameObject newEnemy = Instantiate(EnemyPrefab, this.transform);
                newEnemy.transform.localPosition = buildPosition;
                Enemies[x].Add(newEnemy);
            }
        }
    }

    void CleanTheMatrix()
    {
        //Debug.Log(Enemies.Count);
        if (Enemies[0] == null)
        {
            Enemies.RemoveAt(0);
        }
        if (Enemies.Count != 0 && Enemies[Enemies.Count - 1] == null)
        {
            Enemies.RemoveAt(Enemies.Count - 1);
        }
        //Debug.Log(Enemies.Count);
    }

}
