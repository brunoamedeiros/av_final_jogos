using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
  private static float SpawnSpeed = 3f;
  public string NewLevel = "Level 2";
  private Scene currentScene;
  private float minY = -2.35f;
  private float maxY = 3.76f;
  public bool gameStart;
  public GameObject collectable;
  public GameObject health;
  public GameObject immunity;

  // Start is called before the first frame update
  void Start()
  {
    currentScene = SceneManager.GetActiveScene();

    InvokeRepeating("SpawnItems", 0, SpawnSpeed);

    // InvokeRepeating("subIncreaseSpawnSpeed", 30, 30);

    gameStart = false;
  }

  void subIncrease_Spawn_Speed()
  {

    // float SpeedIncrease = 0.3f;

    CancelInvoke("SpawnItems");

    // if ((SpawnSpeed - SpeedIncrease) < 1)
    // {
    //   SpawnSpeed = 0.1f;
    // }
    // else
    // {
    //   SpawnSpeed = SpawnSpeed - SpeedIncrease;
    // }

    InvokeRepeating("SpawnItems", 0, SpawnSpeed);
  }

  void SpawnItems()
  {
    bool spawned = false;
    int randomChance = Random.Range(0, 100);

    Debug.Log(randomChance);

    GameObject choosedItem = null;

    if (randomChance < 10)
    {
      choosedItem = immunity;
      spawned = !spawned;
    }
    else if (randomChance < 25)
    {
      choosedItem = health;
      spawned = !spawned;
    }
    else if (randomChance < 60)
    {
      choosedItem = collectable;
      spawned = !spawned;
    }

    if (spawned)
    {
      Instantiate(choosedItem, new Vector2(transform.position.x, Random.Range(minY, maxY)),
            Quaternion.identity);
      spawned = !spawned;
    }
  }


}
