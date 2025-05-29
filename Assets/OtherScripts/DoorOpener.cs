using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    BoxCollider m_Trigger;
    Animation m_Animation;
    public AnimationClip m_OpenDoorAnim;
    public AnimationClip m_CloseDoorAnim;

    void Start()
    {
        m_Animation=GetComponent<Animation>();
        m_Trigger=GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_Animation.Play(m_OpenDoorAnim.name);
    }
    private void OnTriggerExit(Collider other)
    {
        m_Animation.Play(m_CloseDoorAnim.name);
    }
}
