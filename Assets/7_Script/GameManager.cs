using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 상태를 저장할 enum
    public enum State
    {
        TITLE,      // 0 : 0
        READY,      // 1 : 1
        PLAY,       // 2 : 2
        GAMEOVER,   // 3 : 3
        BESTSCORE   // 4 : 4
    }
    public static GameManager Instance;

    [SerializeField] SpriteRenderer background;
    [SerializeField] Animator floorAnim;
    [SerializeField] Animator birdAnim;
    [SerializeField] AudioClip acReady;
    [SerializeField] AudioClip acHit;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject[] stateUI;
    [SerializeField] Sprite[] bgSprite;

    State gameState; // 게임상태를 저장할 변수
    new AudioSource audio;  // 컴퍼넌트에 오디오가 있어서 new 를 붙여준다.
    public State GameState => gameState;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        // AudioSource 연결
        audio = GetComponent<AudioSource>();
        // 정상적인 게임의 시간이 흐르게 만드는것
        Time.timeScale = 1.0f;
        // 게임의 시작은 Title에서
        GameTitle();
    }

    public void PlayAudio(AudioClip clip)
    {
        // 파라미터로 넘어온 clip을 한번 플레이시킨다
        audio.PlayOneShot(clip);
    }

    void ChangeState(State value)
    {
        gameState = value;
        // stateUI에 있는 모든 UI를 끈다
        foreach (var item in stateUI)
        {
            item.SetActive(false);
        }

        // State값을 공통으로 사용하므로, 미리 int 값으로 변환
        int temp = (int)gameState;
        // 해당하는 Background sprite 연결
        background.sprite = bgSprite[temp % 2];
        // 해당하는 stateUI를 켠다
        stateUI[temp].SetActive(true);
    }

    public void GameTitle()
    {
        ChangeState(State.TITLE);
    }

    public void GameReady()
    {
        ChangeState(State.READY);
        // Ready Sound
        PlayAudio(acReady);
        // 새를 뒤로 움직인다.
        birdAnim.SetTrigger("Ready");

    }

    public void GamePlay()
    {
        ChangeState(State.PLAY);
        // 새의 Animator 는 끈다
        birdAnim.enabled = false;
    }

    public void GameOver()
    {
        ChangeState(State.GAMEOVER);
        // 피격 사운드
        PlayAudio(acHit);
        // floor 애니메이션을 멈춤다
        floorAnim.enabled = false;
        // restart 버튼을 일단 꺼둔다
        restartButton.SetActive(false);
        // 코루틴을 사용해서 잠시 시간을 지연시킨다
        StartCoroutine(StopTimer());
    }

    IEnumerator StopTimer()
    {
        // 2초 기다렸다 다음 로직 실행
        yield return new WaitForSeconds(2f);
        // 게임 시간을 멈춘다.
        Time.timeScale = 0f;
        // restart 버튼 나오게
        restartButton.SetActive(true);

    }

    public void GameBestScore()
    {
        ChangeState(State.BESTSCORE);
    }

    public void RestartGame()
    {
        // 현재 씬을 다시 불러오기
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
