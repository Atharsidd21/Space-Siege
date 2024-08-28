using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uimanager : MonoBehaviour
{
    [SerializeField]
    public Text ScoreText , BestText;
    
    
    
     [SerializeField]
    private Image LivesImage;
    [SerializeField]
    private Sprite[] livesSprites; 
    [SerializeField]
    private Text GameOverText;
    [SerializeField]
    private Text RestartText;
    private GameManager Game_Manager;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "Score:" + 0;
        GameOverText.gameObject.SetActive(false);
        Game_Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (Game_Manager != null)
        {
            Debug.LogError("GM is Null");
        }
    }

   public void UpdateScore(int playerScore)
   {
     ScoreText.text = "Score:" + playerScore;
   }
   //High Score
   
   public void UpdateLives(int currentLives)
   {
     //display sprite 
     LivesImage.sprite = livesSprites[currentLives];

     if(currentLives==0)
     {
       GameOverSequence();

     }
   }
   void GameOverSequence()
   {
  
    Game_Manager.GameOver();
      GameOverText.gameObject.SetActive(true);
      RestartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
   }
     IEnumerator GameOverFlickerRoutine()
     {
        while(true)
        {

        GameOverText.text = "GAME OVER!";
        yield return new WaitForSeconds(0.5f);
        GameOverText.text = "";
        yield return new WaitForSeconds(0.5f);    
        }
     }
    //ResumePlay
     public void ResumePlay()
     {
       GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
       gm.ResumeGame();
     }
     //Back To Main Menu
     public void BackToMainMenu()
     {
       SceneManager.LoadScene(0);
     }

   
}
