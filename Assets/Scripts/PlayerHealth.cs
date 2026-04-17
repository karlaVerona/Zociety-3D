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
        Debug.Log("Recibiendo daño: " + cantidad);
        vidaActual -= cantidad;
        ActualizarUI();

        if (vidaActual <= 0)
        {
            GameManager.instancia.GameOver();
        }
    }

    void ActualizarUI()
    {
        if (textoVida != null)
            textoVida.text = "Vida: " + vidaActual;
    }
}