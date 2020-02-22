using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomPhysics.Controllers;


namespace Behaviours.Platform
{
    [CreateAssetMenu(fileName = "behaviour.asset", menuName = "Behaviours/Platforms/PassThrough", order = 1)]
    public class PassThroughBehaviour : Base.SOBehaviour
    {
        public float heightToRaise = 2;
        Vector3 currentVelocity = Vector3.zero;

        public override void Action(GameObject obj)
        {
            currentVelocity = obj.GetComponent<HorizontalController>().GetVelocity();

            Vector3 delta = currentVelocity * Time.deltaTime;

            if(currentVelocity.y > 0)
            {
                obj.transform.position += (delta * heightToRaise);
            }
            else
            {
                return;
            }
        }
    }
}