using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    private BoxCollider2D _gridArea;

    private void Start()
    {
        _gridArea = GameObject.Find("GridArea").GetComponent<BoxCollider2D>();
        RandomizePlacement();
    }

    public void RandomizePlacement()
    {
        Bounds bounds = _gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }
}
