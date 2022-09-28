using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPun, IPunObservable
{
    public static GameManager Instance;

    public GameObject RedPlayer;

    public List<GameObject> Map;

    //A가 다이가 된 횟수
    public int AdieCount = 0;
    //B가 다이가 된 횟수
    public int BdieCount = 0;
    //A라운드 승리 횟수
    public int AroundWinCount = 0;
    //B라운드 승리 횟수
    public int BroundWinCount = 0;



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

    private void OnDestroy()
    {
        print(11);
    }

    private void Awake()
    {
        //if (!photonView.IsMine) return;
        if (Instance == null)
        {
            //인스턴스에 나를 넣고
            Instance = this;
            //나를 씬이 전환이 되어도 파괴되지 않게 하겠다

            DontDestroyOnLoad(gameObject);
        }
        //그렇지 않으면
        else
        {
            Destroy(gameObject);
            print("게임매니저 삭제");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    //현재 방 플레이어 다 담기
    public List<GameObject> players = new List<GameObject>();
    void Update()
    {
        if (photonView.IsMine == false) return;
        switch(gameRule)
        {
            case GameRule.Matching:
                //만약에 인원이 다 들어왔으면
                //currentTime += Time.deltaTime;
                //if(currentTime>=2)
                if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                {
                    currentTime = 0;
                    //레드 카드 선택 씬으로 변경
                    //SceneManager.LoadScene("SYA_RedSellectScene");
                    PhotonNetwork.LoadLevel("SYA_RedSellectScene");
                    //상태변경
                    gameRule = GameRule.CardSellectRed;
                }
                    break;
            case GameRule.CardSellectRed:
                if (redOnClick)
                {
                    if (!first)
                    {
                        //레드 카드 셀렉을 끝낼 경우
                        //블루셀렉 씬으로 전환하기
                        //SceneManager.LoadScene("SYA_BlueSellectScene");
                        PhotonNetwork.LoadLevel("SYA_BlueSellectScene");
                        redOnClick = false;
                        //상태변경
                        gameRule = GameRule.CardSellectBlue;
                    }
                    else
                    {
                        //게임씬으로 전환하기
                        //SceneManager.LoadScene("SyaScene");
                        PhotonNetwork.LoadLevel("SyaScene");
                        redOnClick = false;
                        //상태변경
                        gameRule = GameRule.Ready;
                    }
                }
                break;
            case GameRule.CardSellectBlue:
                if (blueOnClick)
                {
                    //게임씬으로 전환하기
                    //SceneManager.LoadScene("SyaScene");
                    PhotonNetwork.LoadLevel("SyaScene");
                    blueOnClick = false;
                    //상태변경
                    gameRule = GameRule.Ready;
                }
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

    bool redOnClick;
    //결정버튼 누르면 호출
    public void CardSellectRed()
    {
        //if (!GameObject.Find("Canvas").GetComponent<PhotonView>().IsMine) return;
        //방장이 주인이 아닐 경우 리턴
        redOnClick = true;
    }

    bool blueOnClick;
    //결정버튼 누르면 호출
    public void CardSellectBlue()
    {
        //if (GameObject.Find("Canvas").GetComponent<PhotonView>().IsMine) return;
        //방장이 주인일 경우 리턴
        //블루 카드 셀렉을 끝낼 경우
        blueOnClick = true;
    }

    //트루펄스로 전 스테이트 파악하기
    public void Ready()
    {

        Time.timeScale = 1;

        //맵생성이 완료되면
        if (GameObject.Find("P1_Pos"))
        {
            //플레이어 리스트 리셋
            players.Clear();
            //상태변경
            gameRule = GameRule.GameStart;
        }

    }

    public void GameStart()
    {
        if(players.Count>=2)
        gameRule = GameRule.Duel;
    }

    public void Duel()
    {
        if (players[0].GetComponent<SY_PlayerMove>().state == SY_PlayerMove.State.Die || players[1].GetComponent<SY_PlayerMove>().state == SY_PlayerMove.State.Die)
        {
            if (players[0].GetComponent<SY_PlayerMove>().state == SY_PlayerMove.State.Die)
                AdieCount++;
            else if (players[1].GetComponent<SY_PlayerMove>().state == SY_PlayerMove.State.Die)
                BdieCount++;
            //맵시간 천천히
            Time.timeScale = 0.1f;
            //상태변경
            gameRule = GameRule.DuelResult;
        }
    }

    public float currentTime = 0;
    public void DuelResult()
    {

        //1초 뒤
        currentTime += Time.deltaTime;
        if (currentTime > 0.5f)
        {
            currentTime = 0;
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
                //다시 준비상태(game)
                gameRule = GameRule.Ready;

                // PhotonNetwork.AutomaticallySyncScene = true;
                StartCoroutine(aaa());
            }
        }
    }

    IEnumerator aaa()
    {
        PhotonNetwork.LoadLevel("SYA_MapLoadScene");
        yield return new WaitForSeconds(2);
        PhotonNetwork.LoadLevel("SyaScene");
    }

    public void RoundResult()
    {
        //1초 뒤
        currentTime += Time.deltaTime;
        if (currentTime > 0.5f)
        {
            currentTime = 0;
            //누군가 라운드 수가 3이 되면
            if (AroundWinCount == 3 || BroundWinCount == 3)
            {
                //만약a가3이라면
                if (AroundWinCount == 3)
                {
                    //위너 a플레이어==players[0] 닉네임
                    winner = /*"Red 승리!!";//*/players[0].GetComponent<SY_PlayerMove>().nicName;
                }
                //아니라면
                else
                {
                    //위너 b플레이어 닉네임
                    winner = /*"Blue 승리!!";//*/players[1].GetComponent<SY_PlayerMove>().nicName;
                }
                //결과씬으로 변환
                gameRule = GameRule.GameEnd;
                //SceneManager.LoadScene("SYA_ResultScene");
                PhotonNetwork.LoadLevel("SYA_ResultScene");
                turnJump = GameObject.Find("Turn Jump");
                turnJump.SetActive(false);
                //게임상태 엔드로 변환
            }
            //아니라면
            else
            {
                if (AdieCount == 2)
                {
                    //카드선택으로 씬 변경
                    //SceneManager.LoadScene("SYA_RedSellectScene");
                    PhotonNetwork.LoadLevel("SYA_RedSellectScene");
                    first = true;
                    //상태변경
                    gameRule = GameRule.CardSellectRed;
                }
                else if(BdieCount==2)
                {
                    //카드선택으로 씬 변경
                    //SceneManager.LoadScene("SYA_BlueSellectScene");
                    PhotonNetwork.LoadLevel("SYA_BlueSellectScene");
                    //상태변경
                    gameRule = GameRule.CardSellectBlue;
                }
                AdieCount = 0;
                BdieCount = 0;
            }
            //타임스케일 원상복구
            Time.timeScale = 1;
        }
    }
    bool first;

    public Text winnerText;
    public GameObject turnJump;
    public void GameEnd()
    {
        Time.timeScale = 1;

        print("게임 끝남");
        //press to jump
        currentTime += Time.deltaTime;
        if(currentTime>=2)
        {
            turnJump.SetActive(true);
        }
    }

    [PunRPC]
    void CreatePlayer(Vector3 map)
    {
        PhotonNetwork.Instantiate("Blue_Player", map, Quaternion.identity);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(gameRule);
        }

        else
        {
            gameRule = (GameRule)stream.ReceiveNext();
        }
    }
}
