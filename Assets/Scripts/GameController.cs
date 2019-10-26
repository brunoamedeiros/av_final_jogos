using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
  public GameObject bone;
  public GameObject heath;
  public GameObject immunity;
  private static float SpawnSpeed = 2f;
  public string NewLevel = "Level 2";
  private Scene currentScene;
  private float minY = -3.1f;
  private float maxY = 2.96f;

  // Start is called before the first frame update
  void Start()
  {
    currentScene = SceneManager.GetActiveScene();

    InvokeRepeating("SpawnBalls", 0, SpawnSpeed);

    InvokeRepeating("subIncreaseSpawnSpeed", 10, 10);
  }

  void subIncrease_Spawn_Speed()
  {

    float SpeedIncrease = 0.3f;

    CancelInvoke("SpawnItems");

    if ((SpawnSpeed - SpeedIncrease) < 1)
    {
      SpawnSpeed = 0.1f;
    }
    else
    {
      SpawnSpeed = SpawnSpeed - SpeedIncrease;
    }

    InvokeRepeating("SpawnItems", 0, SpawnSpeed);
  }

  void SpawnItems()
  {
    GameObject choosedItem;

    int whichItem = Random.Range(1, 4);

    switch (whichItem)
    {
      case 1:
        choosedItem = bone;
        break;
      case 2:
        choosedItem = heath;
        break;
      case 3:
        choosedItem = immunity;
        break;
      default:
        choosedItem = immunity;
        break;
    }

    Instantiate(choosedItem, new Vector2(transform.position.x, Random.Range(minY, maxY)),
        Quaternion.identity);
  }
}
