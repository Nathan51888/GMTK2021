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
        GameManager.Instance.totalFoodCount += totalFoodCount;
        _currentFoodCount = totalFoodCount;
        _foodCountText = GetComponentInChildren<TextMeshPro>();
        _foodCountText.text = _currentFoodCount.ToString();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ants") && _currentFoodCount > 0 || other.CompareTag("Player"))
        {
            if (other.GetComponent<AntCarry>().isCarryFood)
                return;
            
            other.GetComponent<AntCarry>().CarryFood();
            _currentFoodCount--;
            GameManager.Instance.totalFoodCount -= 1;
            _foodCountText.text = _currentFoodCount.ToString();
            
            if (_currentFoodCount <= 0)
            {
                GetComponent<SpriteRenderer>().color = Color.gray;
                gameObject.layer = LayerMask.NameToLayer("StopMovement");
            }
        }
    }
}
