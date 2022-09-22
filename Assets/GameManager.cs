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
    public List<PhotonView> players = new List<PhotonView>();
    void Update()
    {
        switch(gameRule)
        {
            case GameRule.Matching:
                //���࿡ �ο��� �� ��������
                currentTime += Time.deltaTime;
                if(currentTime>=2)
                //if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                {
                    currentTime = 0;
                    //���� ī�� ���� ������ ����
                    SceneManager.LoadScene("SYA_RedSellectScene");
                    //PhotonNetwork.LoadLevel("SYA_RedSellectScene");
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
                        SceneManager.LoadScene("SYA_BlueSellectScene");
                        redOnClick = false;
                        //���º���
                        gameRule = GameRule.CardSellectBlue;
                    }
                    else
                    {
                        //���Ӿ����� ��ȯ�ϱ�
                        SceneManager.LoadScene("SyaScene");
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
                    SceneManager.LoadScene("SyaScene");
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
        //if (!photonView.IsMine) return;
        redOnClick = true;
    }

    bool blueOnClick;
    //������ư ������ ȣ��
    public void CardSellectBlue()
    {
        //if (photonView.IsMine) return;
        //��� ī�� ������ ���� ���
        blueOnClick = true;
    }

    //Ʈ���޽��� �� ������Ʈ �ľ��ϱ�
    public void Ready()
    {
        Time.timeScale = 1;
        duelResult = GameObject.Find("duelResult").GetComponent<Text>();
        duelResult.enabled = false;
        roundResult = GameObject.Find("roundResult").GetComponent<Text>();
        roundResult.enabled = false;
        //�ʻ����� �Ϸ�Ǹ�
        if (GameObject.Find("P1_Pos")&&duelResult&&roundResult)
        {

            /*        if (photonView.IsMine)
                        PhotonNetwork.Instantiate("Red_Player", GameObject.Find("P1_Pos").transform.position, Quaternion.identity);
                    else
                        photonView.RPC("CreatePlayer", RpcTarget.All, GameObject.Find("P2_Pos").transform.position);*/
            //���º���
            gameRule = GameRule.GameStart;

        }
    }

    public void GameStart()
    {
        //�÷��̾� ����
        GameObject player = Instantiate(RedPlayer);
        player.transform.position = GameObject.Find("P1_Pos").transform.position;
        /*if (photonView.IsMine)
            PhotonNetwork.Instantiate("Red_Player", GameObject.Find("P1_Pos").transform.position, Quaternion.identity);
        else
            photonView.RPC("CreatePlayer", RpcTarget.All, GameObject.Find("P2_Pos").transform.position);*/

        //�ѹ߻� ����
        //�̵� ����
        //���º���
        gameRule = GameRule.Duel;
    }

    public void Duel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BdieCount++;
            //�ʽð� õõ��
            Time.timeScale = 0.1f;
            //���º���
            gameRule = GameRule.DuelResult;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AdieCount++;
            //�ʽð� õõ��
            Time.timeScale = 0.1f;
            //���º���
            gameRule = GameRule.DuelResult;
        }
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

    public float currentTime = 0;
    public void DuelResult()
    {
        //��� UI�����ֱ�
        duelResult.text ="���� Ƚ�� : A "+ BdieCount + " : B " + AdieCount;
        duelResult.enabled = true;
        //1�� ��
        currentTime += Time.deltaTime;
        if (currentTime > 0.5f)
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
                //�ٽ� �غ����(game)
                gameRule = GameRule.CardSellectBlue;
                CardSellectBlue();
            }
        }
    }

    public void RoundResult()
    {
        //���� ��� �����ϱ�
        roundResult.text= "�̱� Ƚ�� : A "+AroundWinCount + " : B " + BroundWinCount;
        roundResult.enabled = true;
        //1�� ��
        currentTime += Time.deltaTime;
        if (currentTime > 0.5f)
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
                    winner = "Red �¸�!!";//players[0].Owner.NickName;
                }
                //�ƴ϶��
                else
                {
                    //���� b�÷��̾� �г���
                    winner = "Blue �¸�!!";//players[1].Owner.NickName;
                }
                //��������� ��ȯ
                gameRule = GameRule.GameEnd;
                SceneManager.LoadScene("SYA_ResultScene");
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
                    SceneManager.LoadScene("SYA_RedSellectScene");
                    //PhotonNetwork.LoadLevel("SYA_MatchingScene");
                    first = true;
                    //���º���
                    gameRule = GameRule.CardSellectRed;
                }
                else if(BdieCount==2)
                {
                    //ī�弱������ �� ����
                    SceneManager.LoadScene("SYA_BlueSellectScene");
                    //PhotonNetwork.LoadLevel("SYA_MatchingScene");
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
        winnerText = GameObject.Find("Winner").GetComponent<Text>();
        winnerText.text = winner + " WIN!!";
        print("���� ����");
        //press to jump
        currentTime += Time.deltaTime;
        if(currentTime>=2)
        {
            turnJump.SetActive(true);
        }
    }

}
