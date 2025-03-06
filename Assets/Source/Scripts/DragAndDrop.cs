using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform _rectTransform;
    private Image _image;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.color = new Color(0f, 255f, 200f, 0.7f);
        _image.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.color = new Color(255f, 255f, 200f, 1f);
        _image.raycastTarget = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }
}
