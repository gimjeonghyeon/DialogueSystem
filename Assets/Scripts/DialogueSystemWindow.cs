using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueSystemWindow : MonoBehaviour
{
    public Image textBox;
    public TextMeshProUGUI textBoxText;
    public Image speakerNameBox;
    public TextMeshProUGUI speakerNameBoxText;
    public Image enterArrow;

    private string dialogue;
    private bool isClicked;
    private UnityAction StopDialogue;

    public void SetText(string speakerName, string text, UnityAction stopDialogue)
    {
        StopDialogue = stopDialogue;
        speakerNameBoxText.text = speakerName;
        dialogue = text;

        isClicked = false;

        StartCoroutine("DoUpdateDialogue");
    }

    private IEnumerator DoUpdateDialogue()
    {
        string tempText = "";

        for (int i = 0; i < dialogue.Length; i++)
        {
            tempText += dialogue[i];
            textBoxText.text = tempText;

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitUntil(() => isClicked);
        
        if (StopDialogue != null)
        {
            StopDialogue.Invoke();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isClicked)
            {
                isClicked = true;
            }
        }
    }

    private void OnDestroy()
    {
        StopCoroutine("DoUpdateDialogue");
    }
}


// 점진적으로 한 줄 씩 보여지는 다이어로그 타입을 하나 만들어서 다르게 연출해줄 것