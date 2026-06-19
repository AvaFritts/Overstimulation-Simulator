using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private const string SettingsFileName = "settings";
    private const string FileExtension = ".json";
    private string SaveDirectory => Application.persistentDataPath;
    private string GetSettingsPath() => Path.Combine(SaveDirectory, $"{SettingsFileName}{FileExtension}");

    private GlobalSettingsData settingsData;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);
        LoadOrCreateSettings();
    }

    private void LoadOrCreateSettings()
    {
        string path = GetSettingsPath();
        if (!File.Exists(path)) { settingsData = new GlobalSettingsData(); WriteSettings(); return; }
        try { settingsData = JsonConvert.DeserializeObject<GlobalSettingsData>(File.ReadAllText(path)); }
        catch { settingsData = new GlobalSettingsData(); WriteSettings(); }
    }

    public GlobalSettingsData GetSettings() => settingsData;

    public void SaveSettings(float master, float music, float sfx)
    {
        settingsData.masterVolume = master;
        settingsData.musicVolume = music;
        settingsData.sfxVolume = sfx;
        WriteSettings();
    }


    public void LoadSettings(out float master, out float music, out float sfx)
    {
        master = settingsData.masterVolume;
        music = settingsData.musicVolume;
        sfx = settingsData.sfxVolume;
    }

    public void SaveProgress()
    {

    }

    public void WriteProgress()
    {

    }

    private void WriteSettings()
    {
        try { File.WriteAllText(GetSettingsPath(), JsonConvert.SerializeObject(settingsData, Formatting.Indented)); }
        catch (Exception e) { Debug.LogError($"[SaveManager] Failed to write settings: {e.Message}"); }
    }
}
