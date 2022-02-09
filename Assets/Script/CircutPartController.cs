using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircutPartController : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer mr;
    public static CircutPartController circutController;
    private Collider _collider;
    [SerializeField] private float moveSpeed = 1.5f;
    public GameObject splashEffect;

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

        float force = Random.Range(25, 35);
        float torque = Random.Range(100, 180);

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
    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.CompareTag("Player"))
        {

            GameObject splash = Instantiate(splashEffect);
            splash.transform.SetParent(transform);
            splash.transform.localEulerAngles = new Vector3(90, Random.Range(0, 359), 0);
            float randomScale = Random.Range(0.15f, 0.20f);
            splash.transform.localScale = new Vector3(randomScale, randomScale, 1);
            var a = target.gameObject.transform;
            splash.transform.position = new Vector3(a.localPosition.x, a.localPosition.y - 0.36f, a.localPosition.z);
            //new Vector3(transform.localPosition.x, transform.localPosition.y + 0.5f, transform.localPosition.z);
            splash.GetComponent<SpriteRenderer>().color = GetComponent<MeshRenderer>().material.color;
        }
            //StartCoroutine(RemoveSplash());
        //}
        //IEnumerator RemoveSplash()
        //{
        //    yield return new WaitForSeconds(0.6f);
        //    Destroy(splashEffect);
        //}
    }
}
