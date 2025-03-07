using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundScroller : MonoBehaviour, IDragHandler
{
    private RectTransform _rectTransform;
    private Vector2 _lastTouchPosition;

    [SerializeField] private float _minX; 
    [SerializeField] private float _maxX; 

    private bool _isDraggingItem;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDraggingItem) return; 

        Vector2 delta = eventData.position - _lastTouchPosition;
        MoveBackground(delta);
        _lastTouchPosition = eventData.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - _lastTouchPosition;
                if (!_isDraggingItem) 
                {
                    MoveBackground(delta);
                }
                _lastTouchPosition = touch.position;
            }
        }
    }

    private void MoveBackground(Vector2 delta)
    {
        Vector3 newPosition = _rectTransform.anchoredPosition + delta;
        newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);
        newPosition.y = _rectTransform.anchoredPosition.y;
        _rectTransform.anchoredPosition = newPosition;
    }
    
    public void SetDraggingItem(bool isDragging)
    {
        _isDraggingItem = isDragging;
    }
}