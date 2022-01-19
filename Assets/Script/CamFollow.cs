using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newpos = Player.position + offset;
        transform.position = newpos;
    }
}
