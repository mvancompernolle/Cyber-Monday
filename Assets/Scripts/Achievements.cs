using UnityEngine;
using System.Collections;

public class Achievements : MonoBehaviour {

    static float currentTime = 3.0f, duration = 3.0f;
    static string achievementToShow;

	// Use this for initialization
	void Start () {
	
	}
	
    public static void onEvent(EventInfo info)
    {
        achievementToShow = info.eventName;
        currentTime = 0.0f;
    }

    void OnGUI()
    {
        if (currentTime < duration) {
            GUI.TextArea(new Rect(10, 10, 200, 100), achievementToShow);
            Debug.Log("achievement");
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
