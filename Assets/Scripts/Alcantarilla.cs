using UnityEngine;

public class Alcantarilla : MonoBehaviour
{
    public Transform puntoLlegada;

    void OnTriggerEnter(Collider other)
{
    Debug.Log("Trigger con: " + other.name);
    if (other.CompareTag("Player"))
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            player.transform.position = puntoLlegada.position;
            cc.enabled = true;
        }
    }
}
}