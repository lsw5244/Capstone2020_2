using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject controlUI;

   public void gameOver(bool isGameOver) {
        if (isGameOver == true) {
            gameOverUI.SetActive(true);
            controlUI.SetActive(false);
        } else {
            gameOverUI.SetActive(false);
        }
    }
   
}
