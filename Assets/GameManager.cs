using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public static GameManager Instance;

    public GameObject RedPlayer;

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
    public List<PhotonView> players = new List<PhotonView>();
    void Update()
    {
        switch(gameRule)
        {
            case GameRule.Matching:
                //만약에 인원이 다 들어왔으면
                currentTime += Time.deltaTime;
                if(currentTime>=2)
                //if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                {
                    currentTime = 0;
                    //레드 카드 선택 씬으로 변경
                    SceneManager.LoadScene("SYA_RedSellectScene");
                    //PhotonNetwork.LoadLevel("SYA_RedSellectScene");
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
                        SceneManager.LoadScene("SYA_BlueSellectScene");
                        redOnClick = false;
                        //상태변경
                        gameRule = GameRule.CardSellectBlue;
                    }
                    else
                    {
                        //게임씬으로 전환하기
                        SceneManager.LoadScene("SyaScene");
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
                    SceneManager.LoadScene("SyaScene");
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
        //if (!photonView.IsMine) return;
        redOnClick = true;
    }

    bool blueOnClick;
    //결정버튼 누르면 호출
    public void CardSellectBlue()
    {
        //if (photonView.IsMine) return;
        //블루 카드 셀렉을 끝낼 경우
        blueOnClick = true;
    }

    //트루펄스로 전 스테이트 파악하기
    public void Ready()
    {
        Time.timeScale = 1;
        duelResult = GameObject.Find("duelResult").GetComponent<Text>();
        duelResult.enabled = false;
        roundResult = GameObject.Find("roundResult").GetComponent<Text>();
        roundResult.enabled = false;
        //맵생성이 완료되면
        if (GameObject.Find("P1_Pos")&&duelResult&&roundResult)
        {

            /*        if (photonView.IsMine)
                        PhotonNetwork.Instantiate("Red_Player", GameObject.Find("P1_Pos").transform.position, Quaternion.identity);
                    else
                        photonView.RPC("CreatePlayer", RpcTarget.All, GameObject.Find("P2_Pos").transform.position);*/
            //상태변경
            gameRule = GameRule.GameStart;

        }
    }

    public void GameStart()
    {
        //플레이어 생성
        GameObject player = Instantiate(RedPlayer);
        player.transform.position = GameObject.Find("P1_Pos").transform.position;
        /*if (photonView.IsMine)
            PhotonNetwork.Instantiate("Red_Player", GameObject.Find("P1_Pos").transform.position, Quaternion.identity);
        else
            photonView.RPC("CreatePlayer", RpcTarget.All, GameObject.Find("P2_Pos").transform.position);*/

        //총발사 가능
        //이동 가능
        //상태변경
        gameRule = GameRule.Duel;
    }

    public void Duel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BdieCount++;
            //맵시간 천천히
            Time.timeScale = 0.1f;
            //상태변경
            gameRule = GameRule.DuelResult;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AdieCount++;
            //맵시간 천천히
            Time.timeScale = 0.1f;
            //상태변경
            gameRule = GameRule.DuelResult;
        }
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

    public float currentTime = 0;
    public void DuelResult()
    {
        //결과 UI보여주기
        duelResult.text ="죽인 횟수 : A "+ BdieCount + " : B " + AdieCount;
        duelResult.enabled = true;
        //1초 뒤
        currentTime += Time.deltaTime;
        if (currentTime > 0.5f)
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
                //다시 준비상태(game)
                gameRule = GameRule.CardSellectBlue;
                CardSellectBlue();
            }
        }
    }

    public void RoundResult()
    {
        //라운드 결과 송출하기
        roundResult.text= "이긴 횟수 : A "+AroundWinCount + " : B " + BroundWinCount;
        roundResult.enabled = true;
        //1초 뒤
        currentTime += Time.deltaTime;
        if (currentTime > 0.5f)
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
                    winner = "Red 승리!!";//players[0].Owner.NickName;
                }
                //아니라면
                else
                {
                    //위너 b플레이어 닉네임
                    winner = "Blue 승리!!";//players[1].Owner.NickName;
                }
                //결과씬으로 변환
                gameRule = GameRule.GameEnd;
                SceneManager.LoadScene("SYA_ResultScene");
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
                    SceneManager.LoadScene("SYA_RedSellectScene");
                    //PhotonNetwork.LoadLevel("SYA_MatchingScene");
                    first = true;
                    //상태변경
                    gameRule = GameRule.CardSellectRed;
                }
                else if(BdieCount==2)
                {
                    //카드선택으로 씬 변경
                    SceneManager.LoadScene("SYA_BlueSellectScene");
                    //PhotonNetwork.LoadLevel("SYA_MatchingScene");
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
        winnerText = GameObject.Find("Winner").GetComponent<Text>();
        winnerText.text = winner + " WIN!!";
        print("게임 끝남");
        //press to jump
        currentTime += Time.deltaTime;
        if(currentTime>=2)
        {
            turnJump.SetActive(true);
        }
    }

}
