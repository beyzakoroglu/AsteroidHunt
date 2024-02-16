using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
   public Player player;
   public float respawnTime = 3.0f;
   public int lives = 3;
   private int score;

   public GameObject scoreObj;
   private TextMeshProUGUI scoreText;

   public GameObject gameOverScreen;
   AudioManager audioManager;

     private void Awake()
     {
          audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
          if(SceneManager.GetActiveScene().buildIndex == 1){
               scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
          }
     }

     public void GetScore(Asteroid asteroid)
     {
          if(asteroid.size <= 0.75f) {
               score += 100; 
          } else if (asteroid.size <= 1.0f) {
               score += 50;
          } else {
               score += 25;
          }

          scoreText.text = score.ToString();

          //Debug.Log("Score: " + score);
     }
     public void PlayerDied()
     {
          lives--;
          Debug.Log("Lives: " + lives);
          audioManager.playClip(audioManager.die);

          if (lives <= 0)
          {
               GameOver();
          } else {
               Invoke("Respawn", respawnTime);
          }        
     }

     private void Respawn()
     {
          audioManager.playClip(audioManager.respawn);
          player.transform.position = Vector3.zero;
          player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
          player.gameObject.SetActive(true);
          Invoke("TurnOnCollisions", 3.0f);    //after 3 seconds the layer of player will change

     }

     private void TurnOnCollisions()
     {
          player.gameObject.layer = LayerMask.NameToLayer("Player");
     }

     private void GameOver()
     {
          
          Debug.Log("GAME OVER!");
          gameOverScreen.SetActive(true);
     }

     public void RestartGame()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }

     public void StartGame()
     {

          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
          //audioManager.playClip(audioManager.background2);
     }

     public void Quit()
     {
          Application.Quit();
     }


}
