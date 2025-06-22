using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera mainCamera;
    public GameObject truck;

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

        // Cek jika truk sudah sepenuhnya keluar dari kanan layar
        if (truckViewport.x > 1.1f)
        {
            levelFinished = true;
            StartCoroutine(LevelComplete());
        }
    }

    IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Level Selesai!");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
    }
}
