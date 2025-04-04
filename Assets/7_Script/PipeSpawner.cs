using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] float maxTime = 1.5f;          // 몇초 마다 생성할건지
    [SerializeField] float heightRange = 0.5f;      // 생성위치 y의 랜덤 범위
    [SerializeField] GameObject pipePrefab;         // 파이프의 Prefab 연결
    [SerializeField] GameObject RedPipePrefab;      // 빨간 파이프 프레팝 연결
    float timer;

    void Update()
    {
        // 게임상태가 PLAY일때만 파이프를 생성
        if (GameManager.Instance.GameState != GameManager.State.PLAY) return;

        // timer 가 maxTime 를 넘으면
        if (timer > maxTime)
        {
            // pipe를 만드는 함수 호출
            SpawnPipe();
            // timer는 0로 
            timer = 0;
        }
        // timer에 시간 더하기     
        timer += Time.deltaTime;
    }

    void SpawnPipe()
    {
        // 랜덤으로 y값을 정해서 생설될 파이프의 위치 정하기
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));
        // 랜덤으로 녹색, 빨간색 파이프 선택
        GameObject pipePf = (Random.Range(0, 100) > 10) ? pipePrefab : RedPipePrefab;
        // Instantiate 로 생성, 생성된 객체는 pipe라는 GameObject에 할당
        GameObject pipe = Instantiate(pipePrefab, spawnPos, Quaternion.identity);
        // 5초 뒤에 pipe 객체 파괴
        Destroy(pipe, 5f);
    }
}
