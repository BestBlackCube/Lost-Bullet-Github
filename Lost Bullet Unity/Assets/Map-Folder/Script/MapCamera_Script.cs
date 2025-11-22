using UnityEngine;
using UnityEngine.InputSystem;

public class MapCamera_Script : MonoBehaviour
{
    [SerializeField] Camera MapCamera;
    [SerializeField] AutoMap MapTransform;
    Vector3 MapT;
    Vector3 CameraT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraT = MapCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.minusKey.wasPressedThisFrame)
        {
            if(MapCamera.orthographicSize > 0)
                MapCamera.orthographicSize -= 1;
        }
        if(Keyboard.current.equalsKey.wasPressedThisFrame)
        {
            if(MapCamera.orthographicSize < 30) 
                MapCamera.orthographicSize += 1;
        }

        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            CameraMove("Field-A");
        }
        if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            CameraMove("Field-B");
        }
        if(Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            CameraMove("Field-C");
        }
        if(Keyboard.current.digit4Key.wasPressedThisFrame)
        {

        }
        if(Keyboard.current.upArrowKey.isPressed) MapCamera.transform.position = new Vector3(CameraT.x, CameraT.y += 0.1f + Time.deltaTime, CameraT.z);
        if(Keyboard.current.downArrowKey.isPressed) MapCamera.transform.position = new Vector3(CameraT.x, CameraT.y -= 0.1f + Time.deltaTime, CameraT.z);
        if(Keyboard.current.leftArrowKey.isPressed) MapCamera.transform.position = new Vector3(CameraT.x -= 0.1f + Time.deltaTime, CameraT.y, CameraT.z);
        if(Keyboard.current.rightArrowKey.isPressed) MapCamera.transform.position = new Vector3(CameraT.x += 0.1f + Time.deltaTime, CameraT.y, CameraT.z);
    }
    void CameraMove(string N)
    {
        Transform MapObject = GameObject.Find(N).transform;
        MapT = new Vector3(MapObject.transform.position.x, MapObject.transform.position.y, MapCamera.transform.position.z);
        MapCamera.transform.position = MapT;
    }
}
