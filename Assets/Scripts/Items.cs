using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
  private float speed = 3f;
  private float originalY;
  public string type;

  private void Start()
  {
    originalY = transform.position.y;
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
