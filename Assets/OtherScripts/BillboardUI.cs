using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    RectTransform m_CanvasRectTransform;

    void Awake()
    {
        m_CanvasRectTransform=GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if(m_CanvasRectTransform != null)
            m_CanvasRectTransform.LookAt(transform.position+Camera.main.transform.rotation*Vector3.forward, Camera.main.transform.rotation*Vector3.up);
    }
}
