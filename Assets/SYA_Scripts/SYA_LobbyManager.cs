using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SYA_LobbyManager : MonoBehaviourPunCallbacks
{
    //방이 생성되면 넣을 리스트
    List<string> roomList = new List<string>();
    string roomName;

    //랜덤 입장 버튼 누르면
    public void OnClickRandomRoom()
    {
        CreateRoom();
    }

    //방 생성
    public void CreateRoom()
    {
        // 방 옵션을 설정
        RoomOptions roomOptions = new RoomOptions();
        // 최대 인원 (0이면 최대인원)
        roomOptions.MaxPlayers = 2;
        // 룸 리스트에 보이지 않게? 보이게?
        roomOptions.IsVisible = true;
        roomName = "room" + Random.Range(0, 10000);
        roomList.Add(roomName);
        // 방 생성 요청 (해당 옵션을 이용해서)
        //PhotonNetwork.CreateRoom(roomName, roomOptions);
        PhotonNetwork.JoinRandomOrCreateRoom(null,2,MatchmakingMode.FillRoom,null,null,roomName,roomOptions,null);
    }

    //방이 생성되면 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //방 생성이 실패 될때 호출 되는 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
    }

    //방 참가 요청
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    //방 참가가 완료 되었을 때 호출 되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("SYA_MatchingScene");
    }

    //방 참가가 실패 되었을 때 호출 되는 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
