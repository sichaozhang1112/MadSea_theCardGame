using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum TurnStage
{
    start,
    prepare,
    play,
    discard,
    draw,
    wrath,
    curse,
    end
}

public enum SPMode
{
    none,
    exile,
    discard
}

public enum PileType
{
    discard,
    hand,
    deck,
    voidPile
}

public class MainManager : MonoBehaviour
{
    static public MainManager instance;

    [SerializeField] Text uiGold;
    [SerializeField] Text uiAttack;
    [SerializeField] Text uiLife;
    [SerializeField] Text uiDeckNum;
    [SerializeField] Text uiScore;
    [SerializeField] Text uiBattleDeckNum;
    [SerializeField] Text uiVoidNum;
    [SerializeField] Text uiDiscardNum;
    [SerializeField] Image uiStageTimer;
    [SerializeField] GameObject uiExileField;
    [SerializeField] Text uiExileField_num;
    [SerializeField] GameObject uiExileHand;
    [SerializeField] Text uiExileHand_num;
    [SerializeField] GameObject uiDiscardHand;
    [SerializeField] Text uiDiscardHand_num;

    [SerializeField] Deck deck;
    [SerializeField] Hand hand;
    [SerializeField] DiscardPile disacrdPile;
    [SerializeField] PlayedCards playedCards;
    [SerializeField] VoidPile voidPile;
    [SerializeField] BattleField battleField;
    [SerializeField] BattleDeck battleDeck;
    [SerializeField] GameObject cardsInstance;
    [SerializeField] GameObject FangAnim;
    [SerializeField] GameObject btnVictory;


    [SerializeField] int gold;
    [SerializeField] int attack;
    [SerializeField] int life;
    [SerializeField] int score;


    [SerializeField] int drawEveryTurn = 5;

    TurnStage ts = TurnStage.play;
    SPMode sp = SPMode.none;
    float nextStageTimer = 0f;
    [SerializeField] float stageWaitTime = 1.0f;

    int exileField = 0;
    int exileHand = 0;
    //int exileDiscard = 0;
    int discardHand = 0;

    public GameObject GetCardsInstance() { return cardsInstance; }
    public BattleField GetBattleField() { return battleField;}
    public BattleDeck GetBattleDeck() { return battleDeck; }

    public Deck GetDeck() { return deck; }
    public Hand GetHand() { return hand; }
    public DiscardPile GetDiscardPile() { return disacrdPile; }
    public PlayedCards GetPlayedCards() { return playedCards; }
    public VoidPile GetVoidPile() { return voidPile; }

    public int GetGold() { return gold; }
    public int GetAttack() { return attack; }
    public int GetResource(CostType type)
    {
        switch (type)
        {
            case CostType.gold:
                return GetGold();
            case CostType.attack:
                return GetAttack();
            default:
                return 0;
        }
    }

    public void AddGold(int value) { gold = gold + value; }
    public void AddAttack(int value) { attack = attack + value; }

    public void LifeChange(int value)
    {
        life = life + value;
        if (life < 0)
        {
            Debug.Log("YOU DIED!");
            SceneManager.LoadScene("end_dead");
        }
    }

    public void ScoreChange(int value)
    {
        score = score + value;
        if (score < 0)
        {
            score = 0;
        }
    }

    public void StartExile(int numBattleField, int numHand)
    {
        sp = SPMode.exile;
        exileField = numBattleField;
        exileHand = numHand;
    }

    public void StartDiscard(int numHand)
    {
        sp = SPMode.discard;
        discardHand = numHand;
    }

