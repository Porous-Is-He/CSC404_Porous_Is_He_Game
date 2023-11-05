using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>Manages data for persistance between play sessions.</summary>
public class PlayerInfoManager : MonoBehaviour 
{
    public float sensitivity = 0;
    public float aimSensitivity = 0;
    public bool useController = true;
    public bool showControls = true;
    public bool hardmode = false;

    private static string sensitivityKey = "PLAYER_SENSITIVITY";
    private static string aimSensitivityKey = "PLAYER_AIMSENSITIVITY";
    private static string useControllerKey = "PLAYER_USECONTROLLER";
    private static string showControlsKey = "PLAYER_SHOWCONTROLS";
    private static string hardmodeKey = "PLAYER_HARDMODE";

    public void Save()
    {
        // Set the values to the PlayerPrefs file using their corresponding keys.
        PlayerPrefs.SetFloat(sensitivityKey, sensitivity);
        PlayerPrefs.SetFloat(aimSensitivityKey, aimSensitivity);
        PlayerPrefs.SetInt(useControllerKey, Convert.ToInt32(useController));
        PlayerPrefs.SetInt(showControlsKey, Convert.ToInt32(showControls));
        PlayerPrefs.SetInt(hardmodeKey, Convert.ToInt32(hardmode));

        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(sensitivityKey))
        {
            sensitivity = PlayerPrefs.GetFloat(sensitivityKey);
        }

        if (PlayerPrefs.HasKey(aimSensitivityKey))
        {
            aimSensitivity = PlayerPrefs.GetFloat(aimSensitivityKey);
        }
        
        if (PlayerPrefs.HasKey(useControllerKey))
        {
            useController = PlayerPrefs.GetInt(useControllerKey) == 0 ? false : true;
        }

        if (PlayerPrefs.HasKey(showControlsKey))
        {
            showControls = PlayerPrefs.GetInt(showControlsKey) == 0 ? false : true;
        }

        if (PlayerPrefs.HasKey(hardmodeKey))
        {
            hardmode = PlayerPrefs.GetInt(hardmodeKey) == 0 ? false : true;
        }
    }

    /// <summary>Deletes all values from the PlayerPrefs file.</summary>
    public void Delete()
    {
        // Delete all values from the PlayerPrefs file.
        PlayerPrefs.DeleteAll();
    }
}