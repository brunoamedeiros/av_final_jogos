using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBalloon : MonoBehaviour
{

  public int speed = 7;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

    // if (Input.touchCount > 0)
    // {
    //   Up();
    // }

    if (Input.GetMouseButton(0))
    {
      Up();
    }
    else
    {
      Down();
    }
  }

  private void Up()
  {
    float y = transform.position.y + (speed * Time.deltaTime);
    transform.position = new Vector3(transform.position.x, y, transform.position.z);
  }

  private void Down()
  {
    float y = transform.position.y + (speed * Time.deltaTime * -1);
    transform.position = new Vector3(transform.position.x, y, transform.position.z);
  }
}
