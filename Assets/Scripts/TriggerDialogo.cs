using UnityEngine;

public class TriggerDialogo : MonoBehaviour
{
    private SistemaDialogos sistema;
    private bool yaActivado = false;

    [Header("Activación")]
    public bool iniciarAutomatico = true;
    public float retrasoInicio = 0.5f;

    void Start()
    {
        sistema = GetComponent<SistemaDialogos>();

        if (iniciarAutomatico)
        {
            Invoke("ActivarDialogo", retrasoInicio);
        }
    }

    void ActivarDialogo()
    {
        sistema.IniciarDialogo();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!iniciarAutomatico && other.CompareTag("Player") && !yaActivado)
        {
            yaActivado = true;
            sistema.IniciarDialogo();
        }
    }
}
