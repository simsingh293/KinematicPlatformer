using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomPhysics.Base;
using Conditions.Base;

namespace CustomPhysics.Controllers
{
    public class HorizontalController : PhysicsObject
    {
        public float maxSpeed = 5;
        public float jumpStartSpeed = 8;

        public SOCondition[] platformConditions;


        protected override void ComputeVelocity()
        {
            Vector3 move = Vector3.zero;

            move.x = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y = jumpStartSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            targetVelocity = move * maxSpeed;
        }

        protected override void CheckForCollisionsAgainst()
        {
            
            foreach(var condition in platformConditions)
            {
                if (condition.isMet(gameObject))
                {
                    condition.trueBehaviour.Action(gameObject);
                }
                else if(condition.falseBehaviour != null)
                {
                    condition.falseBehaviour.Action(gameObject);
                }

            }
            

        }

    }
}