using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public static GameManager Instance;

    //A가 다이가 된 횟수
    public int AdieCount = 0;
    //B가 다이가 된 횟수
    public int BdieCount = 0;
    //A라운드 승리 횟수
    public int AroundWinCount = 0;
    //B라운드 승리 횟수
    public int BroundWinCount = 0;

    //결투결과 UI
    public Text duelResult;
    //라운드결과 UI
    public Text roundResult;

    //승자 이름 담는 변수
    public string winner;

    //게임 규칙에 따른 상태 
    public enum GameRule
    {
        Matching,
        CardSellectRed,
        CardSellectBlue,
        Ready,
        GameStart,
        Duel,
        DuelResult,
        RoundResult,
        GameEnd
    }
    public GameRule gameRule=GameRule.Matching ;

    private void Awake()
    {
        if (Instance == null)
        {
            //인스턴스에 나를 넣고
            Instance = this;
            //나를 씬이 전환이 되어도 파괴되지 않게 하겠다
            DontDestroyOnLoad(gameObject);
        }
        //그렇지 않으면
        else
            //나르 파괴하겠다
            Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    //현재 방 플레이어 다 담기
    public List<PhotonView> players = new List<PhotonView>();
    void Update()
    {
        if (!photonView.IsMine) return;
        switch(gameRule)
        {
            case GameRule.Matching:
                //만약에 인원이 다 들어왔으면
                if (players.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
                {
                    //레드 카드 선택 씬으로 변경
                    SceneManager.LoadScene("");
                    //상태변경
                    gameRule = GameRule.CardSellectRed;
                }
                    break;
            case GameRule.CardSellectRed:
                CardSellectRed();
                break;
            case GameRule.CardSellectBlue:
                CardSellectBlue();
                break;
            case GameRule.Ready:
                Ready();
                break;
            case GameRule.GameStart:
                GameStart();
                break;
            case GameRule.Duel:
                Duel();
                break;
            case GameRule.DuelResult:
                DuelResult();
                break;
            case GameRule.RoundResult:
                RoundResult();
                break;
            case GameRule.GameEnd:
                GameEnd();
                break;
        }
    }


    public void CardSellectRed()
    {
        //레드 카드 셀렉을 끝낼 경우
        //블루셀렉 씬으로 전환하기
        SceneManager.LoadScene(" ");
        //상태변경
        gameRule = GameRule.CardSellectBlue;
    }

    public void CardSellectBlue()
    {
        //블루 카드 셀렉을 끝낼 경우
        //게임씬으로 전환하기
        SceneManager.LoadScene(" ");
        //상태변경
        gameRule = GameRule.Ready;
    }

    public void Ready()
    {
        //맵생성이 완료되면
        //플레이어 생성
        if (photonView.IsMine)
            PhotonNetwork.Instantiate("Red_Player", GameObject.Find("P1_Pos").transform.position, Quaternion.identity);
        else
            photonView.RPC("CreatePlayer", RpcTarget.All, GameObject.Find("P2_Pos").transform.position);
        //상태변경
        gameRule = GameRule.GameStart;
    }

    public void GameStart()
    {
        //총발사 가능
        //이동 가능
        //상태변경
        gameRule = GameRule.Duel;
    }

    public void Duel()
    {
        /*if (player1.state == Die || player2.state == Die)
        {
            if (player1.state == Die)
                AdieCount++;
            else if (player2.state == Die)
                BdieCount++;
            //맵시간 천천히
            Time.timeScale = 0.5f;
            //상태변경
            gameRule = GameRule.DuelResult;
        }*/
    }

    float currentTime = 0;
    public void DuelResult()
    {
        //결과 UI보여주기
        duelResult.text = BdieCount + " : " + AdieCount;
        duelResult.enabled = true;
        //1초 뒤
        currentTime += Time.deltaTime;
        if (currentTime > 1)
        {
            currentTime = 0;
            //UI숨기기
            duelResult.enabled = false;
            //만약 둘 중 횟수가 2라면
            if (AdieCount == 2 || BdieCount == 2)
            {
                //a가 다이횟수 2라면
                if (AdieCount == 2)
                {
                    //b라운드 횟수+1
                    BroundWinCount++;
                }
                //아니라면
                else
                {
                    //a라운드 횟수 +1
                    AroundWinCount++;
                }
                //라운드리절트상태변환
                gameRule = GameRule.RoundResult;
            }
            //아니라면
            else
            {
                //다시 준비상태
                gameRule = GameRule.RoundResult;
            }
        }
    }

    public void RoundResult()
    {
        //라운드 결과 송출하기
        roundResult.text= AroundWinCount + " : " + BroundWinCount;
        roundResult.enabled = true;
        //1초 뒤
        currentTime += Time.deltaTime;
        if (currentTime > 1)
        {
            currentTime = 0;
            roundResult.enabled = false;
            //누군가 라운드 수가 3이 되면
            if (AroundWinCount == 3 || BroundWinCount == 3)
            {
                //만약a가3이라면
                if (AroundWinCount == 3)
                {
                    //위너 a플레이어==players[0] 닉네임
                    winner = players[0].Owner.NickName;
                }
                //아니라면
                else
                {
                    //위너 b플레이어 닉네임
                    winner = players[1].Owner.NickName;
                }
                //게임상태 엔드로 변환
                gameRule = GameRule.GameEnd;
            }
            //아니라면
            else
            {
                //카드선택으로 씬 변경
                SceneManager.LoadScene(" ");
            }
            //타임스케일 원상복구
            Time.timeScale = 1;
        }
    }

    public void GameEnd()
    {
        //결과씬으로 변환
        SceneManager.LoadScene(" ");
    }

    [PunRPC]
    void CreatePlayer(Vector3 map)
    {
        PhotonNetwork.Instantiate("Blue_Player", map, Quaternion.identity);
    }
}
