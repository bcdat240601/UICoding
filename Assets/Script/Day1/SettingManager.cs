using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : SetupBehaviour
{
    [SerializeField] protected Slider soundSlider;
    [SerializeField] protected Slider musicSlider;
    [SerializeField] protected Dropdown graphicDropdown;
    [SerializeField] protected Dropdown languageDropdown;
    [SerializeField] protected SettingSO settingSO;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetSoundSlider();
        GetMusicSlider();
        GetGraphicDropdown();
        GetLanguageDropdown();
        GetSettingSO();
    }
    protected override void Awake()
    {
        base.Awake();
        LoadSetting();
    }

    protected virtual void GetSettingSO()
    {
        if (settingSO != null) return;
        string path = "Setting/SettingSO";
        settingSO = Resources.Load<SettingSO>(path);
        if (settingSO == null)
        {
            CreateSettingSO();
            settingSO = Resources.Load<SettingSO>(path);
        }
        Debug.Log("Reset " + nameof(settingSO) + " in " + GetType().Name);

    }
    protected virtual void CreateSettingSO()
    {
        SettingSO setting = ScriptableObject.CreateInstance<SettingSO>();        

        string path = "Assets/Resources/Setting/SettingSO.asset";        
        AssetDatabase.CreateAsset(setting, path);

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();
    }

    protected virtual void GetSoundSlider()
    {
        if (soundSlider != null) return;
        soundSlider = transform.Find("Sound").GetComponentInChildren<Slider>();
        Debug.Log("Reset " + nameof(soundSlider) + " in " + GetType().Name);
    }
    protected virtual void GetMusicSlider()
    {
        if (musicSlider != null) return;
        musicSlider = transform.Find("Music").GetComponentInChildren<Slider>();
        Debug.Log("Reset " + nameof(musicSlider) + " in " + GetType().Name);
    }
    protected virtual void GetGraphicDropdown()
    {
        if (graphicDropdown != null) return;
        graphicDropdown = transform.Find("Graphic").GetComponentInChildren<Dropdown>();
        Debug.Log("Reset " + nameof(graphicDropdown) + " in " + GetType().Name);
    }
    protected virtual void GetLanguageDropdown()
    {
        if (languageDropdown != null) return;
        languageDropdown = transform.Find("Language").GetComponentInChildren<Dropdown>();
        Debug.Log("Reset " + nameof(languageDropdown) + " in " + GetType().Name);
    }
    public virtual void SetSetting()
    {
        settingSO.Sound = soundSlider.value;
        settingSO.Music = musicSlider.value;
        settingSO.Graphic = graphicDropdown.value;
        settingSO.Language = languageDropdown.value;
        string json = JsonUtility.ToJson(settingSO);
        string path = Application.persistentDataPath + "/setting.json";
        File.WriteAllText(path, json);
    }
    public virtual void LoadSetting()
    {
        string path = Application.persistentDataPath + "/setting.json";
        if (!File.Exists(path))
        {
            Debug.LogWarning("File is not created");
            return;
        }
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, settingSO);
        soundSlider.SetValueWithoutNotify(settingSO.Sound);
        musicSlider.SetValueWithoutNotify(settingSO.Music);
        graphicDropdown.SetValueWithoutNotify(settingSO.Graphic);
        languageDropdown.SetValueWithoutNotify(settingSO.Language);
    }
}
