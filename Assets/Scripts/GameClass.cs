using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GameClass : MonoBehaviour
{
    // Initialize game over screen
    public GameOverScreen GameOverScreen;

    // Initialize lists
    public List<Button> cards = new List<Button>();
    public List<Sprite> cardFaces = new List<Sprite>();

    // Initialize required variables

    [SerializeField]
    private Sprite cardBack;
    public Sprite[] faces;
    private bool click1;
    private bool click2;
    private int clickCount;
    private int correctClickCount;
    private int allowedClicks;
    private string click1Name;
    private string click2Name;
    private int click1Index;
    private int click2Index;

    // Start is called before the first frame update
    void Start()
    {
        // Get a list of game objects cards
        GameObject[] cardObjects = GameObject.FindGameObjectsWithTag("CardFlip");
        for (int i = 0; i < cardObjects.Length; i++)
        {
            cards.Add(cardObjects[i].GetComponent<Button>());
            cards[i].image.sprite = cardBack;
        }
        // Initialize listeners to buttons
        foreach (Button cardBtn in cards)
        {
            cardBtn.onClick.AddListener(() => CardOnClick());
        }
        LoadCards();
        RandomizeCards(cardFaces);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // load cards to the scene
    void LoadCards()
    {
        int count = 16;
        int a = 0;
        for (int i = 0; i < count; i++)
        {
            if (a == count / 2)
            {
                a = 0;
            }
            cardFaces.Add(faces[a]);
            a++;
        }
    }

    // Listener for each card
    // Compare if card clicked first
    // is the same as second
    public void CardOnClick()
    {
        string clickedCard = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        string[] toParse = clickedCard.Split("card");

        if (!click1)
        {
            click1 = true;
            click1Index = int.Parse(toParse[1]);
            click1Name = cardFaces[click1Index].name;
            cards[click1Index].image.sprite = cardFaces[click1Index];
        }
        else if (!click2)
        {
            click2 = true;
            click2Index = int.Parse(toParse[1]);
            click2Name = cardFaces[click2Index].name;
            cards[click2Index].image.sprite = cardFaces[click2Index];
            StartCoroutine(CheckCardsMatch());
        }
    }

    // Check if two cards match after second click
    // If match set them to interactable
    // Check if the rest matches to end game
    // otherwise flip cards back
    IEnumerator CheckCardsMatch()
    {
        yield return new WaitForSeconds(1f);
        if (click1Name == click2Name)
        {
            yield return new WaitForSeconds(1.5f);
            cards[click1Index].interactable = false;
            cards[click2Index].interactable = false;
            gameOver();
        }
        else
        {
            cards[click1Index].image.sprite = cardBack;
            cards[click2Index].image.sprite = cardBack;
        }
        yield return new WaitForSeconds(0.5f);
        click1 = false;
        click2 = false;
    }

    // Shuffle cards to get
    // a different set of cards each game
    void RandomizeCards(List<Sprite> spritesList)
    {
        for (int i = 0; i < spritesList.Count; i++)
        {
            Sprite s = spritesList[i];
            int rIndex = Random.Range(i, spritesList.Count);
            spritesList[i] = spritesList[rIndex];
            spritesList[rIndex] = s;
        }
    }

    // Display game over screen
    void gameOver()
    {
        correctClickCount++;
        if (correctClickCount == 8)
        {
            GameOverScreen.Setup();
        }
    }
}