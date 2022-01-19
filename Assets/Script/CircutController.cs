using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircutController : MonoBehaviour
{
    [SerializeField] private CircutPartController[] circut = null;
    public void BreakAllCircuts()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        foreach (CircutPartController c in circut)
        {
            c.BreakingPlatforms();
        }

        StartCoroutine(RemoveParts());
    }

    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
