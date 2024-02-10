using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioandVisMGR : MonoBehaviour
{
    public int audioLevel, Visual, audioSet, VisualSet;
    public float MusicFloat, SFXFloat;
    public Slider SFX, Music;
    public GameObject MGO;
    public AudioSource MAS, SFXAS;
    public GameObject[] sfx;
    void Start()
    {
        MGO = GameObject.FindGameObjectWithTag("music");
        sfx = GameObject.FindGameObjectsWithTag("sfx");
        MAS = MGO.GetComponent<AudioSource>();
        MusicFloat = PlayerPrefs.GetFloat("music", 1);
        MAS.volume = MusicFloat;
        SFXFloat = PlayerPrefs.GetFloat("SFX", 1);
        foreach (GameObject s in sfx)
        {
            SFXAS = s.GetComponent<AudioSource>();
            SFXAS.volume = SFXFloat;
        }
        Visual = PlayerPrefs.GetInt("Visual", 2);
        switch (Visual)
        {
            case 1:
                Screen.SetResolution(1280, 720, true);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 3:
                Screen.SetResolution(2560, 1440, true);
                break;
            case 4:
                Screen.SetResolution(3440, 1440, true);
                break;
        }
    }

    public void setAudio()
    {
        PlayerPrefs.SetFloat("music", Music.value);
        PlayerPrefs.SetFloat("SFX", SFX.value);
        foreach (GameObject s in sfx)
        {
            SFXAS = s.GetComponent<AudioSource>();
            SFXAS.volume = SFXFloat;
        }
        MAS.volume = Music.value;
    }

    public void set720()
    {
        PlayerPrefs.SetInt("Visual", 1);
        Screen.SetResolution(1280, 720, true);
    }
    public void set1080()
    {
        PlayerPrefs.SetInt("Visual", 2);
        Screen.SetResolution(1920, 1080, true);
    }
    public void set2560()
    {
        PlayerPrefs.SetInt("Visual", 3);
        Screen.SetResolution(2560, 1440, true);
    }
    public void set3440()
    {
        PlayerPrefs.SetInt("Visual", 4);
        Screen.SetResolution(3440, 1440, true);
    }
}
