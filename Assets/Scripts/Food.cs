using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    public int totalFoodCount;
    private int currentFoodCount;

    private void Start()
    {
        currentFoodCount = totalFoodCount;
    }

    private void Update()
    {
        if (currentFoodCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ants") || other.CompareTag("Player"))
        {
            if (other.GetComponent<AntCarry>().isCarryFood)
                return;
            
            other.GetComponent<AntCarry>().CarryFood();
            currentFoodCount--;
        }
    }
}
