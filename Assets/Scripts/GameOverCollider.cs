using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
  private GameController gameController;

  private void Start()
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

  private void OnTriggerEnter2D(Collider2D target)
  {
    StartCoroutine(gameController.GameOver());
  }
}
