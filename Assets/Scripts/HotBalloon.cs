using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBalloon : MonoBehaviour
{

  private GameController gameController;
  public int speed = 7;
  private float originalY;
  private float minY = -2.35f;
  private float maxY = 3.76f;
  private TextMesh text;

  void Awake()
  {
    text = GameObject.Find("TouchStart").GetComponent<TextMesh>();
  }

  // Start is called before the first frame update
  void Start()
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

    if (Input.GetMouseButtonDown(0))
    {
      gameController.gameStarted = true;
      text.gameObject.SetActive(false);
    }

    if (gameController.gameStarted)
    {
      if (Input.GetMouseButton(0))
      {
        Up();
      }
      else
      {
        Down();
      }
    }
    else
    {
      Float();
    }
  }

  private void Up()
  {
    float y = transform.position.y + (speed * Time.deltaTime);
    VerifyLimitScreen(y);
  }

  private void Down()
  {
    float y = transform.position.y - (speed * Time.deltaTime);
    VerifyLimitScreen(y);
  }

  private void Float()
  {
    float floatY = originalY + 0.3f * Mathf.Sin(speed * Time.time);
    transform.position = new Vector3(transform.position.x, floatY, transform.position.z);
  }

  private void VerifyLimitScreen(float y)
  {
    Vector3 currentPosition = new Vector3(transform.position.x, y, transform.position.z);

    if (currentPosition.y < minY)
    {
      currentPosition.y = minY;
    }

    if (currentPosition.y > maxY)
    {
      currentPosition.y = maxY;
    }

    transform.position = new Vector3(transform.position.x, currentPosition.y, transform.position.z);
  }

  void OnTriggerEnter2D(Collider2D target)
  {

    // implementar com ENUM

    if (target.tag == "Item")
    {

      Items item = target.GetComponent<Items>();

      switch (item.type)
      {
        case "collectable":
          gameController.AddScore(1);
          break;
        case "immunity":
          gameController.ActiveImmunity();
          break;
        case "health":
          gameController.AddHealth();
          break;

        default:
          break;
      }

      Destroy(target.gameObject);

    }

    if (target.tag == "Obstacle")
    {
      if (gameController.immunityActivated)
      {
        print("Indestructible");
      }
      else
      {
        gameController.TakeDamage(15);
        print(target);
        Destroy(target.gameObject);
      }
    }
  }
}