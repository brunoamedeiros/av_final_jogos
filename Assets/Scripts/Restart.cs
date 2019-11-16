using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
  private GameController gameController;

  private void Awake()
  {
    GameObject gameControllerObject = GameObject.FindWithTag("GameController");


    if (gameControllerObject != null)
    {
      gameController = gameControllerObject.GetComponent<GameController>();
    }

    if (gameController == null)
    {
      Debug.Log("Cannot find 'GameController' script");
    }
  }

  public void reset()
  {
    // Reset Game
    GameController.score = 0;
    gameController.gameStarted = false;
    gameController.currentHealth = 0;
    gameController.maxHealth = 100;
    gameController.immunityActivated = false;
    gameController.source[1].mute = false;

    SceneManager.LoadScene(0);
  }
}
