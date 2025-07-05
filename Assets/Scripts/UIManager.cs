using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject pausePanel;
    public GameObject losePanel;
    public static UIManager Instance;
    public bool isPaused = false;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        if (winPanel != null) winPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void ShowWinPanel()
    {
        Time.timeScale = 0f;
        if (winPanel != null) winPanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        Time.timeScale = 0f;
        if (losePanel != null) losePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        if (winPanel != null) winPanel.SetActive(false);
        int index = PlayerPrefs.GetInt("SelectedLevelIndex", 0);
        LevelProgress.SaveCompleted(index);
        LevelProgress.UnlockNext(index);
        SceneManager.LoadScene("SelectLevel");
    }

    public void GoToSelectLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SelectLevel");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (pausePanel != null) pausePanel.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;                  
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (pausePanel != null) pausePanel.SetActive(false);
        isPaused = false;

        Cursor.lockState = CursorLockMode.Confined; 
        Cursor.visible = true;                     
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        int index = PlayerPrefs.GetInt("SelectedLevelIndex", 0);
        SceneManager.LoadScene("Main");
    }
}
