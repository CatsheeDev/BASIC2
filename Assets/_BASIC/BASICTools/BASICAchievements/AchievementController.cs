using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementController : Singleton<AchievementController>
{
    private AchievementProfile currProfile; 

    void Start()
    {
        currProfile = GameControllerScript.Instance.achievementProfile; 
        if (currProfile == null)
        {
            Debug.LogError("what. the using achievements check passed even tho the profile is null LMFAO");
            Destroy(this);
            return; //not sure if this will sitll run but fuck me 
        }

        InitAchievements(); 
    }

    //we need to do some shit to fucking get rthe fucking achievements up 
    private void InitAchievements()
    {
/*        int indexer = 0; 
        foreach (Achievement achieve in currProfile.Achievements)
        {
            indexer++;
            //check if unlocked
            if (PlayerPrefs.GetInt(achieve.Name + currProfile.saveFile.ToString(), 0) == 1)                                                                     K   
            {
                Achievement tempachievemtn = new();
                tempachievemtn.Unlocked = true;
                currProfile.Achievements[indexer] = tempachievemtn; 
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
