using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

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

   [Header("Efecto Daño")]
public Image pantallaRoja;
private Coroutine efectoDano;

public void RecibirDano(float cantidad)
{
    Debug.Log("Recibiendo daño: " + cantidad);
    vidaActual -= cantidad;
    ActualizarUI();

    if (pantallaRoja != null)
    {
        if (efectoDano != null) StopCoroutine(efectoDano);
        efectoDano = StartCoroutine(ParpadeaRojo());
    }

    if (vidaActual <= 0)
    {
        GameManager.instancia.GameOver();
    }
}

IEnumerator ParpadeaRojo()
{
    Color color = pantallaRoja.color;
    color.a = 0.4f;
    pantallaRoja.color = color;

    yield return new WaitForSeconds(0.1f);

    color.a = 0f;
    pantallaRoja.color = color;
}
    void ActualizarUI()
    {
        if (textoVida != null)
            textoVida.text = "Vida: " + vidaActual;
    }
}