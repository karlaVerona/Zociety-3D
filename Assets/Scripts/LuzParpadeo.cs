using UnityEngine;

public class LuzParpadeo : MonoBehaviour
{
    public float tiempoMinEncendida = 0.5f;
    public float tiempoMaxEncendida = 2f;
    public float tiempoMinApagada = 0.1f;
    public float tiempoMaxApagada = 0.5f;

    private Light luz;
    private float timer;
    private bool encendida = true;

    void Start()
    {
        luz = GetComponent<Light>();
        timer = Random.Range(tiempoMinEncendida, tiempoMaxEncendida);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            encendida = !encendida;
            luz.enabled = encendida;

            if (encendida)
                timer = Random.Range(tiempoMinEncendida, tiempoMaxEncendida);
            else
                timer = Random.Range(tiempoMinApagada, tiempoMaxApagada);
        }
    }
}
