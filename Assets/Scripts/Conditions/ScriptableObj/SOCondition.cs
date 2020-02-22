using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Behaviours.Base;

namespace Conditions.Base
{
    public abstract class SOCondition : ScriptableObject
    {
        public SOBehaviour trueBehaviour = null;
        public SOBehaviour falseBehaviour = null;

        public virtual bool isMet(GameObject obj)
        {
            return false;
        }
    }
}