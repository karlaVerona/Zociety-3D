using UnityEngine;
using UnityEngine.UI;

public class LiquidoToxico : MonoBehaviour
{
    public float danoPorSegundo = 10f;
    public Image pantallaVerde;
    public float velocidadParpadeo = 3f;

    private bool enLiquido = false;

    void Update()
    {
        if (pantallaVerde != null)
        {
            if (enLiquido)
            {
                float alpha = (Mathf.Sin(Time.time * velocidadParpadeo) + 1) / 2;
                Color color = pantallaVerde.color;
                color.a = alpha * 0.5f;
                pantallaVerde.color = color;
            }
            else
            {
                Color color = pantallaVerde.color;
                color.a = 0f;
                pantallaVerde.color = color;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enLiquido = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enLiquido = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().RecibirDano(danoPorSegundo * Time.deltaTime);
        }
    }
}