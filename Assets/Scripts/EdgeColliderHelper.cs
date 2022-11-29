using Shapes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColliderHelper : MonoBehaviour
{
    [ContextMenu("Apply")]
    public void Apply()
    {
        Polyline polyline = GetComponent<Polyline>();
        EdgeCollider2D edgeCollider2D = GetComponent<EdgeCollider2D>();

        edgeCollider2D.points = new Vector2[] { };

        List<Vector2> points = new List<Vector2>();

        foreach (var point in polyline.points)
        {
            points.Add(point.point);
        }

        edgeCollider2D.points = points.ToArray();
    }
}
