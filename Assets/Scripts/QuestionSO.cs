using UnityEngine;

[CreateAssetMenu(fileName = "QuestionSlide", menuName = "AHM_Quiz")]
public class QuestionSO : ScriptableObject
{
    public string question;
    public string answerA;
    public string answerB;
    public string answerC;
    public string answerD;
    [Range(0, 3)] public int correctAnswer;
}
