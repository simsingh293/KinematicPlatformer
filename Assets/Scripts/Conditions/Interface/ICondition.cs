using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Conditions.Base
{
    public interface ICondition
    {
        bool isMet(GameObject obj);
    }
}