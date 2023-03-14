using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using Types;

public class EnemyDesignerWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D rogueSectionTexture;

    Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;

    static MageData mageData;
    static WarriorData warriorData;
    static RogueData rogueData;

    public static MageData MageInfo { get { return mageData; } }
    public static WarriorData WarriorInfo { get { return warriorData; } }
    public static RogueData RogueInfo { get { return rogueData; } }

    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }
    /// <summary>
    /// Similar to Start() or Awake()
    /// </summary>
    void OnEnable()
    {
        InitTextures();
        InitData();
    }
    public static void InitData()
    {
        mageData = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
        warriorData = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
        rogueData = (RogueData)ScriptableObject.CreateInstance(typeof(RogueData));
    }
    /// <summary>
    /// Initialize Texture 2D values
    /// </summary>
    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        mageSectionTexture = Resources.Load<Texture2D>("icons/mage");
        warriorSectionTexture = Resources.Load<Texture2D>("icons/warrior");
        rogueSectionTexture = Resources.Load<Texture2D>("icons/rogue");
    }
    /// <summary>
    /// Similar to any Update function,
    /// Not called once por frame. Called 1 or more times per interaction.
    /// </summary>
    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
    }
    /// <summary>
    /// Defines Rect values and paints textures base on Rects
    /// </summary>
    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        mageSection.x = 0;
        mageSection.y = 50;
        mageSection.width = Screen.width / 3f;
        mageSection.height = Screen.width - 50;

        warriorSection.x = Screen.width / 3f;
        warriorSection.y = 50;
        warriorSection.width = Screen.width / 3f;
        warriorSection.height = Screen.width - 50;

        rogueSection.x = Screen.width / 3f * 2;
        rogueSection.y = 50;
        rogueSection.width = Screen.width / 3f;
        rogueSection.height = Screen.width - 50;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
    }
    /// <summary>
    /// Draw contents of header
    /// </summary>
    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
        GUILayout.Label("Enemy Designer");
        GUILayout.EndArea();
    }
    /// <summary>
    /// Draw contents of mage region
    /// </summary>
    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);
        GUILayout.Label("Mage");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Damage: ");
        mageData.dmgType = (MageDmgType)EditorGUILayout.EnumPopup(mageData.dmgType);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Waepon: ");
        mageData.wpnType = (MageWpnType)EditorGUILayout.EnumPopup(mageData.wpnType);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }

        GUILayout.EndArea();
    }
    /// <summary>
    /// Draw contents of warrior region
    /// </summary>
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);
        GUILayout.Label("Warrior");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Class: ");
        warriorData.classType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.classType);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Waepon: ");
        warriorData.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(warriorData.wpnType);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }

        GUILayout.EndArea();
    }
    /// <summary>
    /// Draw contents of rogue region
    /// </summary>
    void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogueSection);
        GUILayout.Label("Rogue");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Strategy: ");
        rogueData.strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(rogueData.strategyType);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Waepon: ");
        rogueData.wpnType = (RogueWpnType)EditorGUILayout.EnumPopup(rogueData.wpnType);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }

        GUILayout.EndArea();
    }

    public class GeneralSettings : EditorWindow
    {
        public enum SettingsType
        {
            MAGE,
            WARRIOR,
            ROGUE
        }

        static SettingsType dataSetting;
        static GeneralSettings window;

        public static void OpenWindow(SettingsType setting)
        {
            dataSetting = setting;
            window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
            window.minSize = new Vector2(250, 200);
            window.Show();
        }

        void OnGUI()
        {
            switch (dataSetting)
            {
                case SettingsType.MAGE:
                    DrawSettings((CharacterData)EnemyDesignerWindow.MageInfo);
                    break;
                case SettingsType.WARRIOR:
                    DrawSettings((CharacterData)EnemyDesignerWindow.WarriorInfo);
                    break;
                case SettingsType.ROGUE:
                    DrawSettings((CharacterData)EnemyDesignerWindow.RogueInfo);
                    break;
                default:
                    break;
            }
        }

        void DrawSettings(CharacterData charData)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab");
            charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();
            if (charData.prefab == null)
            {
                EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created.", MessageType.Warning);
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Max Health: ");
            charData.maxHealth = EditorGUILayout.FloatField(charData.maxHealth);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Max Energy: ");
            charData.maxEnergy = EditorGUILayout.FloatField(charData.maxEnergy);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Power: ");
            charData.power = EditorGUILayout.Slider(charData.power, 0, 100);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("% Crit Chance: ");
            charData.critChance = EditorGUILayout.Slider(charData.critChance, 0, charData.power);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Name: ");
            charData.name = EditorGUILayout.TextField(charData.name);
            EditorGUILayout.EndHorizontal();

            if (charData.name == null || charData.name.Length < 1)
            {
                EditorGUILayout.HelpBox("This enemy needs a [Name] before it can be created.", MessageType.Warning);
            }

            if (charData.prefab != null && charData.name != null)
            {
                if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
                {
                    SaveCharacterData();
                    window.Close();
                }
            }


        }

        void SaveCharacterData()
        {
            string prefabPath; //path to the base prefab
            string newPrefabPath = "Assets/prefabs/characters/";
            string dataPath = "Assets/resources/characterData/data/";

            switch (dataSetting)
            {
                case SettingsType.MAGE:
                    //create the .asset file
                    dataPath += "mage/" + EnemyDesignerWindow.MageInfo.name + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath);

                    newPrefabPath += "mage/" + EnemyDesignerWindow.MageInfo.name + ".prefab";
                    //get prefab path
                    prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));

                    if (!magePrefab.GetComponent<Mage>())
                    {
                        magePrefab.AddComponent(typeof(Mage));
                    }

                    magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.MageInfo;

                    break;
                case SettingsType.WARRIOR:
                    //create the .asset file
                    dataPath += "warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.WarriorInfo, dataPath);

                    newPrefabPath += "warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".prefab";
                    //get prefab path
                    prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.WarriorInfo.prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject warriorPrefrab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));

                    if (!warriorPrefrab.GetComponent<Warrior>())
                    {
                        warriorPrefrab.AddComponent(typeof(Warrior));
                    }

                    warriorPrefrab.GetComponent<Warrior>().warriorData = EnemyDesignerWindow.WarriorInfo;
                    break;
                case SettingsType.ROGUE:
                    //create the .asset file
                    dataPath += "rogue/" + EnemyDesignerWindow.RogueInfo.name + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.RogueInfo, dataPath);

                    newPrefabPath += "rogue/" + EnemyDesignerWindow.RogueInfo.name + ".prefab";
                    //get prefab path
                    prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.RogueInfo.prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject roguePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));

                    if (!roguePrefab.GetComponent<Rogue>())
                    {
                        roguePrefab.AddComponent(typeof(Rogue));
                    }

                    roguePrefab.GetComponent<Rogue>().rogueData = EnemyDesignerWindow.RogueInfo;
                    break;
                default:
                    break;
            }
        }
    }
}
