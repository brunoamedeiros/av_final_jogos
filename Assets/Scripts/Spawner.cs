using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  public static float SpawnSpeed = 1.0f;
  private float minY = -2.35f;
  private float maxY = 3.76f;
  public GameObject collectable;
  public GameObject health;
  public GameObject immunity;
  public GameObject bird;
  private GameController gameController;

  // Start is called before the first frame update
  void Start()
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

    InvokeRepeating("SpawnItems", 0, SpawnSpeed);
    InvokeRepeating("subIncreaseSpawnSpeed", 15, 15);
  }

  void subIncreaseSpawnSpeed()
  {
    if (gameController.gameStarted)
    {
      float SpeedIncrease = 0.3f;

      CancelInvoke("SpawnItems");

      if ((SpawnSpeed - SpeedIncrease) < 0.1)
      {
        SpawnSpeed = 0.1f;
      }
      else
      {
        SpawnSpeed = SpawnSpeed - SpeedIncrease;
      }

      InvokeRepeating("SpawnItems", 0, SpawnSpeed);
    }
  }

  void SpawnItems()
  {
    if (gameController.gameStarted && !gameController.gameOver)
    {
      int randomChanceItemOrObstacle = Random.Range(0, 100);
      int randomChanceItem = Random.Range(0, 100);
      int randomChanceObstacle = Random.Range(0, 100);

      GameObject choosedItem = null;
      Vector2 position = new Vector2(transform.position.x, Random.Range(minY, maxY));
      Quaternion rotation = Quaternion.identity;

      if (randomChanceItemOrObstacle <= 20)
      {
        // Items
        if (randomChanceItem < 10)
        {
          choosedItem = immunity;
          Spawn(choosedItem, position, rotation);
        }
        else if (randomChanceItem < 25)
        {
          choosedItem = health;
          Spawn(choosedItem, position, rotation);
        }
        else if (randomChanceItem < 60)
        {
          choosedItem = collectable;
          Spawn(choosedItem, position, rotation);
        }
      }
      else if (randomChanceItemOrObstacle >= 80)
      {
        choosedItem = bird;
        Spawn(choosedItem, position, rotation);
      }
    }
  }

  void Spawn(GameObject item, Vector2 position, Quaternion rotation)
  {
    Instantiate(item, position, rotation);
  }
}
