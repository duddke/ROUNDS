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

    //A�� ���̰� �� Ƚ��
    public int AdieCount = 0;
    //B�� ���̰� �� Ƚ��
    public int BdieCount = 0;
    //A���� �¸� Ƚ��
    public int AroundWinCount = 0;
    //B���� �¸� Ƚ��
    public int BroundWinCount = 0;



    //���� �̸� ��� ����
    public string winner;

    //���� ��Ģ�� ���� ���� 
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
            //�ν��Ͻ��� ���� �ְ�
            Instance = this;
            //���� ���� ��ȯ�� �Ǿ �ı����� �ʰ� �ϰڴ�

            DontDestroyOnLoad(gameObject);
        }
        //�׷��� ������
        else
        {
            Destroy(gameObject);
            print("���ӸŴ��� ����");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    //���� �� �÷��̾� �� ���
    public List<GameObject> players = new List<GameObject>();
    void Update()
    {
        if (photonView.IsMine == false) return;
        switch(gameRule)
        {
            case GameRule.Matching:
                //���࿡ �ο��� �� ��������
                //currentTime += Time.deltaTime;
                //if(currentTime>=2)
                if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                {
                    currentTime = 0;
                    //���� ī�� ���� ������ ����
                    //SceneManager.LoadScene("SYA_RedSellectScene");
                    PhotonNetwork.LoadLevel("SYA_RedSellectScene");
                    //���º���
                    gameRule = GameRule.CardSellectRed;
                }
                    break;
            case GameRule.CardSellectRed:
                if (redOnClick)
                {
                    if (!first)
                    {
                        //���� ī�� ������ ���� ���
                        //��缿�� ������ ��ȯ�ϱ�
                        //SceneManager.LoadScene("SYA_BlueSellectScene");
                        PhotonNetwork.LoadLevel("SYA_BlueSellectScene");
                        redOnClick = false;
                        //���º���
                        gameRule = GameRule.CardSellectBlue;
                    }
                    else
                    {
                        //���Ӿ����� ��ȯ�ϱ�
                        //SceneManager.LoadScene("SyaScene");
                        PhotonNetwork.LoadLevel("SyaScene");
                        redOnClick = false;
                        //���º���
                        gameRule = GameRule.Ready;
                    }
                }
                break;
            case GameRule.CardSellectBlue:
                if (blueOnClick)
                {
                    //���Ӿ����� ��ȯ�ϱ�
                    //SceneManager.LoadScene("SyaScene");
                    PhotonNetwork.LoadLevel("SyaScene");
                    blueOnClick = false;
                    //���º���
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
    //������ư ������ ȣ��
    public void CardSellectRed()
    {
        //if (!GameObject.Find("Canvas").GetComponent<PhotonView>().IsMine) return;
        //������ ������ �ƴ� ��� ����
        redOnClick = true;
    }

    bool blueOnClick;
    //������ư ������ ȣ��
    public void CardSellectBlue()
    {
        //if (GameObject.Find("Canvas").GetComponent<PhotonView>().IsMine) return;
        //������ ������ ��� ����
        //��� ī�� ������ ���� ���
        blueOnClick = true;
    }

    //Ʈ���޽��� �� ������Ʈ �ľ��ϱ�
    public void Ready()
    {

        Time.timeScale = 1;

        //�ʻ����� �Ϸ�Ǹ�
        if (GameObject.Find("P1_Pos"))
        {
            //�÷��̾� ����Ʈ ����
            players.Clear();
            //���º���
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
            //�ʽð� õõ��
            Time.timeScale = 0.1f;
            //���º���
            gameRule = GameRule.DuelResult;
        }
    }

    public float currentTime = 0;
    public void DuelResult()
    {

        //1�� ��
        currentTime += Time.deltaTime;
        if (currentTime > 0.5f)
        {
            currentTime = 0;
            //���� �� �� Ƚ���� 2���
            if (AdieCount == 2 || BdieCount == 2)
            {
                //a�� ����Ƚ�� 2���
                if (AdieCount == 2)
                {
                    //b���� Ƚ��+1
                    BroundWinCount++;
                }
                //�ƴ϶��
                else
                {
                    //a���� Ƚ�� +1
                    AroundWinCount++;
                }
                //���帮��Ʈ���º�ȯ
                gameRule = GameRule.RoundResult;
            }
            //�ƴ϶��
            else
            {
                //�ٽ� �غ����(game)
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
        //1�� ��
        currentTime += Time.deltaTime;
        if (currentTime > 0.5f)
        {
            currentTime = 0;
            //������ ���� ���� 3�� �Ǹ�
            if (AroundWinCount == 3 || BroundWinCount == 3)
            {
                //����a��3�̶��
                if (AroundWinCount == 3)
                {
                    //���� a�÷��̾�==players[0] �г���
                    winner = /*"Red �¸�!!";//*/players[0].GetComponent<SY_PlayerMove>().nicName;
                }
                //�ƴ϶��
                else
                {
                    //���� b�÷��̾� �г���
                    winner = /*"Blue �¸�!!";//*/players[1].GetComponent<SY_PlayerMove>().nicName;
                }
                //��������� ��ȯ
                gameRule = GameRule.GameEnd;
                //SceneManager.LoadScene("SYA_ResultScene");
                PhotonNetwork.LoadLevel("SYA_ResultScene");
                turnJump = GameObject.Find("Turn Jump");
                turnJump.SetActive(false);
                //���ӻ��� ����� ��ȯ
            }
            //�ƴ϶��
            else
            {
                if (AdieCount == 2)
                {
                    //ī�弱������ �� ����
                    //SceneManager.LoadScene("SYA_RedSellectScene");
                    PhotonNetwork.LoadLevel("SYA_RedSellectScene");
                    first = true;
                    //���º���
                    gameRule = GameRule.CardSellectRed;
                }
                else if(BdieCount==2)
                {
                    //ī�弱������ �� ����
                    //SceneManager.LoadScene("SYA_BlueSellectScene");
                    PhotonNetwork.LoadLevel("SYA_BlueSellectScene");
                    //���º���
                    gameRule = GameRule.CardSellectBlue;
                }
                AdieCount = 0;
                BdieCount = 0;
            }
            //Ÿ�ӽ����� ���󺹱�
            Time.timeScale = 1;
        }
    }
    bool first;

    public Text winnerText;
    public GameObject turnJump;
    public void GameEnd()
    {
        Time.timeScale = 1;

        print("���� ����");
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
