using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonPro : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 escalaOriginal;
    public float escalaHover = 1.15f;
    public float velocidad = 10f;

    private Image imagen;
    public Color colorNormal = new Color(0, 0, 0, 0.6f);
    public Color colorHover = new Color(0, 1, 0, 0.8f);

    private bool hover = false;

    void Start()
    {
        escalaOriginal = transform.localScale;
        imagen = GetComponent<Image>();
        imagen.color = colorNormal;
    }

    void Update()
    {
        Vector3 objetivo = hover ? escalaOriginal * escalaHover : escalaOriginal;
        transform.localScale = Vector3.Lerp(transform.localScale, objetivo, Time.deltaTime * velocidad);

        Color targetColor = hover ? colorHover : colorNormal;
        imagen.color = Color.Lerp(imagen.color, targetColor, Time.deltaTime * velocidad);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }
}