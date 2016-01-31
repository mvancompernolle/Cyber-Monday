using UnityEngine;
using System.Collections;

public class Achievements : MonoBehaviour {

    static float currentTime = 3.0f, duration = 3.0f;
    static string achievementToShow;
    public GameObject achievments;
    SpriteRenderer achieventRender;
    public Transform achievmentPOS;
    Vector2 dims;

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
            Vector3 origin = Camera.main.WorldToScreenPoint(new Vector3(achieventRender.bounds.min.x, achieventRender.bounds.max.y, 0.0f));
            Vector3 extent = Camera.main.WorldToScreenPoint(new Vector3(achieventRender.bounds.max.x, achieventRender.bounds.min.y, 0.0f));
            Vector2 dims = new Vector2(extent.x - origin.x, origin.y - extent.y);
            GUI.TextArea(new Rect(origin.x + dims.x * 0.2f, Screen.height - (origin.y - dims.y * 0.15f), dims.x * 0.7f, dims.y * 0.7f), achievementToShow);
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
