using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    [SerializeField] protected GameObject shieldPrefab;
    private List<List<GameObject>> shields = new List<List<GameObject>>();
    private Camera cam;
    private float boundXL;
    private float boundXR;
    private float boundXD;

    void Start()
    {
        cam = Camera.main;
        boundXL = cam.ScreenToWorldPoint(Vector3.zero).x;
        boundXD = cam.ScreenToWorldPoint(Vector3.zero).y;
        boundXR = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0f, 0f)).x;
        for (int i = 0; i < 4; i++)
        {
            buildShields(new Vector3(boundXL + (boundXR - boundXL) / 5 * i, 0, 0));
        }
        this.transform.position = new Vector3(boundXL + (boundXR - boundXL)*2/3, boundXD + 1.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buildShields(Vector3 vector3)
    {
        for (int x = 0; x < 4; x++)
        {
            shields.Add(new List<GameObject>());
            for (int y = 0; y < 3; y++)
            {
                if (!(x == 1 && y == 0 || x == 2 && y == 0))
                {
                    Vector3 buildPosition = new Vector3(0.55f * x, 0.55f * y, 0);
                    buildPosition += vector3;
                    GameObject newShield = Instantiate(shieldPrefab, this.transform);
                    newShield.transform.localPosition = buildPosition;
                    shields[x].Add(newShield);
                }
            }
        }
    }
}
