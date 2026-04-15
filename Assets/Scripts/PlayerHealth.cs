using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMax = 100f;
    public float vidaActual;
    public TextMeshProUGUI textoVida;

    void Start()
    {
        vidaActual = vidaMax;
        ActualizarUI();
    }

    public void RecibirDano(float cantidad)
    {
        vidaActual -= cantidad;
        ActualizarUI();

        if (vidaActual <= 0)
        {
            Debug.Log("Game Over");
            // aqui agregaremos la pantalla de game over despues
        }
    }

    void ActualizarUI()
    {
        if (textoVida != null)
            textoVida.text = "Vida: " + vidaActual;
    }
}