using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DrawNode : ScriptableObject
{
   public abstract void DrawWindow(Node Node);
   public abstract void DrawLabel(Node Node);
   public abstract void DrawLine(Node Node);
}
