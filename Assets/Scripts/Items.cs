using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Items : MonoBehaviour
{
  public float dropChance;
  private float speed = 3f;
  private float originalY;
  private GameController gameController;

  private void Start()
  {
    originalY = transform.position.y;

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

  // Update is called once per frame
  void Update()
  {
    Movement();
    Float();
  }

  private void OnBecameInvisible()
  {
    Destroy(gameObject);
  }

  private void Movement()
  {
    float x = transform.position.x - (speed * Time.deltaTime);
    transform.position = new Vector3(x, transform.position.y, transform.position.z);
  }

  private void Float()
  {
    float floatY = originalY + 0.3f * Mathf.Sin(speed * Time.time);
    transform.position = new Vector3(transform.position.x, floatY, transform.position.z);
  }
}
