using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition 
{
	public int m_Id;
	public Condition m_Condition;
	public int m_TargetState;
}
