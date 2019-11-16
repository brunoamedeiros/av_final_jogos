using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
  public bool muted = false;
  public Sprite OffSprite;
  public Sprite OnSprite;
  public Button but;

  public void toogleSound()
  {
    AudioListener.volume = System.Convert.ToSingle(muted);
    muted = !muted;
    ChangeImage();
  }

  public void ChangeImage()
  {
    if (but.image.sprite == OnSprite)
    {
      but.image.sprite = OffSprite;
    }
    else
    {
      but.image.sprite = OnSprite;
    }
  }
}
