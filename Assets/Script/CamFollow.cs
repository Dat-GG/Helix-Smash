using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float heightGap = 1f;
    private Vector3 _lastPos;
    private void Start()
    {
        _lastPos = transform.position;
    }
    private void Update()
    {
        var pos = transform.position;

        if (!(pos.y - heightGap >= player.transform.position.y)) return;
        var target = new Vector3(pos.x, player.transform.position.y + heightGap, pos.z);
        pos = Vector3.Lerp(_lastPos, target, Time.time * 0.5f);
        transform.position = pos;
    }
}
