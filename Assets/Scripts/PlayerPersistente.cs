using UnityEngine;

public class PlayerPersistente : MonoBehaviour
{
    private static PlayerPersistente instancia;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
    }
}
