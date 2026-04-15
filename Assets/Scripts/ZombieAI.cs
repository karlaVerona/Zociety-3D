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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            jugador = player.transform;
    }

    void Update()
    {
        if (jugador == null) return;
        agent.SetDestination(jugador.position);
    }

    public void RecibirDano(float cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
            Destroy(gameObject);
    }

void OnTriggerStay(Collider other)
{
    if (other.CompareTag("Player") && Time.time >= siguienteDano)
    {
        other.GetComponent<PlayerHealth>().RecibirDano(dano);
        siguienteDano = Time.time + tiempoEntreDano;
    }
}
}
