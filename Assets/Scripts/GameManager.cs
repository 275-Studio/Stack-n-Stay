using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera mainCamera;
    public GameObject vehicle;
    public LevelManager levelManager;
    public Vector3 vehiclePos = new Vector3(-3.4f, -0.5f, 0f);
    private bool levelFinished = false;
    public Button startVehicleButton;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (levelFinished || vehicle == null || mainCamera == null) return;
        Vector3 vehicleViewport = mainCamera.WorldToViewportPoint(vehicle.transform.position);
        if (vehicleViewport.x > 1.1f)
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
        if (vehicle != null)
            Destroy(vehicle);
        GameObject vehiclePrefab = levelManager.GetVehiclePrefab();
        if (vehiclePrefab != null)
        {
            vehicle = Instantiate(vehiclePrefab, vehiclePos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Vehicle prefab tidak tersedia di LevelData!");
        }

        levelManager.ClearPreviousItems();
    }
    public void SpawnVehicleFromLevelData()
    {
        GameObject vehiclePrefab = LevelManager.Instance.GetVehiclePrefab();
        if (vehiclePrefab == null)
        {
            Debug.LogWarning("Vehicle prefab is null!");
            return;
        }

        if (vehicle != null)
        {
            Destroy(vehicle); 
        }
        vehicle = Instantiate(vehiclePrefab);
        vehicle.transform.SetPositionAndRotation(vehiclePos, Quaternion.identity);
        VehicleController controller = vehicle.GetComponent<VehicleController>();
        if (controller != null && startVehicleButton != null)
        {
            startVehicleButton.onClick.RemoveAllListeners();

            startVehicleButton.onClick.AddListener(controller.StartVehicle);
        }
    }

}