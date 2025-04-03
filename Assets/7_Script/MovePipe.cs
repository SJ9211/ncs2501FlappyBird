using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] float speed = 0.65f;
    
    void Update()
    {
        // 파이프의 위치를 speed 만큼 좌로 이동
        transform.position += Vector3.left * speed * Time.deltaTime;    
    }
}
