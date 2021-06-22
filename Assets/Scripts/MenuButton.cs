using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
