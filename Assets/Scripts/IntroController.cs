using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroController : MonoBehaviour
{
    [Header("Referencias")]
    public TextMeshProUGUI texto;
    public CanvasGroup logo;

    [Header("Animaciones")]
    public float velocidadParpadeo = 2f;
    public float velocidadFade = 1f;

    [Header("Escena")]
    public string siguienteEscena = "MenuPrincipal";

    void Update()
    {
        float alphaTexto = Mathf.Abs(Mathf.Sin(Time.time * velocidadParpadeo));
        Color c = texto.color;
        c.a = alphaTexto;
        texto.color = c;

        if (logo.alpha < 1)
        {
            logo.alpha += Time.deltaTime * velocidadFade;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(siguienteEscena);
        }
    }
}