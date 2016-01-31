using UnityEngine;
using System.Collections;

public class AchivementNotify : MonoBehaviour {
    Transform playerTransform;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindWithTag("player").GetComponent<Transform>();
        offset = transform.position - playerTransform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
