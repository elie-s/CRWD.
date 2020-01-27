using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRWD
{
    public abstract class  ScriptedBehaviourRoutine : ScriptableObject
    {
        public abstract IEnumerator Play(Component _component);
    }
}