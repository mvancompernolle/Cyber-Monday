using UnityEngine;
using System.Collections;

public class EventInfo {

    public string eventName;
    public string otherInfo;
    public uint itemCode;

    public EventInfo(string name, uint code = 0, string other = "")
    {
        eventName = name;
        otherInfo = other;
        itemCode = code;
    }
}
