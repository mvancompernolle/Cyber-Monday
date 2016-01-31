using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CauldronInterScript : BaseInteraction
{
    static Dictionary<string, uint> itemCodes = new Dictionary<string, uint>();

    public void Start()
    {
        base.Start();
        itemCodes["eye_of_the_inter_beast"] = 1 << 0;
        itemCodes["frog_leg"] = 1 << 1;
        itemCodes["full_50tb_hdd"] = 1 << 2;
        itemCodes["lazer_dragon"] = 1 << 3;
        itemCodes["techno_liver"] = 1 << 4;
        itemCodes["player_7"] = 1 << 5;
    }

    public override void Interact()
    {
        uint code = 0;
        for( int i = 0; i < charController.items.Count; ++i)
        {
            Debug.Log(charController.items[i].Value);
            code |= getItemCode(charController.items[i].Value);
            Destroy(charController.items[i].Key);
        }
        charController.items.Clear();

        // determine event name
        string eventName = "";
        if(code == (itemCodes["frog_leg"] | itemCodes["full_50tb_hdd"]))
        {
            eventName = "YOU EARNED THIS WEIRD ACHIEVEMENT";
        }
        else
        {
            eventName = "YOU SUCK AT WITCHING - CONTACT GERALT.";
        }

        Achievements.onEvent(new EventInfo(eventName, code));
    }

    public override bool CanInteract()
    {
        return charController.items.Count > 0;
    }

    public uint getItemCode(string item)
    {
        return itemCodes[item];
    }
}
