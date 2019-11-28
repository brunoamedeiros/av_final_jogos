using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
  public float delay = 0f;
  public Vector3 Offset = new Vector3(0, 2, 0);
  public Vector2 RandomizeIntensity = new Vector2(0.5f, 0);

  // Start is called before the first frame update
  void Start()
  {
    Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);

    transform.localPosition = new Vector2(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y));
    transform.localPosition += Offset;
  }
}
