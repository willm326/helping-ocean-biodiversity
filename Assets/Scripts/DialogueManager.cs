using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource Type;
    private Queue<string> sentences;
    public string[] sentencesA;
    public string[] sentencesB;
    public string[] sentencesC;
    public string[] sentencesD;
    public string[] sentencesE;
    public string[] sentencesF;
    public string OptionAText;
    public string OptionBText;
    public string OptionCText;
    public string OptionDText;
    public string OptionEText;
    public string OptionFText;
    public Image Environment;
    public Image Character;
    public Image Cinematic;
    public Text Name;
    public Text dialogueText;
    public int numberOfOptions;
    public string scenario;
    public string nextScenario;
    public string nextScenarioA;
    public string nextScenarioB;
    public string nextScenarioC;
    public string nextScenarioD;
    public string nextScenarioE;
    public string nextScenarioF;
    public Button OptionA;
    public Button OptionB;
    public Button OptionC;
    public Button OptionD;
    public Button OptionE;
    public Button OptionF;
    public GameObject ButtonLayer;
    public Sprite scoreFine;
    public Sprite scoreGood;
    public Sprite scoreBad;
    public Sprite scorePerfect;
    public Sprite scoreTerrible;
    public Image Fish;
    public Image Shark;
    public Image MonkSeal;
    public Image Turtle;
    public Image Vaquita;
    public Image Bird;
    public Image Manatee;
    public bool chosen = false;
    public bool timed;
    public bool timeStart = false;
    public float timeLimit;
    public float startTime;
    public float time;
    public float minutes;
    public float seconds;
    public Text timer;
    public int moralsA = 0;
    public int moralsB = 0;
    public int moralsC = 0;
    public int moralsD = 0;
    public int moralsE = 0;
    public int moralsF = 0;
    public bool positiveMorals = false;
    public bool negativeMorals = false;
    public GameObject NextButton;
    public GameObject StoryText;
    public GameObject PlayAgain;
    public GameObject PlayAgainPerfect;
    public string quote;

    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        Ending();

        if (chosen && timed)
        {
            timer.gameObject.SetActive(false);
        }
        time = timeLimit - Time.time;
        minutes = Mathf.FloorToInt(time % 60);
        seconds = Mathf.FloorToInt(time * 1000f % 1000);
        if (time >= 0)
        {
            timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else if (timed && timeStart && !chosen)
        {
            WorldVariables.choice = 6;
            WorldVariables.choosing = false;
            timer.text = string.Format("{0:0}:{1:00}", 0f, 0f);
            GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Button");
            foreach (GameObject Button in Buttons)
            {
                Destroy(Button.gameObject);
            }
            timed = false;
            timeStart = false;
        }

        if (WorldVariables.choice == 1 && !chosen)
        {
            if (moralsA > 0)
            {
                positiveMorals = true;
            }
            else if (moralsA < 0)
            {
                negativeMorals = false;
            }

            if (WorldVariables.currentScenario == "5" || WorldVariables.currentScenario == "5R" || WorldVariables.currentScenario == "5R2" || WorldVariables.currentScenario == "5R3" || WorldVariables.currentScenario == "5R4" || WorldVariables.currentScenario == "5R5")
            {
                sentences.Clear();
                WorldVariables.OptionA5R = false;
                foreach (string sentence in sentencesA)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentenceA();
                chosen = true;
                WorldVariables.morals += moralsA;
            }
            else if (WorldVariables.currentScenario == "13R1")
            {
                WorldVariables.choice = 0;
                WorldVariables.OptionA13R = false;
                int random = Random.Range(1, 5);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "13R1A Bad";
                }
                else
                {
                    WorldVariables.currentScenario = "13R1A Good";
                }
                WorldVariables.morals += moralsA;
            }
            else if (WorldVariables.currentScenario == "13R2")
            {
                WorldVariables.choice = 0;
                WorldVariables.OptionA13R = false;
                int random = Random.Range(1, 5);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "13R2A Bad";
                }
                else
                {
                    WorldVariables.currentScenario = "13R2A Good";
                }
                WorldVariables.morals += moralsA;
            }
            else if (WorldVariables.currentScenario == "13R3")
            {
                WorldVariables.choice = 0;
                WorldVariables.OptionA13R = false;
                int random = Random.Range(1, 5);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "13R3A Bad";
                }
                else
                {
                    WorldVariables.currentScenario = "13R3A Good";
                }
                WorldVariables.morals += moralsA;
            }
            else if (WorldVariables.currentScenario == "7AAAA")
            {
                WorldVariables.choice = 0;
                int random = Random.Range(1, 3);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "7AAAAA Good";
                }
                else if (random == 2)
                {
                    WorldVariables.currentScenario = "7AAAAA Bad";
                }
                WorldVariables.morals += moralsA;
            }
            else if (WorldVariables.currentScenario == "15")
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                WorldVariables.choice = 0;
                WorldVariables.morals = 0;
                WorldVariables.choosing = false;
                WorldVariables.currentScenario = "1";
                WorldVariables.firstAid = false;
                WorldVariables.OptionA5R = true;
                WorldVariables.OptionB5R = true;
                WorldVariables.OptionC5R = true;
                WorldVariables.OptionD5R = true;
                WorldVariables.OptionE5R = true;
                WorldVariables.OptionA13R = true;
                WorldVariables.OptionB13R = true;
                WorldVariables.OptionC13R = true;
                WorldVariables.OptionD13R = true;

                WorldVariables.vaquita = "";
                WorldVariables.turtle = "";
                WorldVariables.monkSeal = "";
                WorldVariables.manatee = "";
            }
            else
            {
                sentences.Clear();

                foreach (string sentence in sentencesA)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentenceA();
                chosen = true;
                timeStart = false;
                WorldVariables.morals += moralsA;
            }
        }

        else if (WorldVariables.choice == 2 && !chosen)
        {
            if (moralsB > 0)
            {
                positiveMorals = true;
            }
            else if (moralsB < 0)
            {
                negativeMorals = false;
            }

            if (WorldVariables.currentScenario == "5" || WorldVariables.currentScenario == "5R" || WorldVariables.currentScenario == "5R2" || WorldVariables.currentScenario == "5R3" || WorldVariables.currentScenario == "5R4" || WorldVariables.currentScenario == "5R5")
            {
                sentences.Clear();
                WorldVariables.OptionB5R = false;
                foreach (string sentence in sentencesB)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentenceB();
                chosen = true;
                WorldVariables.morals += moralsB;
            }
            else if (WorldVariables.currentScenario == "13R1")
            {
                WorldVariables.choice = 0;
                WorldVariables.OptionB13R = false;
                int random = Random.Range(1, 5);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "13R1B Bad";
                }
                else
                {
                    WorldVariables.currentScenario = "13R1B Good";
                }
                WorldVariables.morals += moralsB;
            }
            else if (WorldVariables.currentScenario == "13R2")
            {
                WorldVariables.choice = 0;
                WorldVariables.OptionB13R = false;
                int random = Random.Range(1, 5);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "13R2B Bad";
                }
                else
                {
                    WorldVariables.currentScenario = "13R2B Good";
                }
                WorldVariables.morals += moralsB;
            }
            else if (WorldVariables.currentScenario == "13R3")
            {
                WorldVariables.choice = 0;
                WorldVariables.OptionB13R = false;
                int random = Random.Range(1, 5);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "13R3B Bad";
                }
                else
                {
                    WorldVariables.currentScenario = "13R3B Good";
                }
                WorldVariables.morals += moralsB;
            }
            else if (WorldVariables.currentScenario == "7AAAA")
            {
                WorldVariables.choice = 0;
                int random = Random.Range(1, 3);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "7AAAAA Good";
                }
                else if (random == 2)
                {
                    WorldVariables.currentScenario = "7AAAAA Bad";
                }
                WorldVariables.morals += moralsB;
            }
            else
            {
                sentences.Clear();

                foreach (string sentence in sentencesB)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentenceB();
                chosen = true;
                WorldVariables.morals += moralsB;
            }
        }

        else if (WorldVariables.choice == 3 && !chosen)
        {
            if (moralsC > 0)
            {
                positiveMorals = true;
            }
            else if (moralsC < 0)
            {
                negativeMorals = false;
            }

            if (WorldVariables.currentScenario == "5" || WorldVariables.currentScenario == "5R" || WorldVariables.currentScenario == "5R2" || WorldVariables.currentScenario == "5R3" || WorldVariables.currentScenario == "5R4" || WorldVariables.currentScenario == "5R5")
            {
                sentences.Clear();
                WorldVariables.OptionC5R = false;
                foreach (string sentence in sentencesC)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentenceC();
                chosen = true;
                WorldVariables.morals += moralsC;
            }
            else if (WorldVariables.currentScenario == "13R1")
            {
                WorldVariables.choice = 0;
                WorldVariables.OptionC13R = false;
                int random = Random.Range(1, 5);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "13R1C Bad";
                }
                else
                {
                    WorldVariables.currentScenario = "13R1C Good";
                }
                WorldVariables.morals += moralsC;
            }
            else if (WorldVariables.currentScenario == "13R2")
            {
                WorldVariables.choice = 0;
                WorldVariables.OptionC13R = false;
                int random = Random.Range(1, 5);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "13R2C Bad";
                }
                else
                {
                    WorldVariables.currentScenario = "13R2C Good";
                }
                WorldVariables.morals += moralsC;
            }
            else if (WorldVariables.currentScenario == "13R3")
            {
                WorldVariables.choice = 0;
                WorldVariables.OptionC13R = false;
                int random = Random.Range(1, 5);
                if (random == 1)
                {
                    WorldVariables.currentScenario = "13R3C Bad";
                }
                else
                {
                    WorldVariables.currentScenario = "13R3C Good";
                }
                WorldVariables.morals += moralsC;
            }
            else
            {
                sentences.Clear();

                foreach (string sentence in sentencesC)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentenceC();
                chosen = true;
                WorldVariables.morals += moralsC;
            }
        }

        else if (WorldVariables.choice == 4 && !chosen)
        {
            if (moralsD > 0)
            {
                positiveMorals = true;
            }
            else if (moralsD < 0)
            {
                negativeMorals = false;
            }

            if (WorldVariables.currentScenario == "5" || WorldVariables.currentScenario == "5R" || WorldVariables.currentScenario == "5R2" || WorldVariables.currentScenario == "5R3" || WorldVariables.currentScenario == "5R4" || WorldVariables.currentScenario == "5R5")
            {
                sentences.Clear();
                WorldVariables.OptionD5R = false;
                foreach (string sentence in sentencesD)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentenceD();
                chosen = true;
                WorldVariables.morals += moralsD;
            }
            else
            {
                sentences.Clear();

                foreach (string sentence in sentencesD)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentenceD();
                chosen = true;
                WorldVariables.morals += moralsD;
            }
        }
        else if (WorldVariables.choice == 5 && !chosen)
        {
            if (moralsE > 0)
            {
                positiveMorals = true;
            }
            else if (moralsE < 0)
            {
                negativeMorals = false;
            }

            if (WorldVariables.currentScenario == "5" || WorldVariables.currentScenario == "5R" || WorldVariables.currentScenario == "5R2" || WorldVariables.currentScenario == "5R3" || WorldVariables.currentScenario == "5R4" || WorldVariables.currentScenario == "5R5")
            {
                sentences.Clear();
                WorldVariables.OptionE5R = false;
                WorldVariables.firstAid = true;
                foreach (string sentence in sentencesE)
                {
                    sentences.Enqueue(sentence);
                }
                DisplayNextSentenceE();
                chosen = true;
                WorldVariables.morals += moralsE;
            }
            else
            {
                sentences.Clear();
                foreach (string sentence in sentencesE)
                {
                    sentences.Enqueue(sentence);
                }
                DisplayNextSentenceE();
                chosen = true;
                WorldVariables.morals += moralsE;
            }
        }
        else if (WorldVariables.choice == 6 && !chosen)
        {
            if (moralsF > 0)
            {
                positiveMorals = true;
            }
            else if (moralsF < 0)
            {
                negativeMorals = false;
            }

            sentences.Clear();
            foreach (string sentence in sentencesF)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentenceF();
            chosen = true;
            WorldVariables.morals += moralsF;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Environment.sprite = dialogue.Environment;
        Character.sprite = dialogue.Character;
        Cinematic.sprite = dialogue.Cinematic;
        Name.text = dialogue.speaker;
        numberOfOptions = dialogue.Options;
        sentencesA = dialogue.sentencesA;
        sentencesB = dialogue.sentencesB;
        sentencesC = dialogue.sentencesC;
        sentencesD = dialogue.sentencesD;
        sentencesE = dialogue.sentencesE;
        sentencesF = dialogue.sentencesF;
        scenario = dialogue.Scenario;
        nextScenario = dialogue.nextScenario;
        nextScenarioA = dialogue.nextScenarioA;
        nextScenarioB = dialogue.nextScenarioB;
        nextScenarioC = dialogue.nextScenarioC;
        nextScenarioD = dialogue.nextScenarioD;
        nextScenarioE = dialogue.nextScenarioE;
        nextScenarioF = dialogue.nextScenarioF;
        OptionAText = dialogue.OptionAButton;
        OptionBText = dialogue.OptionBButton;
        OptionCText = dialogue.OptionCButton;
        OptionDText = dialogue.OptionDButton;
        OptionEText = dialogue.OptionEButton;
        OptionFText = dialogue.OptionFButton;
        moralsA = dialogue.moralsA;
        moralsB = dialogue.moralsB;
        moralsC = dialogue.moralsC;
        moralsD = dialogue.moralsD;
        moralsE = dialogue.moralsE;
        moralsF = dialogue.moralsF;
        timed = dialogue.timed;
        timeLimit = dialogue.timeLimit;
        chosen = false;

        if (!timed)
        {
            timer.gameObject.SetActive(false);
        }

        if (dialogue.vaquita != " " && dialogue.vaquita != "" && dialogue.vaquita != null)
        {
            WorldVariables.vaquita = dialogue.vaquita;
        }

        if (dialogue.turtle != " " && dialogue.turtle != "" && dialogue.turtle != null)
        {
            WorldVariables.turtle = dialogue.turtle;
        }

        if (dialogue.monkSeal != " " && dialogue.monkSeal != "" && dialogue.monkSeal != null)
        {
            WorldVariables.monkSeal = dialogue.monkSeal;
        }

        if (dialogue.manatee != " " && dialogue.manatee != "" && dialogue.manatee != null)
        {
            WorldVariables.manatee = dialogue.manatee;
        }

        if (dialogue.bird != " " && dialogue.bird != "" && dialogue.bird != null)
        {
            WorldVariables.bird = dialogue.bird;
        }

        if (dialogue.fish != " " && dialogue.fish != "" && dialogue.fish != null)
        {
            WorldVariables.fish = dialogue.fish;
        }

        if (dialogue.shark != " " && dialogue.shark != "" && dialogue.shark != null)
        {
            WorldVariables.shark = dialogue.shark;
        }

        if (WorldVariables.currentScenario == "10A")
        {
            WorldVariables.secludedArea = true;
        }
        else if (WorldVariables.currentScenario == "10B")
        {
            WorldVariables.crowdedArea = true;
        }

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void Ending()
    {
        if (WorldVariables.currentScenario == "15")
        {
            if (WorldVariables.morals == 0)
            {
                Cinematic.sprite = scoreFine;
                PlayAgain.SetActive(true);
            }
            else if (WorldVariables.morals > 0)
            {
                Cinematic.sprite = scoreGood;
                PlayAgain.SetActive(true);
            }
            else if (WorldVariables.morals < 0)
            {
                Cinematic.sprite = scoreBad;
                PlayAgain.SetActive(true);
            }
            else if (WorldVariables.morals > 0 && !negativeMorals && WorldVariables.vaquita == "good" && WorldVariables.monkSeal == "good" && WorldVariables.manatee == "good" && WorldVariables.bird == "good")
            {
                Cinematic.sprite = scorePerfect;
                PlayAgainPerfect.SetActive(true);
            }
            else if (WorldVariables.morals > 0 && !negativeMorals && WorldVariables.vaquita == "good" && WorldVariables.fish == "good" && WorldVariables.manatee == "good" && WorldVariables.bird == "good")
            {
                Cinematic.sprite = scorePerfect;
                PlayAgainPerfect.SetActive(true);
            }
            else if (WorldVariables.morals < 0 && !positiveMorals)
            {
                Cinematic.sprite = scoreTerrible;
                PlayAgain.SetActive(true);
            }

            if (WorldVariables.vaquita == "good")
            {
                Vaquita.gameObject.SetActive(true);
            }
            if (WorldVariables.turtle == "good")
            {
                Turtle.gameObject.SetActive(true);
            }
            if (WorldVariables.monkSeal == "good")
            {
                MonkSeal.gameObject.SetActive(true);
            }
            if (WorldVariables.manatee == "good")
            {
                Manatee.gameObject.SetActive(true);
            }
            if (WorldVariables.bird == "good")
            {
                Bird.gameObject.SetActive(true);
            }
            if (WorldVariables.shark == "good")
            {
                Shark.gameObject.SetActive(true);
            }
            if (WorldVariables.fish == "good")
            {
                Fish.gameObject.SetActive(true);
            }

            NextButton.SetActive(false);
            StoryText.SetActive(false);

        }
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentenceA()
    {
        if (sentences.Count == 0)
        {
            EndDialogueA();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentenceB()
    {
        if (sentences.Count == 0)
        {
            EndDialogueB();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentenceC()
    {
        if (sentences.Count == 0)
        {
            EndDialogueC();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentenceD()
    {
        if (sentences.Count == 0)
        {
            EndDialogueD();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentenceE()
    {
        if (sentences.Count == 0)
        {
            EndDialogueE();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentenceF()
    {
        if (sentences.Count == 0)
        {
            EndDialogueF();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (Type.isPlaying == false && letter.ToString() != " ")
            {
                Type.UnPause();
            }
            if (letter.ToString() == "." || letter.ToString() == "?" || letter.ToString() == "M" || letter.ToString() == quote)
            {
                Type.Pause();
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    void EndDialogue()
    {
        Debug.Log("Scenario Finished");
        if (timed && !WorldVariables.choosing)
        {
            timer.gameObject.SetActive(true);
            timeLimit = timeLimit + Time.time + 0.1f;
            timeStart = true;
        }

        if (numberOfOptions == 0)
        {
            if (WorldVariables.currentScenario == "14")
            {
                if (WorldVariables.vaquita == "good")
                {
                    WorldVariables.currentScenario = "14A Good";
                }
                else if (WorldVariables.vaquita == "" || WorldVariables.vaquita == " " || WorldVariables.vaquita == null)
                {
                    WorldVariables.currentScenario = "14A Neutral";
                }
                else if (WorldVariables.vaquita == "bad")
                {
                    WorldVariables.currentScenario = "14A Bad";
                }
                else
                {
                    WorldVariables.currentScenario = "14A Neutral";
                }
            }
            else if (WorldVariables.currentScenario == "14A Good" || WorldVariables.currentScenario == "14A2 Neutral" || WorldVariables.currentScenario == "14A2 Bad")
            {
                if (WorldVariables.turtle == "" || WorldVariables.turtle == " " || WorldVariables.turtle == null || WorldVariables.turtle == "good")
                {
                    WorldVariables.currentScenario = "14B Good";
                }
                else if (WorldVariables.turtle == "first aid")
                {
                    WorldVariables.currentScenario = "14B Neutral";
                }
                else if (WorldVariables.turtle == "beach")
                {
                    WorldVariables.currentScenario = "14B Beach";
                }
                else if (WorldVariables.turtle == "bad")
                {
                    WorldVariables.currentScenario = "14B Bad";
                }
            }
            else if (WorldVariables.currentScenario == "14B Good" || WorldVariables.currentScenario == "14B2 Neutral" || WorldVariables.currentScenario == "14B2 Bad")
            {
                if (WorldVariables.monkSeal == "" || WorldVariables.monkSeal == " " || WorldVariables.monkSeal == null)
                {
                    WorldVariables.currentScenario = "14C Neutral";
                }
                else if (WorldVariables.monkSeal == "good")
                {
                    WorldVariables.currentScenario = "14C Good";
                }
                else if (WorldVariables.monkSeal == "bad")
                {
                    WorldVariables.currentScenario = "14C Bad";
                }
            }
            else if (WorldVariables.currentScenario == "14C Good" || WorldVariables.currentScenario == "14C2 Neutral" || WorldVariables.currentScenario == "14C2 Bad")
            {
                if (WorldVariables.manatee == "" || WorldVariables.manatee == " " || WorldVariables.manatee == null)
                {
                    WorldVariables.currentScenario = "14D Neutral";
                }
                else if (WorldVariables.manatee == "good")
                {
                    WorldVariables.currentScenario = "14D Good";
                }
                else if (WorldVariables.manatee == "bad")
                {
                    WorldVariables.currentScenario = "14D Bad";
                }
            }
            else
            {
                WorldVariables.currentScenario = nextScenario;
            }
        }

        else if (numberOfOptions == 1 && WorldVariables.currentScenario != "15")
        {
            Button InstatiatedOptionA = Instantiate(OptionA, new Vector3(0f, 150f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionA.transform.localPosition = new Vector3(0f, 200f, 0f);
            InstatiatedOptionA.GetComponentInChildren<Text>().text = OptionAText;
            WorldVariables.choosing = true;
        }

        else if (numberOfOptions == 2)
        {
            Button InstatiatedOptionA = Instantiate(OptionA, new Vector3(0f, 150f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionA.transform.localPosition = new Vector3(0f, 300f, 0f);
            InstatiatedOptionA.GetComponentInChildren<Text>().text = OptionAText;
            Button InstatiatedOptionB = Instantiate(OptionB, new Vector3(0f, 100f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionB.transform.localPosition = new Vector3(0f, 100f, 0f);
            InstatiatedOptionB.GetComponentInChildren<Text>().text = OptionBText;
            WorldVariables.choosing = true;
            if(WorldVariables.currentScenario == "10BAB" || WorldVariables.currentScenario == "10BBA2")
            {
                if (WorldVariables.secludedArea)
                {
                    InstatiatedOptionA.interactable = false;
                }
            }
            if (WorldVariables.currentScenario == "10AA" && WorldVariables.crowdedArea)
            {
                InstatiatedOptionB.interactable = false;
            }
        }

        else if (numberOfOptions == 3)
        {
            Button InstatiatedOptionA = Instantiate(OptionA, new Vector3(0f, 150f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionA.transform.localPosition = new Vector3(0f, 400f, 0f);
            InstatiatedOptionA.GetComponentInChildren<Text>().text = OptionAText;
            if (WorldVariables.currentScenario == "6AAAAA" && !WorldVariables.firstAid)
            {
                InstatiatedOptionA.interactable = false;
            }
            Button InstatiatedOptionB = Instantiate(OptionB, new Vector3(0f, 100f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionB.transform.localPosition = new Vector3(0f, 200f, 0f);
            InstatiatedOptionB.GetComponentInChildren<Text>().text = OptionBText;
            Button InstatiatedOptionC = Instantiate(OptionC, new Vector3(0f, 50f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionC.transform.localPosition = new Vector3(0f, 0f, 0f);
            InstatiatedOptionC.GetComponentInChildren<Text>().text = OptionCText;
            WorldVariables.choosing = true;
        }

        else if (numberOfOptions == 4)
        {
            Button InstatiatedOptionA = Instantiate(OptionA, new Vector3(0f, 150f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionA.transform.localPosition = new Vector3(0f, 500f, 0f);
            InstatiatedOptionA.GetComponentInChildren<Text>().text = OptionAText;
            Button InstatiatedOptionB = Instantiate(OptionB, new Vector3(0f, 100f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionB.transform.localPosition = new Vector3(0f, 300f, 0f);
            InstatiatedOptionB.GetComponentInChildren<Text>().text = OptionBText;
            Button InstatiatedOptionC = Instantiate(OptionC, new Vector3(0f, 50f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionC.transform.localPosition = new Vector3(0f, 100f, 0f);
            InstatiatedOptionC.GetComponentInChildren<Text>().text = OptionCText;
            Button InstatiatedOptionD = Instantiate(OptionD, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionD.transform.localPosition = new Vector3(0f, -100f, 0f);
            InstatiatedOptionD.GetComponentInChildren<Text>().text = OptionDText;
            WorldVariables.choosing = true;
        }
        else if (numberOfOptions == 5)
        {
            Button InstatiatedOptionA = Instantiate(OptionA, new Vector3(0f, 150f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionA.transform.localPosition = new Vector3(0f, 500f, 0f);
            InstatiatedOptionA.GetComponentInChildren<Text>().text = OptionAText;
            if (!WorldVariables.OptionA13R)
            {
                InstatiatedOptionA.interactable = false;
            }
            Button InstatiatedOptionB = Instantiate(OptionB, new Vector3(0f, 100f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionB.transform.localPosition = new Vector3(0f, 350f, 0f);
            InstatiatedOptionB.GetComponentInChildren<Text>().text = OptionBText;
            if (!WorldVariables.OptionB13R)
            {
                InstatiatedOptionB.interactable = false;
            }
            Button InstatiatedOptionC = Instantiate(OptionC, new Vector3(0f, 50f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionC.transform.localPosition = new Vector3(0f, 200f, 0f);
            InstatiatedOptionC.GetComponentInChildren<Text>().text = OptionCText;
            if (!WorldVariables.OptionC13R)
            {
                InstatiatedOptionC.interactable = false;
            }
            Button InstatiatedOptionD = Instantiate(OptionD, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionD.transform.localPosition = new Vector3(0f, 50f, 0f);
            InstatiatedOptionD.GetComponentInChildren<Text>().text = OptionDText;
            if (!WorldVariables.OptionD13R)
            {
                InstatiatedOptionD.interactable = false;
            }
            Button InstatiatedOptionE = Instantiate(OptionE, new Vector3(0f, -140f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionE.transform.localPosition = new Vector3(0f, -100f, 0f);
            InstatiatedOptionE.GetComponentInChildren<Text>().text = OptionEText;
            WorldVariables.choosing = true;
        }
        else if (numberOfOptions == 6)
        {
            Button InstatiatedOptionA = Instantiate(OptionA, new Vector3(0f, 150f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionA.transform.localPosition = new Vector3(0f, 500f, 0f);
            InstatiatedOptionA.GetComponentInChildren<Text>().text = OptionAText;
            if (!WorldVariables.OptionA5R)
            {
                InstatiatedOptionA.interactable = false;
            }
            Button InstatiatedOptionB = Instantiate(OptionB, new Vector3(0f, 100f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionB.transform.localPosition = new Vector3(0f, 380f, 0f);
            InstatiatedOptionB.GetComponentInChildren<Text>().text = OptionBText;
            if (!WorldVariables.OptionB5R)
            {
                InstatiatedOptionB.interactable = false;
            }
            Button InstatiatedOptionC = Instantiate(OptionC, new Vector3(0f, 50f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionC.transform.localPosition = new Vector3(0f, 260f, 0f);
            InstatiatedOptionC.GetComponentInChildren<Text>().text = OptionCText;
            if (!WorldVariables.OptionC5R)
            {
                InstatiatedOptionC.interactable = false;
            }
            Button InstatiatedOptionD = Instantiate(OptionD, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionD.transform.localPosition = new Vector3(0f, 140f, 0f);
            InstatiatedOptionD.GetComponentInChildren<Text>().text = OptionDText;
            if (!WorldVariables.OptionD5R)
            {
                InstatiatedOptionD.interactable = false;
            }
            Button InstatiatedOptionE = Instantiate(OptionE, new Vector3(0f, -140f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionE.transform.localPosition = new Vector3(0f, 20f, 0f);
            InstatiatedOptionE.GetComponentInChildren<Text>().text = OptionEText;
            if (!WorldVariables.OptionE5R)
            {
                InstatiatedOptionE.interactable = false;
            }
            Button InstatiatedOptionF = Instantiate(OptionF, new Vector3(0f, -140f, 0f), new Quaternion(0f, 0f, 0f, 0f), ButtonLayer.transform);
            InstatiatedOptionF.transform.localPosition = new Vector3(0f, -100f, 0f);
            InstatiatedOptionF.GetComponentInChildren<Text>().text = OptionFText;
            WorldVariables.choosing = true;
        }
    }

    void EndDialogueA()
    {
        WorldVariables.choice = 0;
        WorldVariables.currentScenario = nextScenarioA;
    }

    void EndDialogueB()
    {
        WorldVariables.choice = 0;
        WorldVariables.currentScenario = nextScenarioB;
    }

    void EndDialogueC()
    {
        WorldVariables.choice = 0;
        WorldVariables.currentScenario = nextScenarioC;
    }

    void EndDialogueD()
    {
        WorldVariables.choice = 0;
        WorldVariables.currentScenario = nextScenarioD;
    }

    void EndDialogueE()
    {
        WorldVariables.choice = 0;
        WorldVariables.currentScenario = nextScenarioE;
    }

    void EndDialogueF()
    {
        WorldVariables.choice = 0;
        WorldVariables.currentScenario = nextScenarioF;
    }

}
