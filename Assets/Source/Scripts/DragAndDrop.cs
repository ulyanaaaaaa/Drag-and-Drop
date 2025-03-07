using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(RectTransform))]
public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private Rigidbody2D _rigidbody;
    private BackgroundScroller _parent;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _parent = GetComponentInParent<BackgroundScroller>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _parent.SetDraggingItem(true);
        _canvasGroup.blocksRaycasts = false;
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        _parent.SetDraggingItem(false);
        _canvasGroup.blocksRaycasts = true;
        
        if (!transform.parent.TryGetComponent(out Slot slot))
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Slot slot))
        {
            transform.SetParent(slot.transform);
            _rigidbody.bodyType = RigidbodyType2D.Static;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Slot slot))
        {
            transform.SetParent(_parent.transform);
        }
    }
}