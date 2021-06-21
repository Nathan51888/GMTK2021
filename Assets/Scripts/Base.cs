using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{
    private TextMeshProUGUI _winText;
    
    private void Start()
    {
        _winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        _winText.enabled = false;
    }

    private IEnumerator LevelCompleted()
    {
        _winText.enabled = true;
        _winText.text = "Level Completed!";
        
        yield return new WaitForSeconds(1f);
        GameManager.Instance.LoadScene(GameManager.GameScenes.Level);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Check if player has enough food
            //If so then go to next level
            if (GameManager.Instance.totalFoodCount <= 0)
            {
                Debug.Log("Level Completed");
                AudioManager.Instance.Play("Win");
                StartCoroutine(LevelCompleted());
            }
            else
            {
                Debug.Log("Need More Food!");
                AudioManager.Instance.Play("Bump");
                _winText.enabled = true;
                _winText.text = "Need More Food! (Press R to restart)";
            }
        }
    }
}
