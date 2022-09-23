using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SYA_BlockMoving : MonoBehaviourPun
{
    // Start is called before the first frame update

    public enum BlockMoveState
    {
    //������ �����ϸ� 1�������� �̵��Ѵ�.
        ToOneFromStart,
    //1������ �����ϸ� 2�������� �̵��Ѵ�.
        ToTwoFromOne,
    //2������ �����ϸ� 1�������� �̵��Ѵ�.
        ToOneFromTwo,
    //1������ �����ϸ� 3�������� �̵��Ѵ�.
        ToThreeFromOne,
    //3������ �����ϸ� 4�������� �̵��Ѵ�.
        ToFourFromThree,
    //4������ �����ϸ� 3�������� �̵��Ѵ�.
        ToThreeFromFour,
    //3������ �����ϸ� 1�������� �̵��Ѵ�.
        ToOneFromThree
    }

    public BlockMoveState moveState = BlockMoveState.ToOneFromStart;

    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3;
    public GameObject Pos4;

    public float standard = 0.5f;

    Transform ToPos;
    public float speed = 5;
    public float time=0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false) return;
        switch(moveState)
        {
            case BlockMoveState.ToOneFromStart:
                OnChangePos(Pos1.transform, BlockMoveState.ToTwoFromOne);
                break;
            case BlockMoveState.ToTwoFromOne:
                OnChangePos(Pos2.transform, BlockMoveState.ToOneFromTwo);
                break;
            case BlockMoveState.ToOneFromTwo:
                OnChangePos(Pos1.transform, BlockMoveState.ToThreeFromOne);
                break;
            case BlockMoveState.ToThreeFromOne:
                OnChangePos(Pos3.transform, BlockMoveState.ToFourFromThree);
                break;
            case BlockMoveState.ToFourFromThree:
                OnChangePos(Pos4.transform, BlockMoveState.ToThreeFromFour);
                break;
            case BlockMoveState.ToThreeFromFour:
                OnChangePos(Pos3.transform, BlockMoveState.ToOneFromThree);
                break;
            case BlockMoveState.ToOneFromThree:
                OnChangePos(Pos1.transform, BlockMoveState.ToTwoFromOne);
                break;
        }
        transform.position = Vector3.Lerp(transform.position, ToPos.position, time);
    }

    public void OnChangePos(Transform pos, BlockMoveState blockMove)
    {
        ToPos = pos.transform;
        float dis = Vector3.Distance(transform.position, ToPos.position);
        time += speed * Time.deltaTime;
        if (dis <= standard)
        {
            moveState = blockMove;
            time = 0;
        }
    }
}
