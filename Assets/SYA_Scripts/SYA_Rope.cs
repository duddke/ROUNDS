using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_Rope : MonoBehaviour
{
    public GameObject End;

    public LineRenderer lineRenderer;
    //라인을 이루는 동그라미의 갯수(세그먼트)
    public int segmentCount=15;
    //세그먼트의 길이
    public float segmentLength=0.1f;
    //로프 너비
    public float ropeWidth = 0.1f;
    //중력
    public Vector2 gravity = new Vector2(0f, -9.81f);
    //처음위치 가져오기()트랜스폼)
    [Space(10f)]
    public Transform startTransform;

    //세그먼트 리스트
    public List<Segment> segments = new List<Segment>();

    //리셋할 경우 라인렌더러 가져오기
    private void Reset()
    {
        TryGetComponent(out lineRenderer);
    }

    //Awake에서 리스트 추가
    private void Awake()
    {
        //처음 위치에서 점점 아래쪽으로 내려가서 세그먼트 만들어주기
        Vector2 segmentPos = startTransform.position;
        for(int i=0; i<segmentCount; i++)
        {
            segments.Add(new Segment(segmentPos));
            segmentPos.y -= segmentLength;
        }
    }

    //움직여주기
    private void FixedUpdate()
    {
        UpdateSegments();
        ApplyConstraint();
        DrawRope();
        
    }

    //로프를 라인렌더러로 그리는 함수
    private void DrawRope()
    {
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        Vector3[] segmentPositions = new Vector3[segments.Count];
        for(int i=0; i< segments.Count; i++)
        {
            segmentPositions[i] = segments[i].position;
        }
        //라인렌더러의 위치들을 넣어주기
        lineRenderer.positionCount = segmentPositions.Length;
        lineRenderer.SetPositions(segmentPositions);
    }

    //세그먼트를 움직여줄 함수
    private void UpdateSegments()
    {
        for(int i=0; i< segments.Count; i++)
        {
            //속도
            segments[i].velocity = segments[i].position - segments[i].previousPos;
            //이전 위치
            segments[i].previousPos = segments[i].position;
            //위치에 중력값 추가
            segments[i].position += gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
            //위치에 속력 추가
            segments[i].position += segments[i].velocity;
        }
    }

    //세그먼트 위치 값지정
    private void ApplyConstraint()
    {
        //첫번째 위치 고정
        segments[0].position = startTransform.position;
        //영아
        if (End != null)
        {
            segments[1].position = End.transform.position;
        }
        else

            //

            for (int i = 0; i < segments.Count - 1; i++)
            {
                //두 점의 거리
                float distance = (segments[i].position - segments[i + 1].position).magnitude;
                //거리와 목표거리 차
                float difference = segmentLength - distance;
                //어느방향으로 이동시킬지 정할 방향
                Vector2 dir = (segments[i + 1].position - segments[i].position).normalized;

                //움직일 위치를 지정
                Vector2 movement = dir * difference;
                //두번째부터 움직이도록
                //첫번째 점은 위치를 빼줘야 함
                if (i == 0)
                    segments[i + 1].position += movement;
                else
                {

                    segments[i].position -= movement * 0.5f;
                    segments[i + 1].position += movement * 0.5f;

                }
            }
    }


    //세그먼트 컨트롤할 클래스
    public class Segment
    {
        //이전 위치 지장할 변수
        public Vector2 previousPos;
        //현재 위치를 지장할 변수
        public Vector2 position;
        //속도변스
        public Vector2 velocity;

        //리셋 함수(이전 위치, 현재 위치)
        public Segment(Vector2 _position)
        {
            previousPos = _position;
            position = _position;
            velocity = Vector2.zero;
        }
    }
}
