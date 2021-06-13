using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Transform spawnPoint;
    public Transform playerMovePoint;
    public Transform playerPreviousMovePoint;
    private List<Transform> _previousMovePoint = new List<Transform>();
    private List<Transform> _segmentMovePoint = new List<Transform>();
    private List<Transform> _segments = new List<Transform>();
    private Vector3 _playerAxis = new Vector3(0,0,0);
    
    public Transform segmentsPrefab;
    public LayerMask whatStopsMovement;
    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GameManager.Instance.Respawn();
        }

        //Player Moves
        _segments[0].position = Vector3.MoveTowards(
            _segments[0].position, 
            playerMovePoint.position, 
            moveSpeed * Time.deltaTime);
        //Segment Moves
        for (int i = 1; i < _segments.Count; i++)
        {
            _segments[i].position = Vector3.MoveTowards(
                _segments[i].position,
                _segmentMovePoint[i - 1].position,
                moveSpeed * Time.deltaTime);
        }
        
        //When player has reached the move point
        if (Vector3.Distance(transform.position, playerMovePoint.position) <= 0.05)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(
                    playerMovePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 
                    .2f, whatStopsMovement))
                {
                    if (Math.Abs(playerMovePoint.position.x + Input.GetAxisRaw("Horizontal") - 
                                 _previousMovePoint[0].position.x) > 0 || _segments.Count == 1)
                    {
                        SetSegmentMovePoint();
                        playerMovePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                    }
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(
                    playerMovePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), 
                    .2f, whatStopsMovement))
                {
                    if (Math.Abs(playerMovePoint.position.y + Input.GetAxisRaw("Vertical") -
                                 _previousMovePoint[0].position.y) > 0 || _segments.Count == 1)
                    {
                        SetSegmentMovePoint();
                        playerMovePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                    }
                }            
            }
        }
    }

    private void SetSegmentMovePoint()
    {
        //When player sets new move point
        //Stores current move point for the next ant to follow
        _previousMovePoint[0].position = playerMovePoint.position;
        for (int i = 0; i < _segmentMovePoint.Count; i++)
        {
            _previousMovePoint[i + 1].position = _segmentMovePoint[i].position;
        }
        for (int i = 0; i < _segmentMovePoint.Count; i++)
        {
            _segmentMovePoint[i].position = _previousMovePoint[i].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ants") && other.GetComponent<Ants>().isRescued == false)
        {
            other.GetComponent<Ants>().isRescued = true;
            Grow(other.transform);
        }

        if (other.CompareTag("Obstacle"))
        {
            ResetState();
        }
    }
    private void Grow(Transform ant)
    {
        Debug.Log("Plus one ant");
        ant.position = playerPreviousMovePoint.position;
        
        _segments.Add(ant);
        _segmentMovePoint.Add(ant.Find("MovePoint"));
        _previousMovePoint.Add(ant.Find("PreviousMovePoint"));
        _segments[_segments.Count - 1].gameObject.layer = 3;
        _segmentMovePoint[_segmentMovePoint.Count - 1].parent = null;
        _previousMovePoint[_segments.Count - 1].parent = null;
    }
    
    private void ResetState()
    {
        transform.position = spawnPoint.position;

        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        
        _segments.Clear();
        _previousMovePoint.Clear();
        _segmentMovePoint.Clear();

        _segments.Add(transform);
        _previousMovePoint.Add(playerPreviousMovePoint);
        
        playerMovePoint.parent = null;
        _previousMovePoint[0].parent = null;
    }
}
