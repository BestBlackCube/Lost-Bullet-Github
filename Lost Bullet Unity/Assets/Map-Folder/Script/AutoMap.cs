using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using Unity.VisualScripting;
using System.IO;

[AddComponentMenu("Custom/Auto MapObject")] // 컴포넌트 이름
[DisallowMultipleComponent] // 중복 방지
public class AutoMap : MonoBehaviour
{
    private const string MAPSCRIPT_NAME = "MapControl_Script";
    Type typeName = Type.GetType(MAPSCRIPT_NAME);

    [Header("Field Object Get In")] // 자식 맵 불러올 전체 맵
    public Transform AllMap;

    [System.Serializable] // 구조체 보이기
    public struct Field_Object
    {
        public string ElementName;
        public int Count_FrontField;
        public int Count_SkyField;
        public GameObject[] Field;
    }
    [Header("Field List")]
    public Field_Object[] InGameField_Object;


    private void Reset() // 이 컴포넌트 추가 했을때 이 오브젝트에 컨트롤러 스크립트 파일이 없는 경우
    {

        if (typeName == null)
            Debug.LogError("Not found MapControl_Script Get in Script\n Warning Script Name \"MapControl_Script\"");


        MonoScript script = MonoScript.FromMonoBehaviour(this);
        string scriptPath = AssetDatabase.GetAssetPath(script);
        string folderPath = Path.GetDirectoryName(scriptPath);
        string targetFile = Path.Combine(folderPath, $"{MAPSCRIPT_NAME}.cs");

        if (!File.Exists(targetFile)) CreateScript();
    }
#if UNITY_EDITOR
    private void OnValidate() // 인스펙터 창에서 이 컴포넌트에서 변경 사항이 생겼을 때
    {
        if (typeName == null) // 변경중 컨트롤러 스크립트 파일이 없는 경우
            Debug.LogError("Not found MapControl_Script Get in Script\n Warning Script Name \"MapControl_Script\"");

        if (AllMap == null && InGameField_Object != null) InGameField_Object = null; // 맵이 없는 경우 초기화

        if (this.GetComponent(typeName) == null) // 이 오브젝트에 MapControl_Script 파일이 없는 경우
        {
            this.gameObject.AddComponent(typeName);
            FieldInfo gameObject_Name = typeName.GetField("FieldMap");
            if (gameObject_Name != null)
            {
                gameObject_Name.SetValue(this.GetComponent(typeName), this.GetComponent<AutoMap>());
            }
        }

        EditorApplication.delayCall += () => // 불러 올때 딜레이를 넣어 과부하 제어
        {
            if (this != null && AllMap != null) FieldAutoSetup();
        };
    }
#endif

    public void FieldAutoSetup() // AllMap에 등록된 맵 파일을 불러와 구조체에 자동 인식 시키기
    {
        int MapCount = AllMap.childCount;
        InGameField_Object = new Field_Object[MapCount];

        for (int i = 0; i < MapCount; i++)
        {
            // 구조체 이름과 FieldA-Z의 자식 갯수 대입
            InGameField_Object[i].ElementName = AllMap.GetChild(i).name;
            InGameField_Object[i].Field = new GameObject[AllMap.GetChild(i).childCount];
            for (int j = 0; j < InGameField_Object[i].Field.Length; j++)
            {
                // 자식 이름 ASCII 기준 65-90까지 작동
                string CreateName;
                CreateName = "Field" + ((char)(64 + i)) + "Ground"; // Field + A-Z + Ground -> FieldAGround

                // n자식 FieldMap 오브젝트를 구조체에 대입
                Transform ChildField = AllMap.GetChild(i).transform;
                InGameField_Object[i].Field[j] = ChildField.GetChild(j).gameObject;

                if(InGameField_Object[i].Field[j].name.Contains(CreateName))
                    InGameField_Object[i].Count_FrontField++;

                CreateName = "Field" + ((char)(64 + i)) + "SkyGround"; // Field + A-Z + SkyGround -> FieldASkyGround
                if (InGameField_Object[i].Field[j].name.Contains(CreateName))
                    InGameField_Object[i].Count_SkyField++;
            }
        }
    }
    private void CreateScript() // 만약 이 스크립트를 오브젝트에 추가 할때 MapControl.cs 파일이 없는경우 생성
    {
        MonoScript script = MonoScript.FromMonoBehaviour(this);
        string scriptPath = AssetDatabase.GetAssetPath(script);
        string folderPath = Path.GetDirectoryName(scriptPath);

        string fullPath = Path.Combine(folderPath, $"{MAPSCRIPT_NAME}.cs");

        // 스크립트 내용 작성
        string scriptContent = @"using UnityEngine;

public class MapControl_Script : MonoBehaviour
{
    public AutoMap FieldMap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
";
        File.WriteAllText(fullPath, scriptContent);
        AssetDatabase.Refresh();
    }
}
