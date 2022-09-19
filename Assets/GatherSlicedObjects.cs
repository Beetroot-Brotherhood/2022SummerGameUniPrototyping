using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatherSlicedObjects : MonoBehaviour
{
    public static GatherSlicedObjects instance;

    void Awake () {
        if (instance != null) {
            Debug.LogError("There is more than one GatherSlicedObjects!");
        }
        else {
            instance = this;
        }

        if (!_ballRoll.IsNull) // Assigns the relevant event reference to it's instance 
            {
                ballRoll = FMODUnity.RuntimeManager.CreateInstance(_ballRoll);
            }

            /* ballRoll.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(thisBall)); */
            ballRoll.start();
    }


    //public Text scoreText;
    public int score;
    public GameObject player;
    public int radius;
    public LayerMask layerMask;
    List<SlicedCounter> movingParts = new List<SlicedCounter>();
    public GameObject owner;

    public float recallTimer;
    private float currentRecallTimer;
    public float recallSpeed;


    [Space] 
    [Header("Ball Roll Sound")]
    [Space]


    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.5f;       
    public LayerMask GroundLayers;
    private float ballSpeed = 0.0f;
    private Vector3 thisBall;
    [SerializeField] private FMODUnity.EventReference _ballRoll;
    private FMOD.Studio.EventInstance ballRoll;





    // Update is called once per frame
    void Update()
    {
        //scoreText.text = "Score: " + score;
        if (OnSlicerInput.instance.onGatherSlicedParts) {
            Collider[] objectsToGather = Physics.OverlapSphere(player.transform.position , radius, layerMask.value, QueryTriggerInteraction.UseGlobal);
            for (int i = 0; i < objectsToGather.Length; i++) {
                SlicedCounter slicedCounter;
                if (objectsToGather[i].TryGetComponent<SlicedCounter>(out slicedCounter)) {
                    if (!movingParts.Contains(slicedCounter)) {
                        slicedCounter.StartMovingTowardsPlayer(player);
                        movingParts.Add(slicedCounter);
                    }
                }
            }
        }
        else {
            foreach (SlicedCounter movingPart in movingParts) {
                try {
                    movingPart.StopMovingTowardsPlayer();
                }
                catch (System.Exception) {
                }
            }
            movingParts = new List<SlicedCounter>();
        }

        currentRecallTimer += Time.deltaTime;
        if(currentRecallTimer >= recallTimer)
        {
            MoveTowardsPlayer();
        }

        GroundedCheck();
        SetParameter();

        thisBall = this.gameObject.transform.position;
        ballRoll.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(thisBall));

    }


    private void FixedUpdate()
    {
        ballSpeed = this.gameObject.GetComponent<Rigidbody>().velocity.magnitude;

        if (!Grounded)
        {
            ballSpeed = 0.0f;
        }
    }

    private void GroundedCheck() // Checking if ball is grounded
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


    void MoveTowardsPlayer()
    {
       
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, owner.transform.position, (Time.deltaTime * (currentRecallTimer - recallTimer)) * recallSpeed);
        if (Vector3.Distance(owner.transform.position, gameObject.transform.position) < 0.4)
        {
            
            Destroy(this.gameObject);
            ballRoll.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }





}
