using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    [SerializeField] protected GameObject shieldPrefab;
    private List<List<GameObject>> shields = new List<List<GameObject>>();
    
    void Start()
    {
        buildShields();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buildShields()
    {
        for (int x = 0; x < 4; x++)
        {
            shields.Add(new List<GameObject>());
            for (int y = 0; y < 3; y++)
            {
                if (!(x == 1 && y == 0 || x == 2 && y == 0))
                {
                    Vector3 buildPosition = new Vector3(0.65f * x, 0.65f * y, 0);
                    GameObject newShield = Instantiate(shieldPrefab, this.transform);
                    newShield.transform.localPosition = buildPosition;
                    shields[x].Add(newShield);
                }
            }
        }
    }
}
