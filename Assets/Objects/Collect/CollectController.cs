using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectController : MonoBehaviour
{
    public LayerMask LetterLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LetterLayer != (LetterLayer | (1 << collision.gameObject.layer))) return;
        SpawnPointsSetup.Instance.PickUpCollect(gameObject);
    }
}
