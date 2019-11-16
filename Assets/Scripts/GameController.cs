using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public bool gameStarted;
  public static int score;
  public int currentHealth = 0;
  public int maxHealth = 100;
  private TextMesh ScoreText;
  public int NewLevel = 1;
  public float delay = 10;
  public Scene currentScene;
  public bool immunityActivated = false;
  public SimpleHealthBar healthBar;
  private float timer;
  public AudioClip gameOverSound;
  public AudioSource[] source;
  public GameObject gameOverPanel;
  public bool gameOver = false;

  private void Awake()
  {
    GameObject go = GameObject.FindWithTag("ScoreText");
    ScoreText = go.GetComponent<TextMesh>();
    source = GetComponents<AudioSource>();
    currentScene = SceneManager.GetActiveScene();
    gameOverPanel.SetActive(false);
    ScoreText.gameObject.SetActive(false);
  }

  // Start is called before the first frame update
  void Start()
  {
    if (currentScene.name == "Level 1")
    {
      gameStarted = false;
    }

    currentHealth = maxHealth;

    UpdateScore();
    UpdateHealth();
  }

  void Update()
  {
    if (gameStarted && !gameOver)
    {
      AddScoreByTime();
      ScoreText.gameObject.SetActive(true);
    }
  }

  public void AddScore(int newScoreValue)
  {
    score += newScoreValue;
    UpdateScore();
  }

  public void AddScoreByTime()
  {
    timer += Time.deltaTime;

    if (timer > 3f)
    {

      score += 1;

      UpdateScore();

      //Reset the timer to 0.
      timer = 0;
    }

    if (score >= 5 && currentScene.name == "Level 1")
    {
      SceneManager.LoadScene(1);
    }
  }

  public void RemoveScore(int newScoreValue)
  {
    score -= newScoreValue;
    UpdateScore();
  }

  public void UpdateScore()
  {
    ScoreText.text = score.ToString();
  }

  public void AddHealth()
  {
    // Increase the current health by 25%.
    currentHealth += (maxHealth / 4);

    // If the current health is greater than max, then set it to max.
    if (currentHealth > maxHealth)
      currentHealth = maxHealth;

    // Update the Simple Health Bar with the new Health values.
    UpdateHealth();
  }

  public void TakeDamage(int damage)
  {
    currentHealth -= damage;
    UpdateHealth();
  }

  public void UpdateHealth()
  {
    // Now is where you will want to update the Simple Health Bar. Only AFTER the value has been modified.
    healthBar.UpdateBar(currentHealth, maxHealth);

    if (currentHealth < 0)
    {
      GameOver();
    }
  }

  public void GameOver()
  {
    source[1].mute = true;
    source[0].PlayOneShot(gameOverSound);
    gameOverPanel.SetActive(true);
    gameOver = true;
  }

  public void ActiveImmunity()
  {
    StartCoroutine(Immunity());
  }

  IEnumerator Immunity()
  {
    immunityActivated = !immunityActivated;
    healthBar.UpdateColor(new Color32(0, 94, 144, 255));

    yield return new WaitForSeconds(10);

    healthBar.UpdateColor(new Color32(47, 144, 0, 255));
    immunityActivated = !immunityActivated;

    StopCoroutine("ActiveImmunity");
  }
}
