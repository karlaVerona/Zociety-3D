using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("Stats")]
    public float vida = 100f;
    public float dano = 10f;
    public float tiempoEntreDano = 1f;

    private NavMeshAgent agent;
    private Transform jugador;
    private float siguienteDano = 0f;

    [Header("Audio")]
    public AudioClip sonidoZombie;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            jugador = player.transform;
    }

    private float timerGruñido = 0f;

    void Update()
    {
        if (jugador == null) return;
        agent.SetDestination(jugador.position);

        // Gruñido cada 3 segundos
        timerGruñido -= Time.deltaTime;
        if (timerGruñido <= 0)
        {
            if (sonidoZombie != null)
                audioSource.PlayOneShot(sonidoZombie);
            timerGruñido = 10f;
        }

        float distancia = Vector3.Distance(transform.position, jugador.position);
        if (distancia < 1.5f && Time.time >= siguienteDano)
        {
            jugador.GetComponent<PlayerHealth>().RecibirDano(dano);
            siguienteDano = Time.time + tiempoEntreDano;
        }
    }

    public void RecibirDano(float cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
            Destroy(gameObject);
    }
}