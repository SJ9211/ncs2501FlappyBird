using Unity.VisualScripting;
using UnityEngine;
// using을 사용해서 자주 쓰는 namespace, enum 을 줄여 쓸 수 있다.
using GMState = GameManager.State;
public class BirdControl : MonoBehaviour
{
    [SerializeField] float velocity = 1.5f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] AudioClip acWing;
    [SerializeField] AudioClip acDie;
    Rigidbody2D rb;
    GameManager gmi;

    private void Start()
    {
        // 자주 사용하는 만든 instance 연결
        gmi = GameManager.Instance;
        // rigidbody 연결
        rb = GetComponent<Rigidbody2D>();
        // Bird가 안 떨어지게 중력 값을 조정
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        // 마우스 클릭 (화면 터치)를 하면 위로 움직이게
        if (Input.GetMouseButtonDown(0))
        {
            // 게임상태가 READY면
            if (gmi.GameState == GMState.READY)
            {
                // 게임상태를 PLAY로 바꾸기
                gmi.GamePlay();
                // Bird가 떨어지도록 중력 값 조절
                rb.gravityScale = 1.0f;
            }
            else if (gmi.GameState == GMState.PLAY)       // 게임상태가 PLAY면
            {
                // 새가 나는 소리
                gmi.PlayAudio(acWing);
                // 위로 이동
                rb.velocity = Vector2.up * velocity;
            }

        }
    }

    private void FixedUpdate()
    {
        // update에서 변경된 velocity.y 값만큼 회전
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * rotateSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 게임 PLAY 일때만 충돌감지
        if (gmi.GameState != GMState.PLAY) return;

        //  게임오버
        gmi.GameOver();
        // 새의 Flap 애니메이션을 멈춤
        GetComponent<Animator>().enabled = false;
        // 새의 y좌표에 따라 소리 재생
        if (transform.position.y > -0.25f)
        {
            // 새가 떨어지는 소리
            gmi.PlayAudio(acDie);
        }
    }
}
