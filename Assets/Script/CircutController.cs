using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircutController : MonoBehaviour
{
    [SerializeField] private CircutPartController[] parts = null;
    public void BreakAllCircuts()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        foreach (CircutPartController c in parts)
        {
            c.BreakingCircuts();
            FindObjectOfType<PlayerController>().PlusScore();
        }

        StartCoroutine(RemoveParts());
    }

    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
