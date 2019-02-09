using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCol : MonoBehaviour
{

    [SerializeField] protected GameObject EnemyPrefeb;
    [SerializeField] protected GameObject bulletPrefeb;
    [SerializeField] protected int shootDelay;
    private List<List<Enemy>> Enemies = new List<List<Enemy>>();
    private float timeDelay;

    // Start is called before the first frame update
    void Start()
    {
        timeDelay = 0f;
        EnemiesLen = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timeDelay += timeDelay.time;

        if (timeDelay == shootDelay)
        {
            for (int i = 0; i < Enemies.Count; ++i)
            {
                bool coinFlip = Random.value <= 0.5f;
                if (coinFlip && Enemies[i].Count)
                {
                    Shoot();
                }
                timeDelay = 0;
            }
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(pelletPrefab, Enemies[Enemies.Count - 1].transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = Vector2.down;
        Physics2D.IgnoreCollision(m_collider, newBullet.GetComponent<Collider2D>());
    }

    void BuildEnemies()
    {
        for (int x = 0; x < 11; x++)
        {
            Enemies.Add(new List<Enemy>());
            for (int y = 0; y < 5; y++)
            {
                Vector3 buildPosition = new Vector3(x, y, 0);
                GameObject newEnemy = Instantiate(EnemyPrefab, this.transform);
                newEnemy.transform.localPosition = buildPosition;
                Enemy newTileComponent = newEnemy.GetComponent<TileComponent>();
                newTileComponent.Init(x, y);

                newTileComponent.OnClickedEvent.AddListener(FlipTilesAdjacentTo);

                tileGrid[x].Add(newTileComponent);
            }
        }
    }

}
