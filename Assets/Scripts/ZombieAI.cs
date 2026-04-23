using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("Stats")]
    public float vida = 100f;
    public float dano = 10f;
    public float tiempoEntreDano = 1f;

    [Header("Audio")]
    public AudioClip sonidoZombie;
    private AudioSource audioSource;

    private NavMeshAgent agent;
    private Transform jugador;
    private float siguienteDano = 0f;
    private float timerGruñido = 0f;
    private Animator anim;

 void Start()
{
    agent = GetComponent<NavMeshAgent>();
    anim = GetComponentInChildren<Animator>();
    audioSource = GetComponent<AudioSource>();
    
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        jugador = player.transform;
    }
    else
    {
        Debug.Log("Jugador NO encontrado");
    }
}

    void Update()
    {
    if (jugador == null) return;
    
    agent.SetDestination(jugador.position);

    if (anim != null)
{
    //Debug.Log("Velocidad: " + agent.velocity.magnitude);
    if (agent.velocity.magnitude > 0.1f)
        anim.SetBool("estaCaminando", true);
    else
        anim.SetBool("estaCaminando", false);
}

        // Gruñido cada 10 segundos
        timerGruñido -= Time.deltaTime;
        if (timerGruñido <= 0)
        {
            if (sonidoZombie != null)
                audioSource.PlayOneShot(sonidoZombie);
            timerGruñido = 10f;
        }

        // Daño al jugador
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
        {
            if (anim != null)
                anim.SetBool("estaMuerto", true);
            Invoke("Morir", 2f);
        }
    }

    void Morir()
    {
        Destroy(gameObject);
    }
}