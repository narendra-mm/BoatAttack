﻿using UnityEngine;
using System.Collections;
using WaterSystem;

public class Engine : MonoBehaviour 
{
	//cache vars
	private Rigidbody RB;
	public Vector3 vel;

    public AudioSource engineSound;
    public AudioSource waterSound;

    //engine stats
    public float torque = 35f;
	public float horsePower = 100f;
	public float throttle = 0f; 

	//cache for the boatcontroller
	private BuoyantObject buoyantObject;
	//private BoatController boatController;
	//cache for the wake generator
	//private CreateWake wakeGen;

	public Vector3 enginePosition;
	private Vector3 engineDir;

	void Awake () 
	{
		//cache rigidbody
		RB = gameObject.GetComponent<Rigidbody>();
        engineSound.time = Random.Range(0f, engineSound.clip.length);
        waterSound.time = Random.Range(0f, waterSound.clip.length);

        //cache boat and boat controller
        //boatController = gameObject.GetComponent<BoatController>();
        buoyantObject = gameObject.GetComponent<BuoyantObject>();
        //cache wakeGenerator
        //wakeGen = GameObject.Find ("WakeGenerator").GetComponent<CreateWake> ();

    }	

	void FixedUpdate()
	{
        vel = RB.velocity;
        float velMag = vel.sqrMagnitude;
        engineSound.pitch = Mathf.Max(velMag * 0.015f, 0.3f);
        // if (buoyantObject.percentSubmerged > 0.1f)
        // {
        //     RB.AddForce(Vector3.up * (Mathf.Abs(Vector3.Dot(vel, transform.forward)) * velMag), ForceMode.Force);
        // }
        // if(throttle > 0f)
        // {
        // 	if(transform.TransformPoint(enginePosition).y < 0.5f)
        // 	{
        // 		Vector3 forward = transform.forward + transform.TransformDirection(engineDir * 0.15f);
        // 		forward.Normalize();
        // 		RB.AddForceAtPosition(((forward * horsePower) * throttle), transform.TransformPoint(enginePosition), ForceMode.Acceleration);
        // 	}
        // 	throttle -= Time.fixedDeltaTime;
        // }
        // else
        // {
        // 	throttle = 0f;
        // }

        // if(engineDir.x > 0.1f)
        // 		engineDir.x -= Time.fixedDeltaTime;
        // else if(engineDir.x < -0.1f)
        // 		engineDir.x += Time.fixedDeltaTime;
        // else
        // 	engineDir.x = 0f;
    }

	public void Accel(float modifier)
	{
        if (buoyantObject.percentSubmerged > 0.1f)
        {
            Vector3 forward = transform.forward;
            forward.y = 0f;
            forward.Normalize();
            RB.AddForce(forward * modifier * horsePower, ForceMode.Acceleration);
            RB.AddRelativeTorque(-Vector3.right * modifier, ForceMode.Acceleration);
        }


        // if(throttle < 1f)
        // 	throttle += modifier;
        // if(throttle > 1f)
        // 	throttle = 1f;
    }

	public void TurnLeft(float modifier)
	{
        //if(buoyantObject.percentSubmerged > 0.05f)
        if (buoyantObject.percentSubmerged > 0.1f)
        {
            RB.AddRelativeTorque(new Vector3(0f, -torque, torque * 0.5f) * modifier * 500f, ForceMode.Acceleration);
        }
        // if(engineDir.x < 1f)
        // 	engineDir.x += modifier;
        // if(engineDir.x > 1f)
        // 	engineDir.x = 1f;
    }

	public void TurnRight(float modifier)
	{
        //if(buoyantObject.percentSubmerged > 0.05f)
        if (buoyantObject.percentSubmerged > 0.1f)
        {
            RB.AddRelativeTorque(new Vector3(0f, torque, -torque * 0.5f) * modifier * 500f, ForceMode.Acceleration);
        }
        // if(engineDir.x > -1f)
        // 	engineDir.x -= modifier;
        // if(engineDir.x < -1f)
        // 	engineDir.x = -1f;
    }

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.green;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawCube (enginePosition, new Vector3(0.1f, 0.2f, 0.3f));
	}

}