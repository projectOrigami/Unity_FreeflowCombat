using KWS;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class DemoGUI: MonoBehaviour
{
    public GameObject button;
    public GameObject slider;

    public Camera cam;
    public Light sun;
    public GameObject environment;
    public WaterSystem water;

    public GameObject terrain;
    public Terrain terrainDetails;
    // public SEGI segiPostFX;
    public PlayableDirector timeline;

    int buttonOffset = 35;
    int sliderOffset = 25;
    Vector2 currentElementOffset;

    List<GameObject> waterUIElements = new List<GameObject>();

    GameObject CreateButton(string text, Action action, bool currentActive, bool isWaterElement, params string[] prefixStatus)
    {
        var instance = Instantiate(button, transform);
        if (isWaterElement) waterUIElements.Add(instance);
        var uiElement = instance.GetComponent<KWS_DemoUIElement>();
        uiElement.Initialize(text, action, currentActive, prefixStatus);
       
        uiElement.Rect.anchoredPosition = currentElementOffset;
        currentElementOffset.y -= buttonOffset;
        return instance;
    }

    GameObject CreateSlider(string text, Action<float> action, bool isWaterElement = false)
    {
        var instance = Instantiate(slider, transform);
        if (isWaterElement) waterUIElements.Add(instance);
        var uiElement = instance.GetComponent<KWS_DemoUIElement>();
        uiElement.Initialize(text, action);
        uiElement.Rect.anchoredPosition = currentElementOffset;
        currentElementOffset.y -= sliderOffset;
        return instance;
    }

    void Start () 
    {
//#if KWS_DEBUG
//        var notes = GetComponentInChildren<Text>();
//        if(notes != null) notes.enabled = false;
//#endif

        currentElementOffset = new Vector2(10, -10);

        CreateButton("Next Scene", () =>
        {
            var currentSceneID = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneID < SceneManager.sceneCountInBuildSettings - 1) currentSceneID++;
            else currentSceneID = 0;
            SceneManager.LoadScene(currentSceneID);
        }, currentActive: true, false);

        CreateButton("Previous Scene", () =>
         {
             var currentSceneID = SceneManager.GetActiveScene().buildIndex;
             if (currentSceneID > 0) currentSceneID--;
             else currentSceneID = 0;
             SceneManager.LoadScene(currentSceneID);
         },
            currentActive: true, false);

        if (sun != null)
        {
            CreateButton("Shadows", () =>
            {
                sun.shadows = (sun.shadows == LightShadows.None) ? sun.shadows = LightShadows.Soft : LightShadows.None;
            }, 
            currentActive: true, false, "On", "Off");
        }

        if(environment != null)
        {
            CreateButton("Environment", () =>
            {
                environment.gameObject.SetActive(!environment.gameObject.activeSelf);
            },
           currentActive: true, false, "On", "Off");
        }

        if (terrain != null)
        {
            CreateButton("Terrain", () =>
            {
                terrain.SetActive(!terrain.activeSelf);
            },
           currentActive: true, false, "On", "Off");
        }

        if (terrainDetails != null)
        {
            CreateButton("Terrain details", () =>
            {
                terrainDetails.drawTreesAndFoliage = !terrainDetails.drawTreesAndFoliage;
            },
            currentActive: true, false, "On", "Off");
        }

        if (water != null)
        {
            CreateButton("Water", () =>
            {
                water.gameObject.SetActive(!water.gameObject.activeSelf);
                SetWaterUIElementsActiveStatus(water.gameObject.activeSelf);
            },
           currentActive: true, false, "On", "Off");
        }

        InitializeWaterUI();

        CreateButton("Quit", () =>
        {
            Application.Quit();
        }, currentActive: true, false);
    }

    void SetWaterUIElementsActiveStatus(bool isActive)
    {
        foreach(var element in waterUIElements)
        {
            element.SetActive(isActive);
        }
    }

    int shorelineQuality = 1;
    void InitializeWaterUI()
    {
        var settings = water.Settings;
        currentElementOffset.y -= 50;
        CreateSlider("Transparent", (sliderVal) =>
        { settings.Transparent = Mathf.Lerp(0.1f, 20f, sliderVal);
        }, true);


        CreateButton("Flowing", () =>
        {
            settings.UseFlowMap = !settings.UseFlowMap;
        },
        currentActive: settings.UseFlowMap, true, "On", "Off");

        CreateButton("Dynamic waves", () =>
        {
            settings.UseDynamicWaves = !settings.UseDynamicWaves;
        },
        currentActive: settings.UseDynamicWaves, true, "On", "Off");

        CreateButton("Shoreline", () =>
        {
            settings.UseShorelineRendering = !settings.UseShorelineRendering;
        },
        currentActive: settings.UseShorelineRendering, true, "On", "Off");

        CreateButton("Volumetric Lighting", () =>
        {
            settings.UseVolumetricLight = !settings.UseVolumetricLight;
        },
        currentActive: settings.UseVolumetricLight, true, "On", "Off");

        CreateButton("Caustic Effect", () =>
        {
            settings.UseCausticEffect = !settings.UseCausticEffect;
        },
        currentActive: settings.UseCausticEffect, true, "On", "Off");

        CreateButton("Underwater Effect", () =>
        {
            settings.UseUnderwaterEffect = !settings.UseUnderwaterEffect;
        },
        currentActive: settings.UseUnderwaterEffect, true, "On", "Off");

        CreateButton("Draw to Depth", () =>
        {
            settings.DrawToPosteffectsDepth = !settings.DrawToPosteffectsDepth;
        },
                     currentActive: settings.DrawToPosteffectsDepth, true, "On", "Off");

        CreateButton("Use Tesselation", () =>
        {
            settings.UseTesselation = !settings.UseTesselation;
        },
         currentActive: settings.UseTesselation, true, "On", "Off");
    }
}
