using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Check if player has enough food
            //If so then go to next level
            if (GameObject.Find("Food") == null)
            {
                Debug.Log("Level Completed");
                GameManager.Instance.LoadScene(GameManager.GameScenes.Level);
                AudioManager.Instance.Play("Win");
            }
            else
            {
                Debug.Log("Need More Food!");
                AudioManager.Instance.Play("Bump");
            }
        }
    }
}
