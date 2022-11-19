using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public TextMeshProUGUI input;
    public string name;
    public string current_name;
    public int score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Transition()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateName()
    {
        current_name = input.text;
    }

    public void Clear()
    {
        SaveData data = new SaveData();
        data.name = "NA";
        data.score = 0;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Load();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }
    [System.Serializable]
    class SaveData
    {
        public string name;
        public int score;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.name = current_name;
        data.score = GameObject.Find("MainManager").GetComponent<MainManager>().m_Points;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            name = data.name;
            score = data.score;
        }
    }
}
