using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    [SerializeField] int scoreValue; // 파이프 종류별로 얻게되는 점수 지정
    void OnTriggerEnter2D(Collider2D collision)
    {
        // "Player"라는 태그로 들어온 트리거만 인식
        if ( collision.gameObject.CompareTag("Player"))
        {
            // scoreValue 값을 실제 score에 업데이트
            ScoreManager.Instance.UpdateScore(scoreValue);
        }
    }
}
