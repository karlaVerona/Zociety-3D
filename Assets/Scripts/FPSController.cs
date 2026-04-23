using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float velocidadCorrer = 8f;
    public float fuerzaSalto = 5f;
    public float gravedad = -20f;

    [Header("Camara")]
    public Transform cameraHolder;
    public float sensibilidad = 2f;
    public float limiteVertical = 80f;

    private CharacterController cc;
    private Vector3 velocidadVertical;
    private float rotacionX = 0f;
    private bool estaEnSuelo;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        Mover();
        Rotar();
    }

    void Mover()
    {
        estaEnSuelo = cc.isGrounded;

        if (estaEnSuelo && velocidadVertical.y < 0)
            velocidadVertical.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float velActual = Input.GetKey(KeyCode.LeftShift) ? velocidadCorrer : velocidad;
        Vector3 movimiento = transform.right * x + transform.forward * z;
        cc.Move(movimiento * velActual * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
            velocidadVertical.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);

        velocidadVertical.y += gravedad * Time.deltaTime;
        cc.Move(velocidadVertical * Time.deltaTime);
    }

void Rotar()
{
    float rotY = 0f;
    float rotX = 0f;

    if (Input.GetKey(KeyCode.RightArrow)) rotY += sensibilidad * 2f;
    if (Input.GetKey(KeyCode.LeftArrow)) rotY -= sensibilidad * 2f;
    if (Input.GetKey(KeyCode.UpArrow)) rotX -= sensibilidad * 2f;
    if (Input.GetKey(KeyCode.DownArrow)) rotX += sensibilidad * 2f;

    rotacionX += rotX;
    rotacionX = Mathf.Clamp(rotacionX, -limiteVertical, limiteVertical);

    cameraHolder.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
    transform.Rotate(Vector3.up * rotY);
}


}