using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private Sprite bgImage;



    [SerializeField]
    private PuzzleData puzzleData;

    public  List<Sprite> gamePuzzles = new List<Sprite>();
    public List<Button> btns = new List<Button>();

    public int amountAdded = 0;

    private bool firstGuess, secondGuess;

    private int countGuess;
    private int countCorrectGuess;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;


  /*  private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/meyveler");

    }*/
    void Start()
    {
        GetButtons();
        AddListener();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count /2;
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        Debug.Log(objects.Length);
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }

    }

    void AddGamePuzzles() 
    {
        int totalButtons = btns.Count;
        int totalPairs = totalButtons / 2; // We need half the number of pairs

        int spriteIndex = 0;

        for (int i = 0; i < totalPairs; i++)
        {
            // Ensure spriteIndex doesn't go out of bounds
            if (spriteIndex >= puzzleData.puzzles.Length)
            {
                spriteIndex = 0; // Loop back to the start if we've used all sprites
            }

            // Add the pair of the current sprite
            gamePuzzles.Add(puzzleData.puzzles[spriteIndex]);
            gamePuzzles.Add(puzzleData.puzzles[spriteIndex]);

            // Move to the next sprite
            spriteIndex++;
        }

        // Shuffle the list to randomize the puzzle positions
        Shuffle(gamePuzzles);
    }

    void AddListener()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("You click "+name);
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            StartCoroutine(CheckIfPuzzleMatch());

        }
    }

    IEnumerator CheckIfPuzzleMatch()
    {
        yield return new WaitForSeconds(.5f);

        if (firstGuessPuzzle == secondGuessPuzzle && firstGuessIndex != secondGuessIndex)
        {
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;


            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfPuzzleFinished();
        }
        else
        {
            //temp
            yield return new WaitForSeconds(.5f);
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;

        }

        yield return new WaitForSeconds(.5f);

        firstGuess = secondGuess = false;
    }

    void CheckIfPuzzleFinished()
    {
        countCorrectGuess++;

        if (countCorrectGuess == gameGuesses)
        {
            Debug.Log("oyun bitti " + countGuess + " seferde bitirdin.");
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count - 1);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }


}
