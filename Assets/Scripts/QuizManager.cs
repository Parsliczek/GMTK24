using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuestionSO[] questions;
    [SerializeField] private int curQuestion;

    //Teams
    [SerializeField] private int pointsTeamA;
    [SerializeField] private int pointsTeamB;
    [SerializeField] bool whichTeam;
    [SerializeField] private Color teamAcolor;
    [SerializeField] private Color teamBcolor;
    [SerializeField] private bool answerSuccess;
    [SerializeField] private Text Apoints;
    [SerializeField] private Text Bpoints;

    //Texts
    [SerializeField] Text question;
    [SerializeField] Text answerA;
    [SerializeField] Text answerB;
    [SerializeField] Text answerC;
    [SerializeField] Text answerD;
    [SerializeField] Text timerText;
    [SerializeField] Text teamA;
    [SerializeField] Text teamB;

    //Buttons
    [SerializeField] Button question_but;
    [SerializeField] Button[] answers;

    //Colors
    [SerializeField] Color failColor;
    [SerializeField] Color successColor;
    [SerializeField] Color defaultColor;

    //Timer
    [SerializeField] float timer;
    [SerializeField] float timeStart = 20f;
    [SerializeField] bool pauseTimer;

    //Scenes
    [SerializeField] GameObject quizScene;
    [SerializeField] GameObject punishmentScene;
    [SerializeField] GameObject startingScene;
    [SerializeField] GameObject endingScene;

    [SerializeField] NameRoll NameRoll;
    [SerializeField] PunishmentRoll PunishmentRoll;

    // Start is called before the first frame update
    void Start()
    {
        pauseTimer = true;
        punishmentScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pauseTimer = !pauseTimer;
        }

        if(timer > 0 && !pauseTimer)
        {
            timer -= Time.deltaTime;
            timerText.text = ((int) timer).ToString();
        }

        if(timer <= 0)
        {
            TimerEnd();
        }
    }

    public void SlideSetter()
    {
        if (answerSuccess)
        {
            ResetButtons(true, true);
            curQuestion++;
            quizScene.SetActive(true);
            punishmentScene.SetActive(false);
            if (curQuestion == questions.Length)
            {
                quizScene.SetActive(false);

                Apoints.text = pointsTeamA.ToString();
                Apoints.color = teamAcolor;

                Bpoints.text = pointsTeamB.ToString();
                Bpoints.color = teamBcolor;

                endingScene.SetActive(true);
            }
            else
            {
                question_but.interactable = false;
                question.text = questions[curQuestion].question;
                answerA.text = questions[curQuestion].answerA;
                answerB.text = questions[curQuestion].answerB;
                answerC.text = questions[curQuestion].answerC;
                answerD.text = questions[curQuestion].answerD;
                timer = timeStart;
                timerText.text = ((int)timer).ToString();
                whichTeam = !whichTeam;
                pauseTimer = true;
                TeamSet(whichTeam);
            }
        }
        else
        {
            answerSuccess = true;
            StartPunishment();
        }
    }

    public void CheckAnswer(int answer)
    {
        pauseTimer = true;
        answerSuccess = false;
        for(int i = 0; i < answers.Length; i++)
        {
            if (i == answer)
            {
                if(answer == questions[curQuestion].correctAnswer)
                {
                    answers[i].GetComponent<Image>().color = successColor;
                    AddPoints(whichTeam, +1);
                    answerSuccess = true;
                }
                else
                {
                    answers[answer].GetComponent<Image>().color = failColor;

                }
            }
            answers[questions[curQuestion].correctAnswer].GetComponent<Image>().color = successColor;
        }
        ResetButtons(false, false);
        question_but.interactable = true;
    }

    public void InitialSlide()
    {
        quizScene.SetActive(true);
        punishmentScene.SetActive(false);
        ResetButtons(true, true);
        curQuestion = 0;
        question_but.interactable = false;
        question.text = questions[curQuestion].question;
        answerA.text = questions[curQuestion].answerA;
        answerB.text = questions[curQuestion].answerB;
        answerC.text = questions[curQuestion].answerC;
        answerD.text = questions[curQuestion].answerD;
        timer = timeStart;
        timerText.text = ((int)timer).ToString();
        pauseTimer = true;
        whichTeam = false;
        TeamSet(whichTeam);
    }

    void ResetButtons(bool state, bool changeColor)
    {
        for( int i = 0; i < answers.Length; i++)
        {
            if (changeColor)
            {
                answers[i].GetComponent<Image>().color = defaultColor;
            }
            answers[i].interactable = state;
        }
    }

    void TimerEnd()
    {
        CheckAnswer(5);
    }

    void AddPoints(bool team, int scoreValue)
    {
        if (!team)
        {
            pointsTeamA += scoreValue;
            teamA.text = pointsTeamA.ToString();
        }
        else
        {
            pointsTeamB += scoreValue;
            teamB.text = pointsTeamB.ToString();
        }
    }

    void TeamSet(bool team)
    {
        question_but.GetComponent<Image>().color = defaultColor;
        if (!team)
        {
            question_but.GetComponent<Image>().color = teamAcolor;
        }
        else
        {
            question_but.GetComponent<Image>().color = teamBcolor;
        }
    }

    void StartPunishment()
    {
        quizScene.SetActive(false);
        punishmentScene.SetActive(true);
        Debug.Log("fuck you");
    }
}
