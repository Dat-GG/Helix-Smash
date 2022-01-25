using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRation : MonoBehaviour
{
    public float rotationspeed;
    void Update()
    {
        this.transform.Rotate(0, rotationspeed * Time.smoothDeltaTime, 0);
    }
}
