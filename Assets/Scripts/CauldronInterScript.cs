using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class CauldronInterScript : BaseInteraction
{
    static Dictionary<string, uint> itemCodes = new Dictionary<string, uint>();

    override public void Start()
    {
        base.Start();
        itemCodes["eyes_of_the_interbeast"]    = 1 << 0;
        itemCodes["frog_leg"]                   = 1 << 1;
        itemCodes["full_50tb_hdd"]              = 1 << 2;
        itemCodes["lazer_dragon"]               = 1 << 3;
        itemCodes["techno_liver"]               = 1 << 4;
        itemCodes["doom_merc"]                  = 1 << 5;
        itemCodes["combine_helmet"]             = 1 << 6;
        itemCodes["comp_chip"]                  = 1 << 7;
        itemCodes["fairy_nails"]                = 1 << 8;
        itemCodes["ogre_left_hand"]             = 1 << 9;
        itemCodes["soykaf_coffee"]              = 1 << 10;
        itemCodes["tasty_bagel"]                = 1 << 11;
        itemCodes["20_Sided_Die"]               = 1 << 12;
        itemCodes["ancient_smart_phone"]        = 1 << 13;
        itemCodes["blue_orange_soda"]           = 1 << 14;
        itemCodes["corp_magazine"]              = 1 << 15;
        itemCodes["flux_drive"]                 = 1 << 16;
        itemCodes["haunted_toy_car"]            = 1 << 17;
        itemCodes["screaming_cyber_witches"]    = 1 << 18;
    }
    //0, 4, 7, 9


    public override void Interact()
    {
        uint code = 0;
        for( int i = 0; i < charController.items.Count; ++i)
        {
            Debug.Log(charController.items[i].Value);
            code |= getItemCode(charController.items[i].Value);
            if(charController.items[i].Value == "bad_guy")
            {
                EnemyInterScript.numEnemies--;
            }
            Destroy(charController.items[i].Key);
        }
        charController.items.Clear();

        // determine event name
        string eventName = "";
        Debug.Log(code);
        Debug.Log((itemCodes["eyes_of_the_interbeast"] | itemCodes["techno_liver"] | itemCodes["comp_chip"] | itemCodes["ogre_left_hand"]));
        if (code == (itemCodes["frog_leg"] | itemCodes["tasty_bagel"] | itemCodes["soykaf_coffee"]))
        {
            eventName = "Good job you made: A Balanced Breakfast!";
        }
        else if (code == (itemCodes["eyes_of_the_interbeast"] | itemCodes["techno_liver"] | itemCodes["comp_chip"] | itemCodes["ogre_left_hand"]))
        {
            
            eventName = "Good job you made: Frankenstein 2216!";
        }
        else if (code == (itemCodes["ancient_smart_phone"] | itemCodes["flux_drive"] | itemCodes["full_50tb_hdd"]))
        {

            eventName = "Good job you made: A little piece of history";
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
        if (itemCodes.ContainsKey(item))
        {
            return itemCodes[item];
        }
        return 0;
    }
}
