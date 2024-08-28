using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


   private bool IsGameOver;
   [SerializeField]
   private GameObject PauseMenu;
   private Animator PauseAnimation;
    internal static bool _gameManager;
  

  public void Update()
   {
    if(Input.GetKeyDown(KeyCode.R)&& IsGameOver==true)
    {
      SceneManager.LoadScene(1); //current game scene
    }
    if(Input.GetKeyDown(KeyCode.Escape))
    {
      SceneManager.LoadScene(0); 
    }
    if (Input.GetKeyDown(KeyCode.P))
    {
      PauseMenu.SetActive(true);
   
      Time.timeScale = 0;
    }
   }
   public void GameOver()
   {

    IsGameOver=true;
   }
   public void ResumeGame()
   {
    PauseMenu.SetActive(false);
        Time.timeScale =1;
   }
 

}
