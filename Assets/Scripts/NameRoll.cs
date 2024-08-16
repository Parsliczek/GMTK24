using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameRoll : MonoBehaviour
{
    [SerializeField] string[] names;
    [SerializeField] Text currentName;
    [SerializeField] Button rollButton;

    [SerializeField] GameObject quizCanvas;
    [SerializeField] GameObject rollCanvas;

    [SerializeField] AudioSource tick;

    int Lenght;
    List<int> list = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        rollCanvas.SetActive(true);
        quizCanvas.SetActive(false);
        Lenght = names.Length;
    }

    public void RollName()
    {
        rollButton.interactable = false;
        StartCoroutine(RollSequence());
    }

    IEnumerator RollSequence()
    {
        int winner = UnityEngine.Random.Range(0, names.Length);

        int rollAmount = UnityEngine.Random.Range(20, 40);
        currentName.enabled = false;
        tick.Play();

        for (int i = 0; i < rollAmount; i++)
        {

            int roundIndex = GenNumber();
            currentName.text = names[roundIndex];
            currentName.enabled = true;
            yield return new WaitForSeconds(0.1f);

            currentName.enabled = false;
            yield return new WaitForSeconds(0.2f);
        }

        tick.Stop();
        yield return new WaitForSeconds(0.4f);
        currentName.enabled = true;
        currentName.text = names[winner];
        rollButton.interactable = true;
    }

    int GenNumber()
    {
        list.Clear();
        int fin = 0;
        for (int j = 0; j < Lenght; j++)
        {
            int Rand = UnityEngine.Random.Range(0, names.Length);
            while (list.Contains(Rand))
            {
                Rand = UnityEngine.Random.Range(0, names.Length);
            }
            list.Add(Rand);
            fin = (list[j]);
        }
        return (list[fin]);
    }

    public void EndNameSelection()
    {
        rollCanvas.SetActive(false);
        quizCanvas.SetActive(true);
    }
}
