using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using CustomPhysics.Base;
using Conditions.Base;

namespace CustomPhysics.Controllers 
{ 
    public class EnemyController : PhysicsObject
    {
        public float maxSpeed = 5;

        public SOCondition[] conditions;

        System.Random random = new System.Random();
        public bool moveLeft = false;
        int randomizer = 0;
        public float amountToWalk = 0;
        public float amountWalked = 0;

        protected override void ComputeVelocity()
        {
            Vector3 move = Vector3.right;

            if(amountWalked > amountToWalk)
            {
                randomizer = random.Next(0, 1);
                moveLeft = randomizer == 0 ? false : true;

                randomizer = random.Next(3, 10);
                amountToWalk = randomizer;

                amountWalked = 0;
            }
            else
            {
                amountWalked += Time.deltaTime;
            }

            if (moveLeft)
            {
                move *= -1;
            }

            targetVelocity = move * maxSpeed;
        }

        protected override void CheckForCollisionsAgainst()
        {
            foreach(var condition in conditions)
            {
                if (condition.isMet(gameObject))
                {
                    condition.trueBehaviour.Action(gameObject);
                }
                else if (condition.falseBehaviour != null)
                {
                    condition.falseBehaviour.Action(gameObject);
                }
            }
        }
    }
}