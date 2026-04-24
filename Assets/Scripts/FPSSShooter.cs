using UnityEngine;
using TMPro;

public class FPSShooter : MonoBehaviour
{
    [Header("Disparo")]
    public float alcance = 200f;
    public float dano = 25f;
    public float fireRate = 0.3f;
    public int municionMax = 30;
    public int municionActual;

    [Header("Proyectil")]
    public float velocidadProyectil = 80f;
    public float tiempoVidaProyectil = 3f;
    public Vector3 offsetMano = new Vector3(0.3f, -0.2f, 0.5f); // offset mano derecha

    [Header("Efectos")]
    public ParticleSystem muzzleFlash;
    public GameObject impactoEfecto;

    [Header("Crosshair")]
    public float tamañoCrosshair = 4f;
    public float grosorCrosshair = 2f;
    public Color colorCrosshair = Color.white;

    private float nextFireTime = 0f;
    private Camera cam;
    private Texture2D crosshairTexture;

    [Header("UI")]
    public TextMeshProUGUI textoMunicion;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        municionActual = municionMax;
        CrearCrosshairTexture();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && municionActual > 0)
        {
            Disparar();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Disparar()
    {
        municionActual--;

        if (muzzleFlash != null)
            muzzleFlash.Play();

        // Crear proyectil visible
        CrearProyectil();

        // Raycast para daño instantáneo
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, alcance))
        {
            Debug.Log("Golpeaste: " + hit.transform.name);

            ZombieAI zombie = hit.transform.GetComponent<ZombieAI>();
            if (zombie != null)
                zombie.RecibirDano(dano);

            if (impactoEfecto != null)
            {
                GameObject efecto = Instantiate(impactoEfecto, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(efecto, 1f);
            }
        }

        if (textoMunicion != null)
            textoMunicion.text = "Munición: " + municionActual;
    }

    void CrearProyectil()
    {
     // Calcular punto de destino con raycast desde el centro de la cámara
        Vector3 puntoDestino;
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, alcance))
        {
            puntoDestino = hitInfo.point;
        }
        else
        {
            puntoDestino = cam.transform.position + cam.transform.forward * alcance;
        }

        // Posición de origen: mano derecha del jugador
        Vector3 origenProyectil = cam.transform.position
            + cam.transform.right * offsetMano.x
            + cam.transform.up * offsetMano.y
            + cam.transform.forward * offsetMano.z;

        // Dirección desde la mano hacia el punto de mira
        Vector3 direccion = (puntoDestino - origenProyectil).normalized;

        // Crear proyectil visual
        GameObject proyectil = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        proyectil.name = "Proyectil";
        proyectil.transform.localScale = Vector3.one * 0.1f;
        proyectil.transform.position = origenProyectil;

        Destroy(proyectil.GetComponent<Collider>());

        Renderer rend = proyectil.GetComponent<Renderer>();
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        Material mat = new Material(shader);
        mat.color = new Color(1f, 0.9f, 0.3f);
        mat.SetColor("_EmissionColor", new Color(1f, 0.9f, 0.3f) * 3f);
        mat.EnableKeyword("_EMISSION");
        rend.material = mat;

        Rigidbody rb = proyectil.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = direccion * velocidadProyectil;

        Destroy(proyectil, tiempoVidaProyectil);
    }   

    void CrearCrosshairTexture()
    {
        int size = 64;
        crosshairTexture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        crosshairTexture.filterMode = FilterMode.Point;

        // Fondo transparente
        Color transparente = new Color(0, 0, 0, 0);
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                crosshairTexture.SetPixel(x, y, transparente);

        int centro = size / 2;
        int largo = (int)tamañoCrosshair;
        int grosor = Mathf.Max(1, (int)(grosorCrosshair / 2f));
        int gap = 2; // espacio en el centro

        // Líneas del crosshair (arriba, abajo, izquierda, derecha)
        for (int i = gap; i <= largo + gap; i++)
        {
            for (int g = -grosor; g <= grosor; g++)
            {
                // Arriba
                if (centro + i < size) crosshairTexture.SetPixel(centro + g, centro + i, colorCrosshair);
                // Abajo
                if (centro - i >= 0) crosshairTexture.SetPixel(centro + g, centro - i, colorCrosshair);
                // Derecha
                if (centro + i < size) crosshairTexture.SetPixel(centro + i, centro + g, colorCrosshair);
                // Izquierda
                if (centro - i >= 0) crosshairTexture.SetPixel(centro - i, centro + g, colorCrosshair);
            }
        }

        crosshairTexture.Apply();
    }

    void OnGUI()
    {
        if (crosshairTexture != null)
        {
            float size = tamañoCrosshair * 6f;
            Rect pos = new Rect(
                (Screen.width - size) / 2f,
                (Screen.height - size) / 2f,
                size, size
            );
            GUI.DrawTexture(pos, crosshairTexture);
        }
    }
}
