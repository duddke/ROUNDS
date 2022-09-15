using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYA_Rope : MonoBehaviour
{
    public GameObject End;

    public LineRenderer lineRenderer;
    //������ �̷�� ���׶���� ����(���׸�Ʈ)
    public int segmentCount=15;
    //���׸�Ʈ�� ����
    public float segmentLength=0.1f;
    //���� �ʺ�
    public float ropeWidth = 0.1f;
    //�߷�
    public Vector2 gravity = new Vector2(0f, -9.81f);
    //ó����ġ ��������()Ʈ������)
    [Space(10f)]
    public Transform startTransform;

    //���׸�Ʈ ����Ʈ
    public List<Segment> segments = new List<Segment>();

    //������ ��� ���η����� ��������
    private void Reset()
    {
        TryGetComponent(out lineRenderer);
    }

    //Awake���� ����Ʈ �߰�
    private void Awake()
    {
        //ó�� ��ġ���� ���� �Ʒ������� �������� ���׸�Ʈ ������ֱ�
        Vector2 segmentPos = startTransform.position;
        for(int i=0; i<segmentCount; i++)
        {
            segments.Add(new Segment(segmentPos));
            segmentPos.y -= segmentLength;
        }
    }

    //�������ֱ�
    private void FixedUpdate()
    {
        UpdateSegments();
        ApplyConstraint();
        DrawRope();
        
    }

    //������ ���η������� �׸��� �Լ�
    private void DrawRope()
    {
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        Vector3[] segmentPositions = new Vector3[segments.Count];
        for(int i=0; i< segments.Count; i++)
        {
            segmentPositions[i] = segments[i].position;
        }
        //���η������� ��ġ���� �־��ֱ�
        lineRenderer.positionCount = segmentPositions.Length;
        lineRenderer.SetPositions(segmentPositions);
    }

    //���׸�Ʈ�� �������� �Լ�
    private void UpdateSegments()
    {
        for(int i=0; i< segments.Count; i++)
        {
            //�ӵ�
            segments[i].velocity = segments[i].position - segments[i].previousPos;
            //���� ��ġ
            segments[i].previousPos = segments[i].position;
            //��ġ�� �߷°� �߰�
            segments[i].position += gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
            //��ġ�� �ӷ� �߰�
            segments[i].position += segments[i].velocity;
        }
    }

    //���׸�Ʈ ��ġ ������
    private void ApplyConstraint()
    {
        //ù��° ��ġ ����
        segments[0].position = startTransform.position;
        //����
        if (End != null)
        {
            segments[1].position = End.transform.position;
        }
        else

            //

            for (int i = 0; i < segments.Count - 1; i++)
            {
                //�� ���� �Ÿ�
                float distance = (segments[i].position - segments[i + 1].position).magnitude;
                //�Ÿ��� ��ǥ�Ÿ� ��
                float difference = segmentLength - distance;
                //����������� �̵���ų�� ���� ����
                Vector2 dir = (segments[i + 1].position - segments[i].position).normalized;

                //������ ��ġ�� ����
                Vector2 movement = dir * difference;
                //�ι�°���� �����̵���
                //ù��° ���� ��ġ�� ����� ��
                if (i == 0)
                    segments[i + 1].position += movement;
                else
                {

                    segments[i].position -= movement * 0.5f;
                    segments[i + 1].position += movement * 0.5f;

                }
            }
    }


    //���׸�Ʈ ��Ʈ���� Ŭ����
    public class Segment
    {
        //���� ��ġ ������ ����
        public Vector2 previousPos;
        //���� ��ġ�� ������ ����
        public Vector2 position;
        //�ӵ�����
        public Vector2 velocity;

        //���� �Լ�(���� ��ġ, ���� ��ġ)
        public Segment(Vector2 _position)
        {
            previousPos = _position;
            position = _position;
            velocity = Vector2.zero;
        }
    }
}
