using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBalloon : MonoBehaviour
{

  public int speed = 7;
  private float originalY;
  private bool gameStart;
  private float minY = -3.1f;
  private float maxY = 2.96f;

  // Start is called before the first frame update
  void Start()
  {
    originalY = transform.position.y;
    gameStart = false;
  }

  // Update is called once per frame
  void Update()
  {

    if (Input.GetMouseButtonDown(0))
    {
      gameStart = true;
    }

    if (gameStart)
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
    float y = transform.position.y + (speed * Time.deltaTime * -1);
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
}