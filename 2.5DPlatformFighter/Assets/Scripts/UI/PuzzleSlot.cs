using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    public GameObject puzzlePiece;
    public bool solved;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.gameObject == puzzlePiece)
        {
            //Debug.Log("gggggg");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<DragDrop>().canvasGroup.alpha = 1f;
            eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;
            solved = true;
        }
    }
}
