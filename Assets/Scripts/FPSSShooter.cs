using UnityEngine;
using TMPro;

public class FPSShooter : MonoBehaviour
{
    [Header("Disparo")]
    public float alcance = 50f;
    public float dano = 25f;
    public float fireRate = 0.3f;
    public int municionMax = 30;
    public int municionActual;

    [Header("Efectos")]
    public ParticleSystem muzzleFlash;
    public GameObject impactoEfecto;

    private float nextFireTime = 0f;
    private Camera cam;

    [Header("UI")]
    public TextMeshProUGUI textoMunicion;

    [Header("Audio")]
    public AudioClip sonidoDisparo;
    private AudioSource audioSource;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        municionActual = municionMax;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.timeScale == 0) return; // no hacer nada si el juego esta pausado

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && municionActual > 0)
        {
            Disparar();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.R) && municionActual < municionMax)
        {
            Recargar();
        }
    }

void Disparar()
{
    if (sonidoDisparo != null)
        audioSource.PlayOneShot(sonidoDisparo);
    
    municionActual--;

    RaycastHit hit;
    if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, alcance))
    {
        Debug.Log("Golpeaste: " + hit.transform.name);

        ZombieAI zombie = hit.transform.GetComponent<ZombieAI>();
        if (zombie != null)
            zombie.RecibirDano(dano);
    }
    else
    {
        Debug.Log("No golpeaste nada");
    }
    if (textoMunicion != null)
    textoMunicion.text = "Munición: " + municionActual;
}

void Recargar()
{
    municionActual = municionMax;
    if (textoMunicion != null)
        textoMunicion.text = "Munición: " + municionActual;
    Debug.Log("Recargando...");
    }
}