using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerController : MonoBehaviour
{
    public LayerMask LetterLayer;
    public Color Color;

    
    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = Color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LetterLayer != (LetterLayer | (1 << collision.gameObject.layer))) return;
        collision.GetComponent<SpriteRenderer>().color = Color;
        gameObject.SetActive(false);
    }
}
