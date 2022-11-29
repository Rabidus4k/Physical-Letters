using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnPointsSetup.Instance.PickUpCollect(collision.gameObject);
    }
}