    public void OnCardClicked(string pileTag, int index)
    {
        switch (ts)
        {
            case TurnStage.start:
                break;
            case TurnStage.prepare:
                break;
            case TurnStage.play:
                if(sp == SPMode.exile)
                {
                    if (pileTag == "BattleField" && exileField>0)
                    {
                        Card clickedCard = battleField.GetCard(index);
                        bool avaliable = clickedCard.canExile();
                        if (avaliable)
                        {
                            clickedCard.OnExile();
                            battleField.DrawCard(index);
                            exileField--;
                        }
                    }
                    else if (pileTag == "Hand" && exileHand>0)
                    {
                        Card clickedCard = hand.GetCard(index);
                        bool avaliable = clickedCard.canExile();
                        if (avaliable)
                        {
                            hand.ExileCard(index);
                            exileHand--;
                        }
                    }
                    if(exileHand<=0 && exileField <= 0)
                    {
                        sp = SPMode.none;
                    }
                }
                else if(sp == SPMode.discard)
                {
                    if (pileTag == "Hand" && discardHand > 0)
                    {
                        Card clickedCard = battleField.GetCard(index);
                        hand.DiscardCard(index);
                        discardHand--;
                    }
                    if (discardHand <= 0)
                    {
                        sp = SPMode.none;
                    }
                }
                else
                {
                    if (pileTag == "BattleField")
                    {
                        Card clickedCard = battleField.GetCard(index);
                        bool avaliable = clickedCard.RequirementsCheck();
                        if (avaliable)
                        {
                            clickedCard.OnGetNormally();
                            battleField.DrawCard(index);
                            switch (clickedCard.getCostType())
                            {
                                case CostType.gold:
                                    gold -= clickedCard.getCost();
                                    break;
                                case CostType.attack:
                                    attack -= clickedCard.getCost();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (pileTag == "Hand")
                    {
                        hand.PlayCard(index);
                    }
                }
                break;
            case TurnStage.curse:
                break;
            case TurnStage.wrath:
                break;
            case TurnStage.discard:
                break;
            case TurnStage.draw:
                break;
            case TurnStage.end:
                break;
            default:
                break;
        }
    }

    public void TurnEnd()
    {
        if (ts == TurnStage.play && sp == SPMode.none) SwitchStage();
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(instance is null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("okay? second running will be a problem");
            instance = this;
        }
    }

    public void OnBeatSeaLord()
    {
        btnVictory.SetActive(true);
    }

    void SwitchStage()
    {
        nextStageTimer = stageWaitTime;
        if (ts == TurnStage.end)
        {
            ts = TurnStage.start;
        }
        else
        {
            ts = ts + 1;
        }

        switch (ts)
        {
            case TurnStage.start:
                break;
            case TurnStage.prepare:
                break;
            case TurnStage.play:
                break;
            case TurnStage.curse:
                for (int i = 0; i < battleField.GetCards().Count; i++)
                {
                    Card item = battleField.GetCard(i);
                    bool ret = item.OnCurse();
                    if(PlayerPrefs.GetInt("Difficult") >= 3)
                    {
                        bool ret2 = item.OnCurse();
                        ret = (ret || ret2);
                    }
                    if (ret)
                    {
                        GameObject go = item.GetCursedCard();
                        if (go is null)
                        {
                            Debug.LogError("Invalid null cursed card");
                        }
                        else
                        {
                            GameObject go_tmp = GameObject.Instantiate(go, cardsInstance.transform);
                            go_tmp.GetComponent<Card>().SetLastCursed();
                            battleField.SetCard(i, go_tmp.GetComponent<Card>());
                        }
                    }
                }
                break;
            case TurnStage.wrath:
                int index = 0;
                foreach (var item in battleField.GetCards())
                {
                    bool ret = item.OnWrath();
                    //if (ret)
                    //{
                    //    Vector3 pos = battleField.GetCardPos(index);
                    //    FangAnim.transform.position = pos;
                    //    GameObject go = GameObject.Instantiate(FangAnim);
                    //    Destroy(go, 35.0f/60.0f);
                    //}
                    index++;
                }
                break;
            case TurnStage.discard:
                hand.DiscardAll();
                break;
            case TurnStage.draw:
                playedCards.DiscardAll();
                hand.DrawCards(drawEveryTurn);
                break;
            case TurnStage.end:
                gold = 0;
                attack = 0;
                if(battleDeck.GetCount() == 0)
                {
                    bool winFlag = true;
                    foreach (var item in battleField.GetCards())
                    {
                        if(item.getCostType() == CostType.attack)
                        {
                            winFlag = false;
                        }
                    }
                    if (winFlag)
                    {
                        PlayerPrefs.SetInt("score", score);
                        SceneManager.LoadScene("end_victory");
                    }
                }
                break;
            default:
                break;
        }
        RefreshUI_All();
    }

    private void Start()
    {
        this.battleDeck.Shuffle();
        this.deck.Shuffle();
        this.hand.DrawCards(5);
        RefreshUI_All();
        PlayerPrefs.SetInt("score", 0);
        life = PlayerPrefs.GetInt("Life");
    }

    void RefreshUI_All()
    {
        hand.RefreshUI();
        playedCards.RefreshUI();
        battleField.RefreshUI();
        uiGold.text = gold.ToString();
        uiAttack.text = attack.ToString();
        uiLife.text = life.ToString();
        uiScore.text = score.ToString();
        uiDeckNum.text = deck.GetCount().ToString();
        uiBattleDeckNum.text = battleDeck.GetCount().ToString();
        uiVoidNum.text = voidPile.GetCount().ToString();
        uiDiscardNum.text = disacrdPile.GetCount().ToString();
        if (exileField > 0)
        {
            uiExileField.SetActive(true);
            uiExileField_num.text = exileField.ToString();
        }
        else
        {
            uiExileField.SetActive(false);
        }
        if(exileHand > 0)
        {
            uiExileHand.SetActive(true);
            uiExileHand_num.text = exileHand.ToString();
        }
        else
        {
            uiExileHand.SetActive(false);
        }
        if (discardHand > 0)
        {
            uiDiscardHand.SetActive(true);
            uiDiscardHand_num.text = discardHand.ToString();
        }
        else
        {
            uiDiscardHand.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RefreshUI_All();
        }
        switch (ts)
        {
            case TurnStage.start:
                nextStageTimer -= 5 * Time.deltaTime;
                break;
            case TurnStage.prepare:
                nextStageTimer -= 2 * Time.deltaTime;
                break;
            case TurnStage.play:
                break;
            case TurnStage.curse:
                break;
            case TurnStage.wrath:
                break;
            case TurnStage.discard:
                nextStageTimer -= 5 * Time.deltaTime;
                break;
            case TurnStage.draw:
                nextStageTimer -= 5 * Time.deltaTime;
                break;
            case TurnStage.end:
                nextStageTimer -= 5 * Time.deltaTime;
                break;
            default:
                break;
        }
        if(ts != TurnStage.play)
        {
            if (nextStageTimer > 0) nextStageTimer -= Time.deltaTime;
            else SwitchStage();
            uiStageTimer.fillAmount = (nextStageTimer / stageWaitTime);
        }
        if(sp == SPMode.exile)
        {
            if (exileHand > 0)
            {
                if (hand.GetCount() <= 0)
                {
                    exileHand = 0;
                    sp = SPMode.none;
                }
            }
        }
        if(sp == SPMode.discard)
        {
            if(discardHand>0 && hand.GetCount() <= 0)
            {
                discardHand = 0;
                sp = SPMode.none;
            }
        }
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RefreshUI_All();
        }
    }
}
