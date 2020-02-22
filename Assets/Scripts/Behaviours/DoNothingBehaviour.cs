using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Behaviours.Base
{
    [CreateAssetMenu(fileName = "behaviour.asset", menuName = "Behaviours/Platforms/Nothing", order = 0)]
    public class DoNothingBehaviour : SOBehaviour
    {
        public override void Action(GameObject obj)
        {
            Debug.Log("Doing Nothing");
        }
    }
}
