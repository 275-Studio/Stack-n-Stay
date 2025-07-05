using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject pausePanel;
    public GameObject losePanel;
    public Image winImage;
    public Image loseImage;
    public GameObject endingPanel;
    public Sprite[] image;
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
        int index = PlayerPrefs.GetInt("SelectedLevelIndex", 0);
        int spriteIndex = GetImageIndex(index, true);

        if (winImage != null && spriteIndex >= 0 && spriteIndex < image.Length)
            winImage.sprite = image[spriteIndex];

        if (winPanel != null) winPanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        Time.timeScale = 0f;
        int index = PlayerPrefs.GetInt("SelectedLevelIndex", 0);
        int spriteIndex = GetImageIndex(index, false);

        if (loseImage != null && spriteIndex >= 0 && spriteIndex < image.Length)
            loseImage.sprite = image[spriteIndex];

        if (losePanel != null) losePanel.SetActive(true);
    }

    public void ShowEndingPanel()
    {
        Time.timeScale = 0f;
        if (endingPanel != null) endingPanel.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        if (winPanel != null) winPanel.SetActive(false);
        int index = PlayerPrefs.GetInt("SelectedLevelIndex", 0);
        LevelProgress.SaveCompleted(index);
        LevelProgress.UnlockNext(index);
        int nextIndex = index + 1;
        PlayerPrefs.SetInt("SelectedLevelIndex", nextIndex);

        LevelManager.Instance.LoadLevel(nextIndex); 
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
    private int GetImageIndex(int levelIndex, bool isWin)
    {
        if (levelIndex >= 0 && levelIndex <= 9)
            return isWin ? 0 : 1;
        else if (levelIndex >= 10 && levelIndex <= 19)
            return isWin ? 2 : 3;
        else if (levelIndex >= 20 && levelIndex <= 29)
            return isWin ? 4 : 5;
        else
            return -1;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
