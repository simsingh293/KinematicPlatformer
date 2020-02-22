using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Conditions.Platform
{
    [CreateAssetMenu(fileName = "condition.asset", menuName = "Conditions/Platform/PassThrough", order = 0)]
    public class CanPassThroughCondition : Base.SOCondition
    {
        public float length = 0;
        public Vector3 direction = Vector3.zero;
        public LayerMask layerMask;

        public override bool isMet(GameObject obj)
        {
            direction = direction.normalized;

            Vector3 start = obj.transform.position;
            Vector3 end = obj.transform.position + (direction * length);

            Ray ray = new Ray(start, direction);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, length, layerMask))
            {
                if(hit.collider.gameObject.tag == "Passable")
                {
                    return true;
                }
            }

            return false;
        }

        
    }
}