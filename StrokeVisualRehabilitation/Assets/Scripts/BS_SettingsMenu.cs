using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class BS_SettingsMenu : MonoBehaviour {

    // Public variables
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;

    // Private variables
    Resolution[] resolutions;

    /// <summary>
    /// On scene start
    /// </summary>
    private void Start()
    {
        // Get current resolution options
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        // Add resolution options to dropdown menu
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Save current resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                 resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);

        // Set current resolution on dropdown menu
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    /// <summary>
    /// Set new resolution
    /// </summary>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Set new volume from slider
    /// </summary>
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    /// <summary>
    /// Set new quality
    /// </summary>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Make game fullscreen or not if checkbox is changed
    /// </summary>
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
