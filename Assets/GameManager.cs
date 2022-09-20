using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public static GameManager Instance;

    //A�� ���̰� �� Ƚ��
    public int AdieCount = 0;
    //B�� ���̰� �� Ƚ��
    public int BdieCount = 0;
    //A���� �¸� Ƚ��
    public int AroundWinCount = 0;
    //B���� �¸� Ƚ��
    public int BroundWinCount = 0;

    //������� UI
    public Text duelResult;
    //������ UI
    public Text roundResult;

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

    private void Awake()
    {
        if (Instance == null)
        {
            //�ν��Ͻ��� ���� �ְ�
            Instance = this;
            //���� ���� ��ȯ�� �Ǿ �ı����� �ʰ� �ϰڴ�
            DontDestroyOnLoad(gameObject);
        }
        //�׷��� ������
        else
            //���� �ı��ϰڴ�
            Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    //���� �� �÷��̾� �� ���
    public List<PhotonView> players = new List<PhotonView>();
    void Update()
    {
        if (!photonView.IsMine) return;
        switch(gameRule)
        {
            case GameRule.Matching:
                //���࿡ �ο��� �� ��������
                if (players.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
                {
                    //���� ī�� ���� ������ ����
                    SceneManager.LoadScene("");
                    //���º���
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
        //���� ī�� ������ ���� ���
        //��缿�� ������ ��ȯ�ϱ�
        SceneManager.LoadScene(" ");
        //���º���
        gameRule = GameRule.CardSellectBlue;
    }

    public void CardSellectBlue()
    {
        //��� ī�� ������ ���� ���
        //���Ӿ����� ��ȯ�ϱ�
        SceneManager.LoadScene(" ");
        //���º���
        gameRule = GameRule.Ready;
    }

    public void Ready()
    {
        //�ʻ����� �Ϸ�Ǹ�
        //�÷��̾� ����
        if (photonView.IsMine)
            PhotonNetwork.Instantiate("Red_Player", GameObject.Find("P1_Pos").transform.position, Quaternion.identity);
        else
            photonView.RPC("CreatePlayer", RpcTarget.All, GameObject.Find("P2_Pos").transform.position);
        //���º���
        gameRule = GameRule.GameStart;
    }

    public void GameStart()
    {
        //�ѹ߻� ����
        //�̵� ����
        //���º���
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
            //�ʽð� õõ��
            Time.timeScale = 0.5f;
            //���º���
            gameRule = GameRule.DuelResult;
        }*/
    }

    float currentTime = 0;
    public void DuelResult()
    {
        //��� UI�����ֱ�
        duelResult.text = BdieCount + " : " + AdieCount;
        duelResult.enabled = true;
        //1�� ��
        currentTime += Time.deltaTime;
        if (currentTime > 1)
        {
            currentTime = 0;
            //UI�����
            duelResult.enabled = false;
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
                //�ٽ� �غ����
                gameRule = GameRule.RoundResult;
            }
        }
    }

    public void RoundResult()
    {
        //���� ��� �����ϱ�
        roundResult.text= AroundWinCount + " : " + BroundWinCount;
        roundResult.enabled = true;
        //1�� ��
        currentTime += Time.deltaTime;
        if (currentTime > 1)
        {
            currentTime = 0;
            roundResult.enabled = false;
            //������ ���� ���� 3�� �Ǹ�
            if (AroundWinCount == 3 || BroundWinCount == 3)
            {
                //����a��3�̶��
                if (AroundWinCount == 3)
                {
                    //���� a�÷��̾�==players[0] �г���
                    winner = players[0].Owner.NickName;
                }
                //�ƴ϶��
                else
                {
                    //���� b�÷��̾� �г���
                    winner = players[1].Owner.NickName;
                }
                //���ӻ��� ����� ��ȯ
                gameRule = GameRule.GameEnd;
            }
            //�ƴ϶��
            else
            {
                //ī�弱������ �� ����
                SceneManager.LoadScene(" ");
            }
            //Ÿ�ӽ����� ���󺹱�
            Time.timeScale = 1;
        }
    }

    public void GameEnd()
    {
        //��������� ��ȯ
        SceneManager.LoadScene(" ");
    }

    [PunRPC]
    void CreatePlayer(Vector3 map)
    {
        PhotonNetwork.Instantiate("Blue_Player", map, Quaternion.identity);
    }
}
