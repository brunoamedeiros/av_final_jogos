using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
  public bool gameStarted;
  private int score;
  private int currentHealth = 0;
  private int maxHealth = 100;
  private TextMesh ScoreText;
  public string NewLevel = "Level 2";
  public float delay = 10;
  private Scene currentScene;
  public bool immunityActivated = false;
  public SimpleHealthBar healthBar;

  private void Awake()
  {
    GameObject go = GameObject.FindWithTag("ScoreText");
    ScoreText = go.GetComponent<TextMesh>();
  }

  // Start is called before the first frame update
  void Start()
  {
    gameStarted = false;
    currentHealth = maxHealth;
    currentScene = SceneManager.GetActiveScene();

    // if (currentScene.name == "Level 1")
    // {
    //   StartCoroutine(LoadLevelAfterDelay(delay));
    // }

    UpdateScore();
    UpdateHealth();
  }

  public void AddScore(int newScoreValue)
  {
    score += newScoreValue;
    UpdateScore();
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
      SceneManager.LoadScene(0);
      print("Game Over");
    }
  }

  public void ActiveImmunity()
  {
    StartCoroutine(Immunity());
  }

  IEnumerator Immunity()
  {
    immunityActivated = !immunityActivated;
    healthBar.UpdateColor(Color.cyan);

    yield return new WaitForSeconds(10);

    healthBar.UpdateColor(Color.green);
    immunityActivated = !immunityActivated;

    StopCoroutine("ActiveImmunity");
  }

  IEnumerator LoadLevelAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    // PlayerPrefs.SetInt("Player Score", score);
    // PlayerPrefs.SetFloat("Spawn Speed", fSpawn_Speed);

    // NewLevel = PlayerPrefs.GetString("lastLoadedScene");
    SceneManager.LoadScene(NewLevel);
    StopCoroutine("LoadLevelAfterDelay");
  }
}
