using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotBalloon : MonoBehaviour
{

  private GameController gameController;
  public int speed = 7;
  private float originalY;
  private float minY = -2.35f;
  private float maxY = 3.76f;
  private TextMesh text;
  public AudioClip sound;
  public AudioSource source;
  private GameObject fire;

  void Awake()
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

    if (gameController.currentScene.name == "Level 1")
    {
      text = GameObject.Find("TouchStart").GetComponent<TextMesh>();
    }

    source = GetComponent<AudioSource>();
  }

  // Start is called before the first frame update
  void Start()
  {
    originalY = transform.position.y;

    fire = GameObject.Find("Fire");
    fire.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {

    bool buttonDown = Input.GetMouseButtonDown(0);

    if (EventSystem.current.currentSelectedGameObject &&
      EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
    {
      buttonDown = false;
    }

    if (buttonDown)
    {
      gameController.gameStarted = true;

      if (gameController.currentScene.name == "Level 1")
      {
        text.gameObject.SetActive(false);
      }

      if (!gameController.gameOver)
      {
        source.PlayOneShot(sound);
      }
    }

    if (!gameController.gameOver)
    {
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
  }


  private void Up()
  {
    float y = transform.position.y + (speed * Time.deltaTime);
    fire.SetActive(true);
    VerifyLimitScreen(y);
  }

  private void Down()
  {
    float y = transform.position.y - (speed * Time.deltaTime);
    fire.SetActive(false);
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

    Items item = target.GetComponent<Items>();

    if (target.tag == "Item")
    {
      switch (item.type)
      {
        case "collectable":
          gameController.AddScore(3);
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
    }

    if (target.tag == "Obstacle" && !gameController.immunityActivated)
    {
      int damage = (gameController.currentScene.name == "Level 1") ? 30 : 45;
      gameController.TakeDamage(damage);
    }
  }
}