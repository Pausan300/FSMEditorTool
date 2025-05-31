using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemy : MonoBehaviour
{
    public DoorOpener m_ProtectedDoor;

    private void OnDestroy()
    {
        m_ProtectedDoor.m_EnemiesRequiredToOpen.Remove(gameObject);
    }
}
