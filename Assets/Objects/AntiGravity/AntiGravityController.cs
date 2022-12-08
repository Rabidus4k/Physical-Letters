using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravityController : MonoBehaviour
{
    public LayerMask LetterLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LetterLayer != (LetterLayer | (1 << collision.gameObject.layer))) return;
        collision.GetComponent<Rigidbody2D>().gravityScale *= -1f;
        gameObject.SetActive(false);
    }
}
