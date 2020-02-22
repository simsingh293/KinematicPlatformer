using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomPhysics.Base
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsObject : MonoBehaviour
    {
        public float gravityModifier = 1;
        public float minGroundNormalY = 0.65f;

        protected Vector3 targetVelocity = Vector3.zero;
        protected Rigidbody rb;
        protected Collider col;
        protected Vector3 velocity = Vector3.zero;
        protected bool isGrounded = false;
        protected Vector3 groundNormal = Vector3.zero;
        protected float halfPlayerHeight = 0;

        protected RaycastHit[] hitBuffer = new RaycastHit[16];
        protected List<RaycastHit> hitBufferList = new List<RaycastHit>();

        protected const float minMoveDistance = 0.001f;
        protected const float shellRadius = 0.01f;


        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponentInChildren<Collider>();

            halfPlayerHeight = col.bounds.extents.y;
        }

        void Reset()
        {
            //rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            targetVelocity = Vector3.zero;
            ComputeVelocity();
            CheckForCollisionsAgainst();
        }

        protected virtual void ComputeVelocity() { }

        protected virtual void CheckForCollisionsAgainst() { }


        private void FixedUpdate()
        {
            // adds gravity
            velocity += gravityModifier * Physics.gravity * Time.deltaTime;
            // sets velocity to given velocity horizontal value
            velocity.x = targetVelocity.x;

            isGrounded = false;
            // the change in position in a frame at the current velocity
            Vector3 delta = velocity * Time.deltaTime;
            // TODO figure this out for 3d, only works for 2d right now
            Vector3 moveAlongGround = new Vector3(groundNormal.y, -groundNormal.x, 0);

            Vector3 move = moveAlongGround * delta.x;

            Movement(move, false);

            move = Vector3.up * delta.y;

            Movement(move, true);
        }


        void Movement(Vector3 moveVector, bool yMovement)
        {
            float distance = moveVector.magnitude;

            if (distance > minMoveDistance)
            {
                hitBuffer = rb.SweepTestAll(moveVector, distance + shellRadius);
                int count = hitBuffer.Length;

                hitBufferList.Clear();

                for (int i = 0; i < count; i++)
                {
                    hitBufferList.Add(hitBuffer[i]);
                }


                for (int i = 0; i < hitBufferList.Count; i++)
                {
                    Vector3 currentNormal = hitBufferList[i].normal;

                    if (currentNormal.y > minGroundNormalY)
                    {
                        isGrounded = true;
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    Debug.DrawLine(transform.position, transform.position + currentNormal * 3, Color.red);

                    // difference between velocity and current normal to determine if player's velocity
                    // needs to be substracted if they are going to continue colliding with an object 
                    // in the next frame
                    float projection = Vector3.Dot(velocity, currentNormal);

                    if (projection < 0)
                    {
                        velocity = velocity - projection * currentNormal;
                    }

                    //isGrounded = CheckForGround();

                    float modifiedDistance = hitBufferList[i].distance - shellRadius;

                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }

                rb.position += moveVector.normalized * distance;
            }

        }


        public Vector3 GetVelocity()
        {
            return velocity;
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + groundNormal * 2);
        }


        private void OnCollisionEnter(Collision collision)
        {
            //CheckForCollisionsAgainst(collision.collider);
        }


        private bool CheckForGround()
        {
            float length = halfPlayerHeight + shellRadius + 0.01f;

            Vector3 start = transform.position;
            Vector3 end = transform.position - (Vector3.up * length);

            Debug.DrawLine(start, end, Color.red);

            Ray ray = new Ray(start, Vector3.down);
            RaycastHit hit;

            if (Physics.Linecast(start, end, out hit))
            {
                if (hit.distance < (halfPlayerHeight + shellRadius))
                {
                    return true;
                }
            }

            return false;

        }


        private bool isOnGround()
        {
            float lengthToSearch = 0.8f;

            Vector3 lineStart = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            Vector3 vectorToSearch = new Vector3(this.transform.position.x, lineStart.y - lengthToSearch, this.transform.position.z);

            Color color = new Color(0.0f, 0.0f, 1.0f);
            Debug.DrawLine(lineStart, vectorToSearch, color);

            RaycastHit hitInfo;
            if (Physics.Linecast(this.transform.position, vectorToSearch, out hitInfo))
            {
                if (hitInfo.distance < halfPlayerHeight)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
