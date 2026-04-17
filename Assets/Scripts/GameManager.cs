using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public GameObject panelGameOver;

    void Awake()
    {
        instancia = this;
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Deshabilitar scripts del player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<FPSShooter>().enabled = false;
            player.GetComponent<FPSController>().enabled = false;
        }
    }

    public void Reiniciar()
    {
        Debug.Log("Reiniciando...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
{
    if (panelGameOver.activeSelf && Input.GetKeyDown(KeyCode.R))
    {
        Reiniciar();
    }
}
}