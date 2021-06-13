using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int totalFoodCount;
    private TextMeshPro _foodCountText;
    private int _currentFoodCount;

    private void Start()
    {
        _currentFoodCount = totalFoodCount;
        _foodCountText = GetComponentInChildren<TextMeshPro>();
        _foodCountText.text = _currentFoodCount.ToString();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ants") || other.CompareTag("Player"))
        {
            if (other.GetComponent<AntCarry>().isCarryFood)
                return;
            
            other.GetComponent<AntCarry>().CarryFood();
            _currentFoodCount--;
            _foodCountText.text = _currentFoodCount.ToString();
            
            if (_currentFoodCount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
