using System;
using System.Collections;
using System.Text;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueSystemPopup : MonoBehaviour
{
    public Image textBox;
    public TextMeshProUGUI textBoxText;
    public Image speakerNameBox;
    public TextMeshProUGUI speakerNameBoxText;
    public Image enterArrow;

    private string dialogue;
    private string[] dialogues;
    private bool isCompleteDialogue;
    private bool isCompleteAllDialogue;
    private bool isClicked;
    private UnityAction StopDialogue;

    private void Awake()
    {
        textBoxText.DOText("", 0f);
    }

    public void SetText(string speakerName, string text, UnityAction stopDialogue)
    {
        speakerNameBoxText.text = speakerName;
        dialogue = text;
        textBoxText.text = "";
        
        StopDialogue = stopDialogue;
        
        isClicked = false;
        isCompleteAllDialogue = false;
        isCompleteDialogue = false;

        StartCoroutine("DoUpdateDialogue");
    }

    public void SetText(string speakerName, string[] text, UnityAction stopDialogue)
    {
        speakerNameBoxText.text = speakerName;
        dialogues = text;
        textBoxText.text = "";

        StopDialogue = stopDialogue;
        
        isClicked = false;
        isCompleteAllDialogue = false;
        isCompleteDialogue = false;
        
        StartCoroutine("DoUpdateDialogues");
    }

    private IEnumerator DoUpdateDialogue()
    {
       

        // TODO : 대화가 진행되는 부분을 트윈을 사용하여 할 수 있는지 -> 두트윈 프로 텍스트 매쉬프로에 글자수 출력하는게 있음, 두트윈에서 변수값 임의로 조절할 수 있는게 있음, 두트윈 텍스트매쉬프로 글자출력 찾아보기
        // TODO : 커맨드를 삽입, 사운드(액션), 딜레이, 속도 조절 -> 엑셀로 관리 (json 은 한눈에 관리가 어려움) 엑셀로 프로젝트에 넣고 Json으로 export한 뒤에 클래스형태로 대화 출력되게 (오토로 대화 넘어가는지 여부)
        /*
        string tempText = "";
         for (int i = 0; i < dialogue.Length; i++)
        {
            tempText += dialogue[i];
            textBoxText.text = tempText;

            yield return new WaitForSeconds(0.1f);
        }*/

        textBoxText.DOKill();
        textBoxText.DOText(dialogue, 1.0f).SetEase(Ease.Linear).onComplete = () =>
        {
            isCompleteAllDialogue = true;
        };

        yield return new WaitUntil(() => isCompleteAllDialogue && isClicked);
        
        if (StopDialogue != null)
        {
            StopDialogue.Invoke();
        }
    }

    private IEnumerator DoUpdateDialogues()
    {
        int index = 0;

        var allText = new StringBuilder();
        
        while (true)
        {

            allText.Append(dialogues[index]);

            textBoxText.DOKill();
            textBoxText.DOText(allText.ToString(), 1.0f).SetEase(Ease.Linear).onComplete = () =>
            {
                isCompleteDialogue = true;
            };

            yield return new WaitUntil(() => isCompleteDialogue && isClicked);

            isClicked = false;
            isCompleteDialogue = false;

            index++;
            if (dialogues.Length <= index)
            {
                isCompleteAllDialogue = true;
                break;
            }

            allText.Append("\n");
        }
        
        yield return new WaitUntil(() => isCompleteAllDialogue && isClicked);

        if (StopDialogue != null)
        {
            StopDialogue.Invoke();
        }
    }

    private void Update()
    {
        if ((isCompleteAllDialogue || isCompleteDialogue)  && Input.GetKeyDown(KeyCode.Space))
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


// npoi dll을 이용하여 읽어들이도록(글자를) 
// 시리얼라이즈 코드상으로 해줘야함, 제이슨으로 변환했는데 빈값이 나오면 시리얼라이즈가 필요한 것
// 