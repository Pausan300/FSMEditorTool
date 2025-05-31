using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    BoxCollider m_Trigger;
    Animation m_Animation;
    public AnimationClip m_OpenDoorAnim;
    public AnimationClip m_CloseDoorAnim;

    public List<GameObject> m_EnemiesRequiredToOpen=new List<GameObject>();

    void Start()
    {
        m_Animation=GetComponent<Animation>();
        m_Trigger=GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(m_EnemiesRequiredToOpen.Count<=0)
            m_Animation.Play(m_OpenDoorAnim.name);
    }
    private void OnTriggerExit(Collider other)
    {
        if(m_EnemiesRequiredToOpen.Count<=0)
            m_Animation.Play(m_CloseDoorAnim.name);
    }
}
