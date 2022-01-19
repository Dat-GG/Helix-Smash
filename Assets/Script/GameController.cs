using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] Circuts;
    public float distance = 2;
    public float ySpawn = 0;
    public int NumberofCircuts = 8;
    void Start()
    {
        for (int i = 0; i < NumberofCircuts; i++)
        {
            SpawnCircut(Random.Range(0, Circuts.Length - 1));
        }
        SpawnCircut(Circuts.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnCircut(int Circut)
    {
        Instantiate(Circuts[Circut], transform.up * ySpawn, Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up));
        ySpawn -= distance;
    }
}
