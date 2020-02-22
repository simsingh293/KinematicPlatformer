using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours.Base
{
    public abstract class SOBehaviour : ScriptableObject
    {
        public virtual void Action(GameObject obj)
        {
            Debug.Log("Doing Something");
        }
    }
}