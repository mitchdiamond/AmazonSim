using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroneStatusButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public RenderTexture rt;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        DroneStatusPopupManager.instance.SetActive(rt);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        DroneStatusPopupManager.instance.SetInactive();
    }

}
