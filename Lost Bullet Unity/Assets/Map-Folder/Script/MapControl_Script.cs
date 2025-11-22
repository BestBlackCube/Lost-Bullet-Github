using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapControl_Script : MonoBehaviour
{
    public AutoMap FieldMap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        CreateAllMap();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            CreateAllMap();
        }
    }
    void CreateAllMap() // Front ¸Ê »ý¼º ÄÚµå
    {
        for(int i = 1; i < FieldMap.InGameField_Object.Length; i++)
        {
            for (int j = 0; j < FieldMap.InGameField_Object[i].Count_FrontField; j++)
            {
                if (FieldMap.InGameField_Object[i].Field[j].activeSelf)
                    FieldMap.InGameField_Object[i].Field[j].SetActive(false);
            }
            int choiceMap = Random.Range(1, FieldMap.InGameField_Object[i].Count_FrontField);
            Debug.Log("Field" + ((char)(64 + i)) + ": " + choiceMap);
            FieldMap.InGameField_Object[i].Field[choiceMap].SetActive(true);
        }
    }
}
