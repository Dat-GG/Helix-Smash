using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircutPartController : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer mr;
    public static CircutPartController circutController;
    private Collider _collider;
    [SerializeField] private float moveSpeed = 4.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        circutController = transform.parent.GetComponent<CircutPartController>();
        _collider = GetComponent<Collider>();
    }

    public void BreakingCircuts()
    {
        rb.isKinematic = false;
        _collider.enabled = false; 
        Vector3 forcePoint = transform.parent.position; 
        float parentXPosition = transform.parent.position.x; 
        float xPos = mr.bounds.center.x; 

        Vector3 subDirection = (parentXPosition - xPos < 0) ? Vector3.right : Vector3.left; 
        Vector3 direction = (Vector3.up * moveSpeed + subDirection).normalized;

        float force = Random.Range(800, 1000);
        float torque = Random.Range(120, 180);

        rb.AddForceAtPosition(direction * force, forcePoint, ForceMode.Impulse);  
        rb.AddTorque(Vector3.left * torque); 
        rb.velocity = Vector3.down;
    }

    public void RemoveAllChildCircuts()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).SetParent(null);
            i--;
        }
    }
}
