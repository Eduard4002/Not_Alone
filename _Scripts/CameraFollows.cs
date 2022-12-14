using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public float interpVelocity;
	public float minDistance;
	public float followDistance;
    public Transform target;
    Vector3 targetPos;
    public Vector3 offset;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;

			Vector3 targetDirection = (target.transform.position - posNoZ);

			interpVelocity = targetDirection.magnitude * 5f;

			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 

			transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.25f);

		}
    }

    public void ChangeMusicVolume(float volume){
        audioSource.volume = volume;
    }
}
