using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntCarry : MonoBehaviour
{
    public SpriteRenderer foodSprite;
    public bool isCarryFood = false;

    public void CarryFood()
    {
        Debug.Log(gameObject.name + "Carry food");
        AudioManager.Instance.Play("Eat");
        isCarryFood = true;
        foodSprite.enabled = true;
    }
}
