using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    private int _playerScore = 0;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject gameOverScreen;
   
    public static LogicScript Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddScore()
    {
        _playerScore += 1;
        scoreText.text = _playerScore.ToString();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        GameStateManager.Instance.currentState = GameState.Over;
        gameOverScreen.SetActive(true);  
    }
}