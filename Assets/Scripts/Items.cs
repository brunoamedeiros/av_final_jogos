using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
  private GameController gameController;
  public float speed = 3f;
  private float originalY;
  public string type;
  public AudioClip sound;
  private AudioSource source;

  // Start is called before the first frame update
  void Awake()
  {

    source = GetComponent<AudioSource>();

  }
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

  // private void OnBecameInvisible()
  // {
  //   Destroy(gameObject);
  // }

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

  private void OnTriggerEnter2D(Collider2D target)
  {
    Items item = gameObject.GetComponent<Items>();

    if (target.tag == "Player")
    {
      if (item.type != "obstacle")
      {
        source.PlayOneShot(sound);
        // move the game object off screen while it finishes it's sound, then destroy it
        transform.position = Vector3.one * 9999f;
        Destroy(gameObject.gameObject, sound.length);
      }
      else if (!gameController.immunityActivated)
      {
        source.PlayOneShot(sound);
        // move the game object off screen while it finishes it's sound, then destroy it
        transform.position = Vector3.one * 9999f;
        Destroy(gameObject.gameObject, sound.length);
      }
    }
  }
}
