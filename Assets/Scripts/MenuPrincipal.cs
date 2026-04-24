using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        Debug.Log("CLICK FUNCIONA");
        SceneManager.LoadScene("Intro");
    }

    public void Opciones()
    {
        Debug.Log("Opciones (aún no implementado)");
    }

    public void Salir()
    {
        Application.Quit();
    }
}