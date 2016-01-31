using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    Transform playerTransform;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        offset = transform.position - playerTransform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = playerTransform.position + offset;
	}
}
