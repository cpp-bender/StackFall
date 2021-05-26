using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Utility : MonoBehaviour
{
    public static bool IgnoreUI()
    {
        PointerEventData pointerEventDatas = new PointerEventData(EventSystem.current);
        pointerEventDatas.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventDatas, raycastResults);
        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<Utility>() != null)
            {
                return true;
            }
        }
        return false;
    }
}
