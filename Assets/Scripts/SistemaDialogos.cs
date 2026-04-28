using UnityEngine;
using TMPro;
using System.Collections;

public class SistemaDialogos : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;

    [Header("Configuración")]
    public float velocidadTexto = 0.04f;

    [Header("Mensajes")]
    [TextArea(2, 5)]
    public string[] mensajes;

    private int indiceActual = 0;
    private bool escribiendo = false;
    private bool dialogoActivo = false;
    private Coroutine coroutinaTexto;

    void Start()
    {
        if (panelDialogo != null)
            panelDialogo.SetActive(false);
    }

    void Update()
    {
        if (!dialogoActivo) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (escribiendo)
            {
                StopCoroutine(coroutinaTexto);
                textoDialogo.text = mensajes[indiceActual];
                escribiendo = false;
            }
            else
            {
                indiceActual++;
                if (indiceActual < mensajes.Length)
                {
                    coroutinaTexto = StartCoroutine(EscribirTexto(mensajes[indiceActual]));
                }
                else
                {
                    CerrarDialogo();
                }
            }
        }
    }

    public void IniciarDialogo()
    {
        if (mensajes == null || mensajes.Length == 0) return;

        dialogoActivo = true;
        indiceActual = 0;
        panelDialogo.SetActive(true);

        Time.timeScale = 0f;

        FPSController fps = GetComponent<FPSController>();
        if (fps != null) fps.enabled = false;

        FPSShooter shooter = GetComponent<FPSShooter>();
        if (shooter != null) shooter.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        coroutinaTexto = StartCoroutine(EscribirTexto(mensajes[0]));
    }

    public void CerrarDialogo()
    {
        dialogoActivo = false;
        panelDialogo.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Reactivar shooter
        FPSShooter shooter = GetComponent<FPSShooter>();
        if (shooter != null) shooter.enabled = true;

        // Iniciar animacion de levantarse
        FPSController fps = FindObjectOfType<FPSController>();
        if (fps != null)
            fps.IniciarLevantarse();
    }

    IEnumerator EscribirTexto(string mensaje)
    {
        escribiendo = true;
        textoDialogo.text = "";

        foreach (char letra in mensaje)
        {
            textoDialogo.text += letra;
            yield return new WaitForSecondsRealtime(velocidadTexto);
        }

        escribiendo = false;
    }
}