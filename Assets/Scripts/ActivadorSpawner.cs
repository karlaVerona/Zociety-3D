using UnityEngine;

public class ActivadorSpawner : MonoBehaviour
{
    public ZombieSpawner spawner;

    void OnTriggerEnter(Collider other)
{
    Debug.Log("Trigger activador con: " + other.name);
    if (other.CompareTag("Player"))
    {
        Debug.Log("Activando spawner");
        spawner.Activar();
        Destroy(gameObject);
    }
}
}