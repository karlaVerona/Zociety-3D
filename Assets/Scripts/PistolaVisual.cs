using UnityEngine;
using System.Collections;

public class PistolaVisual : MonoBehaviour
{
    [Header("Partes de la pistola")]
    public Transform pistola;
    public Transform cargador;
    public Transform bala;

    [Header("Disparo")]
    public Vector3 retrocesoPistola = new Vector3(0f, 0f, -0.05f);
    public float velocidadRetroceso = 15f;
    public float velocidadBala = 20f;

    [Header("Recarga")]
    public Vector3 offsetCargadorSalida = new Vector3(0f, -0.3f, 0f);
    public float tiempoAnimRecarga = 1.5f;

    private Vector3 posOriginalPistola;
    private Vector3 posOriginalCargador;
    private Vector3 posOriginalBala;
    private bool animando = false;

    void Start()
    {
        posOriginalPistola = pistola.localPosition;
        posOriginalCargador = cargador.localPosition;
        posOriginalBala = bala.localPosition;
    }

    void Update()
    {
        // Retorno suave de la pistola después del retroceso
        pistola.localPosition = Vector3.Lerp(pistola.localPosition, posOriginalPistola, Time.deltaTime * velocidadRetroceso);
    }

    public void AnimarDisparo()
    {
        // Retroceso de la pistola
        pistola.localPosition = posOriginalPistola + retrocesoPistola;

        // Lanzar bala visual
        StartCoroutine(LanzarBala());
    }

    public void AnimarRecarga()
    {
        if (!animando)
            StartCoroutine(AnimacionRecarga());
    }

    IEnumerator LanzarBala()
    {
        // Crear copia de la bala
        GameObject balaDisparada = Instantiate(bala.gameObject, bala.position, bala.rotation);
        balaDisparada.transform.SetParent(null);
        balaDisparada.SetActive(true);

        // Quitar collider si tiene
        Collider col = balaDisparada.GetComponent<Collider>();
        if (col != null) Destroy(col);

        Rigidbody rb = balaDisparada.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = bala.forward * velocidadBala;

        Destroy(balaDisparada, 2f);
        yield return null;
    }

    IEnumerator AnimacionRecarga()
    {
        animando = true;
        float tiempo = 0f;
        float mitad = tiempoAnimRecarga / 2f;

        Vector3 destino = posOriginalCargador + offsetCargadorSalida;

        // Cargador sale hacia abajo
        while (tiempo < mitad)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / mitad;
            cargador.localPosition = Vector3.Lerp(posOriginalCargador, destino, t);
            yield return null;
        }

        cargador.localPosition = destino;
        tiempo = 0f;

        // Cargador regresa (nuevo cargador entra)
        while (tiempo < mitad)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / mitad;
            cargador.localPosition = Vector3.Lerp(destino, posOriginalCargador, t);
            yield return null;
        }

        cargador.localPosition = posOriginalCargador;
        animando = false;
    }
}
