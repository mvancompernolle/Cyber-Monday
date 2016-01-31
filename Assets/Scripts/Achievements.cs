using UnityEngine;
using System.Collections;

public class Achievements : MonoBehaviour {

    static float currentTime = 3.0f, duration = 3.0f;
    static string achievementToShow;
    public GameObject achievments;
    SpriteRenderer achieventRender;
    public Transform achievmentPOS;

	// Use this for initialization
	void Start () {
        achievmentPOS = achievments.GetComponent<Transform>();
        achieventRender = achievments.GetComponent<SpriteRenderer>();
        achieventRender.enabled = false;
        
	}
	
    public static void onEvent(EventInfo info)
    {
        achievementToShow = info.eventName;
        currentTime = 0.0f;
    }

    void OnGUI()
    {
        if (currentTime < duration) {
            achieventRender.enabled = true;
            GUI.TextArea(new Rect(710, 775, 500, 125), achievementToShow);
        }
        else
        {
            achieventRender.enabled = false;
        }
    }

    void Update()
    {
        if(currentTime < duration)
        {
            currentTime += Time.deltaTime;
        }
    }
}
