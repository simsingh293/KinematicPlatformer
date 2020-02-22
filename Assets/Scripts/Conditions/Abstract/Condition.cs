using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Conditions.Base
{
    [System.Serializable]
    public abstract class Condition
    {
        public virtual bool isMet(GameObject obj)
        {
            return false;
        }
    }
}