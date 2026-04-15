using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float tiempoEntreSpawns = 5f;
    public int totalZombies = 10;

    private float siguienteSpawn = 0f;
    private int zombiesCreados = 0;

    void Update()
    {
        if (zombiesCreados < totalZombies && Time.time >= siguienteSpawn)
        {
            SpawnZombie();
            siguienteSpawn = Time.time + tiempoEntreSpawns;
        }
    }

    void SpawnZombie()
    {
        Vector3 posicion = new Vector3(
            transform.position.x + Random.Range(-3f, 3f),
            transform.position.y,
            transform.position.z + Random.Range(-3f, 3f)
        );

        Instantiate(zombiePrefab, posicion, Quaternion.identity);
        zombiesCreados++;
    }
}