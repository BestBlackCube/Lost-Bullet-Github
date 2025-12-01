using UnityEngine;

public class Camera2D : MonoBehaviour
{
    public Transform target;

    [Header("Dead zone 크기")]
    public float deadW = 4f;    //데드존 좌우 거리
    public float deadH = 2f;    //데드존 상하 거리

    [Header("카메라 부드러움")]
    public float smoothTime = 0.15f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if(target == null) return;

        Vector3 camPos = transform.position;
        Vector3 targetPos = target.position;

        //z값 고정
        targetPos.z = camPos.z;

        //데드존 계산
        float left = camPos.x - deadW * 0.5f;
        float right = camPos.x + deadW * 0.5f;
        float bottom = camPos.y - deadH * 0.5f;
        float top = camPos.y + deadH * 0.5f;
        
        float newX = camPos.x;
        float newY = camPos.y;

        // 플레이어가 데드존을 벗어났을 때만 카메라 이동
        if (targetPos.x < left) newX = targetPos.x + deadW * 0.5f;
        if (targetPos.x > right) newX = targetPos.x - deadW * 0.5f;

        if (targetPos.y < bottom) newY = targetPos.y + deadH * 0.5f;
        if (targetPos.y > top) newY = targetPos.y - deadH * 0.5f;

        Vector3 desiredPos = new Vector3(newX, newY, camPos.z);
        
        transform.position = Vector3.SmoothDamp(camPos, desiredPos, ref velocity, smoothTime);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
