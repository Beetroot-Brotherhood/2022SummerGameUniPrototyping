using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRollingSound : MonoBehaviour
{
    
        public bool Grounded = true;
        public float GroundedOffset = -0.14f;
        public float GroundedRadius = 0.5f;       
        public LayerMask GroundLayers;

        private float ballSpeed = 0.0f;

        private Vector3 thisBall;



        [SerializeField] private FMODUnity.EventReference _ballRoll;

        private FMOD.Studio.EventInstance ballRoll;

        private void Awake()
        {
            if (!_ballRoll.IsNull)
            {
                ballRoll = FMODUnity.RuntimeManager.CreateInstance(_ballRoll);
            }

            /* ballRoll.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(thisBall)); */
            ballRoll.start();
        }



        private void Update()
        {
            GroundedCheck();
            SetParameter();

            thisBall = this.gameObject.transform.position;
            ballRoll.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(thisBall));
        }

        private void FixedUpdate()
        {
            ballSpeed = this.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        }


        private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}




    private void SetParameter()
    {
        if (Grounded)
        {
            ballRoll.setParameterByName("RollingVolume", ballSpeed);
        }
        else
        {
            ballSpeed = 0.0f;
        }
    }

}