using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();

    public Transform segmentsPrefab;
    public int initialSize = 3;
    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D))
            _direction = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.W))
            _direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S))
            _direction = Vector2.down;
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        
        var position = transform.position;
        position = new Vector3(
            Mathf.Round( position.x) + _direction.x, 
            Mathf.Round( position.y) + _direction.y, 
            0);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            other.GetComponent<Food>().RandomizePlacement();
            Grow();
        }

        if (other.CompareTag("Obstacle"))
        {
            ResetState();
        }
    }
    
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentsPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        
        _segments.Add(segment);
    }

    public void ResetState()
    {
        _direction = Vector2.right;
        transform.position = Vector3.zero;

        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        
        _segments.Clear();
        _segments.Add(transform);
        
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }
}
