using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class EnemyDesignerWindow : EditorWindow
{

    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D rogueSectionTexture;

    Color headerSectioncolor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);
    Color mageSectioncolor = new Color(109f / 255f, 56f / 255f, 209f / 255f, 1f);
    Color rogueSectioncolor = new Color(56f / 255f, 89f / 255f, 209f / 255f, 1f);
    Color warriorSectioncolor = new Color(209f / 255f, 56f / 255f, 201f / 255f, 1f);

    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;

    private GUIStyle titleStyle = new GUIStyle();
    private GUIStyle classStyle = new GUIStyle();

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
    private void OnEnable()
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
    /// Initialize Texture2D values
    /// </summary>
    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectioncolor);
        headerSectionTexture.Apply();

        mageSectionTexture = new Texture2D(1, 1);
        mageSectionTexture.SetPixel(0, 0, mageSectioncolor);
        mageSectionTexture.Apply();

        rogueSectionTexture = new Texture2D(1, 1);
        rogueSectionTexture.SetPixel(0, 0, rogueSectioncolor);
        rogueSectionTexture.Apply();

        warriorSectionTexture = new Texture2D(1, 1);
        warriorSectionTexture.SetPixel(0, 0, warriorSectioncolor);
        warriorSectionTexture.Apply();


    }
    /// <summary>
    /// Similar to update, but called 1 or more times per interactions with UI
    /// </summary>
    private void OnGUI()
    {
        titleStyle.fontSize = 30;
        classStyle.fontSize = 20;
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawRogueSettings();
        DrawWarriorSettings();
    }
    /// <summary>
    /// Defines Rect values and paints textures based on rects
    /// </summary>
    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        mageSection.x = 0;
        mageSection.y = 50;
        mageSection.width = position.width /3f;
        mageSection.height = position.width - 50;

        rogueSection.x = position.width / 3f;
        rogueSection.y = 50;
        rogueSection.width = position.width /3f;
        rogueSection.height = position.width - 50;

        warriorSection.x = (position.width / 3f) * 2;
        warriorSection.y = 50;
        warriorSection.width = position.width/3f;
        warriorSection.height = position.width - 50;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);


    }
    /// <summary>
    /// Draw contents of header
    /// </summary>
    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
        GUILayout.Label("Enemy Designer", titleStyle);
        GUILayout.EndArea();
    }
    /// <summary>
    /// Draw contents of Mage
    /// </summary>
    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);
        GUILayout.Label("Mage", classStyle);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage Type");
        mageData.dmgType = (MageDmgType)EditorGUILayout.EnumPopup(mageData.dmgType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon Type");
        mageData.wpnType = (MageWpnType)EditorGUILayout.EnumPopup(mageData.wpnType);
        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }
        GUILayout.EndArea();
    }
    /// <summary>
    /// Draw contents of Warrior
    /// </summary>
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);
        GUILayout.Label("Warrior", classStyle);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon Type");
        warriorData.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(warriorData.wpnType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage Class");
        warriorData.classType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.classType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }
        GUILayout.EndArea();
    }
    /// <summary>
    /// Draw contents of Rogue
    /// </summary>
    void DrawRogueSettings() {
        GUILayout.BeginArea(rogueSection);
        GUILayout.Label("Rogue", classStyle);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon Type");
        rogueData.wpnType = (RogueWpnType)EditorGUILayout.EnumPopup(rogueData.wpnType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Attack Strategy");
        rogueData.strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(rogueData.strategyType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }
        GUILayout.EndArea();
    }

    public class GeneralSettings: EditorWindow
    {
        public enum SettingsType
        {
            MAGE, WARRIOR, ROGUE
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

        private void OnGUI()
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
                    DrawSettings((CharacterData)EnemyDesignerWindow.WarriorInfo);
                    break;
            }
        }

        void DrawSettings(CharacterData charData)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab Object");
            charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();

            charData.maxHealth = EditorGUILayout.FloatField("Max Health", charData.maxHealth);
            charData.maxEnergy = EditorGUILayout.FloatField("Max Energy", charData.maxEnergy);
            charData.power = EditorGUILayout.Slider("Power", charData.power, 0, 100);
            charData.critChance = EditorGUILayout.Slider("Crit Chance %", charData.critChance, 0, charData.power);
            charData.charName = EditorGUILayout.TextField("Name", charData.charName);
            
            if(charData.prefab == null)
            {
                EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created.", MessageType.Warning);
            }else if (charData.charName == null || charData.charName.Length < 2)
            {
                EditorGUILayout.HelpBox("This enemy needs a [Name] longer than 1 letter.", MessageType.Warning);
            }else if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
            {
                SaveCharacterData();
                window.Close();
            }
        }

        void SaveCharacterData()
        {
            string prefabPath; // path to base prefab
            string newPrefabPath = "Assets/prefabs/characters/";
            string dataPath = "Assets/resources/characterData/data/";

            switch(dataSetting)
            {
                case SettingsType.MAGE:
                    // create the .asset file
                    dataPath += "mage/" + EnemyDesignerWindow.MageInfo.charName + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath);

                    // get new prefab path
                    newPrefabPath += "mage/" + EnemyDesignerWindow.MageInfo.charName + ".prefab";

                    // find where the prefab is exactly
                    prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    // Get the prefab we just copied into the folder
                    GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));

                    // if the prefab does nto have mage we need to make sure it gets one
                    if (!magePrefab.GetComponent<Mage>())
                        magePrefab.AddComponent(typeof(Mage));

                    magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.MageInfo;

                    break;
            }
        }
    }
        
}
