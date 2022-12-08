using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorZoneController : MonoBehaviour
{
    public LayerMask LetterLayer;

    public Collider2D col;
    public Color Color;
    
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (LetterLayer != (LetterLayer | (1 << collision.gameObject.layer))) return;
        if (collision.gameObject.GetComponent<SpriteRenderer>().color != Color)
            Physics2D.IgnoreCollision(collision, col, true);
    }
}
