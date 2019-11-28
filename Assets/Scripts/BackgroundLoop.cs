using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
  // Scroll main texture based on time

  public float scrollSpeed = 0.1f;
  Renderer rend;

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

    rend = GetComponent<Renderer>();
  }

  void Update()
  {
    if (!gameController.gameOver)
    {
      float offset = Time.time * scrollSpeed;
      rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
  }
}
