using UnityEngine;
using UnityEngine.EventSystems;

public class DragText : MonoBehaviour, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 offset;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //Move the rectTransform component (its location) based on the input's position
    }
}
