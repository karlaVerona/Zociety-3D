using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public string nombreEscena;
    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }

void OnTriggerEnter(Collider other)
{
    Debug.Log("Trigger con: " + other.name);
    if (other.CompareTag("Player"))
        jugadorCerca = true;
}

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = false;
    }
}