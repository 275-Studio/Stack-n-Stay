using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera mainCamera;
    public GameObject truck;
    public LevelManager levelManager;
    public Vector3 truckPos = new Vector3(6.25f, 0.29f, 0f);

    private bool levelFinished = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (levelFinished || truck == null || mainCamera == null) return;
        Vector3 truckViewport = mainCamera.WorldToViewportPoint(truck.transform.position);
        if (truckViewport.x > 1.1f)
        {
            levelFinished = true;
            StartCoroutine(LevelComplete());
        }
    }

    IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(1f);
        int index = PlayerPrefs.GetInt("SelectedLevelIndex", 0);
        LevelProgress.SaveCompleted(index);
        LevelProgress.UnlockNext(index);
        PlayerPrefs.Save(); 
        ResetGame();
        levelFinished = false;
        UIManager.Instance.ShowWinPanel();
    }


    public void GameOver()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManager.Instance.ShowLosePanel();
    }

    private void ResetGame()
    {
        truck.transform.position = truckPos;
        var truckController = truck.GetComponent<TruckController>();
        if (truckController != null)
        {
            truckController.StopTruck(); 
        }
        truck.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        levelManager.ClearPreviousItems();
    }
}