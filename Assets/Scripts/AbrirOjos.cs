using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbrirOjos : MonoBehaviour
{
    public Image pantallaOscura;
    public float tiempoParpadeo = 0.3f;
    public float tiempoFinal = 2f;
    public SistemaDialogos sistemaDialogos;

    void Start()
    {
        StartCoroutine(SecuenciaOjos());
    }

    IEnumerator SecuenciaOjos()
    {
        SetAlpha(1f);
        yield return new WaitForSecondsRealtime(0.5f);

        // Parpadeo 1
        yield return StartCoroutine(FadeTo(0.3f, tiempoParpadeo));
        yield return StartCoroutine(FadeTo(1f, tiempoParpadeo));
        yield return new WaitForSecondsRealtime(0.3f);

        // Parpadeo 2
        yield return StartCoroutine(FadeTo(0.1f, tiempoParpadeo));
        yield return StartCoroutine(FadeTo(1f, tiempoParpadeo));
        yield return new WaitForSecondsRealtime(0.3f);

        // Abre completamente
        yield return StartCoroutine(FadeTo(0f, tiempoFinal));

        // Inicia dialogos
        if (sistemaDialogos != null)
            sistemaDialogos.IniciarDialogo();
    }

    IEnumerator FadeTo(float targetAlpha, float tiempo)
    {
        float startAlpha = pantallaOscura.color.a;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / tiempo;
            SetAlpha(Mathf.Lerp(startAlpha, targetAlpha, t));
            yield return null;
        }
    }

    void SetAlpha(float alpha)
    {
        Color color = pantallaOscura.color;
        color.a = alpha;
        pantallaOscura.color = color;
    }
}