using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakController : MonoBehaviour
{
    public float LifeTime;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke(nameof(Hide), LifeTime);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
