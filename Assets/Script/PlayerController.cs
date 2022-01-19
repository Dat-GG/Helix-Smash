using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool ignoreNextCollision;
    public Rigidbody Player;
    public float speed = 5;
    void Start()
    {

    }

    void Update()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        Player.velocity = new Vector3(Player.velocity.x, speed, Player.velocity.z);
    }
    

}
