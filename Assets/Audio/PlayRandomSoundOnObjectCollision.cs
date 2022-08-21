using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayRandomSoundOnObjectCollision : MonoBehaviour
    {
        [Header(" ")]
        [Header("                                            ---===== Written by Rhys =====---")]

        [SerializeField] private FMODUnity.EventReference _collisionSounds;

        private FMOD.Studio.EventInstance collisionSounds;


        private void Awake()
        {
            if (!_collisionSounds.IsNull)
            {
                collisionSounds = FMODUnity.RuntimeManager.CreateInstance(_collisionSounds);
            }
        }





        public float RequiredVelocity = 3;
        //Creates inspector window slot in which the GameObject that contains the desired to be played RandomAudioPlayer Sctipt must be placed (In this case it should be tbe object that this script is also placed on)

        void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.magnitude > RequiredVelocity)
            {
                PlayCollision();
            }
        }



        public void PlayCollision()
        {
            collisionSounds.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            collisionSounds.start();
        }




    }
