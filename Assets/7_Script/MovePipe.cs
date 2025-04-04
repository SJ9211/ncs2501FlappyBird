using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] float speed = 0.65f;
    [SerializeField] BoxCollider2D upPipe;
    [SerializeField] BoxCollider2D downPipe;

    private void Update()
    {
        // 게임상태가 PLAY 일때만 움직이도록
        if (GameManager.Instance.GameState == GameManager.State.PLAY)
        {
            // 파이프의 위치를 speed 만큼 좌로 이동
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (GameManager.Instance.GameState == GameManager.State.GAMEOVER)
        {
            // 게임오버상태에서는 파이프의 충돌이 안 일어나게
            upPipe.enabled = downPipe.enabled = false;
        }
    }
}
