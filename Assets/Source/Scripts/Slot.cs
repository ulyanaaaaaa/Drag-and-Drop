using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
