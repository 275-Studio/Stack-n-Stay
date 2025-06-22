using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject pausePanel; 
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

        winPanel.SetActive(false);
        pausePanel.SetActive(false);
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
        winPanel.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        winPanel.SetActive(false);

        GameManager.Instance.levelManager.LoadNextLevel();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        isPaused = true;

        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;                  
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPaused = false;

        Cursor.lockState = CursorLockMode.Confined; 
        Cursor.visible = true;                     
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}