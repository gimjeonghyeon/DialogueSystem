using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    private List<Dialogue> dialogues;
    private bool isNextDialogue;
    public TextAsset textAsset;
    
    private void Start()
    {
        dialogues = GetTestDelayDialogue();
        StartCoroutine("DoDetection");
    }

    private IEnumerator DoDetection()
    {
        while (true)
        {
            yield return new WaitUntil(() => dialogues != null);

            var dialogueSystemWindow = Instantiate(Resources.Load<DialogueSystemPopup>("Popup/Dialogue System Popup"));
            
            foreach (var dialogue in dialogues)
            {
                isNextDialogue = false;

                if (dialogue.type == Dialogue.Type.NORMAL)
                {
                    dialogueSystemWindow.SetText(dialogue.speaker, dialogue.text, StopDialogue);
                }
                else
                {
                    dialogueSystemWindow.SetText(dialogue.speaker, dialogue.delayText, StopDialogue);
                }

                yield return new WaitUntil(() => isNextDialogue);
            }
            
            Destroy(dialogueSystemWindow.gameObject);
            
            dialogues = null;
        }
    }

    private void StopDialogue()
    {
        isNextDialogue = true;
    }

    private void OnDestroy()
    {
        StopCoroutine("DoDetection");
    }

    private List<Dialogue> GetTestDialogue()
    {
        List<Dialogue> testDialogue = new List<Dialogue>();
        
        testDialogue.Add(new Dialogue("클레이턴", "안녕하세요."));
        testDialogue.Add(new Dialogue("클레이턴", "클레이턴 입니다."));
        testDialogue.Add(new Dialogue("클레이턴", "만나서 반갑습니다."));
        testDialogue.Add(new Dialogue("리타", "안녕하세요."));
        testDialogue.Add(new Dialogue("리타", "저는 리타 입니다."));
        testDialogue.Add(new Dialogue("리타", "저도 만나서 반갑습니다."));
        testDialogue.Add(new Dialogue("클레이턴", "잘 부탁드립니다."));
        testDialogue.Add(new Dialogue("리타", "저도 잘 부탁드립니다."));
        testDialogue.Add(new Dialogue("클레이턴", "감사합니다."));
        
        return testDialogue;
    }
    
    private List<Dialogue> GetTestDelayDialogue()
    {
        List<Dialogue> testDialogue = new List<Dialogue>();

        testDialogue.Add(new Dialogue("클레이턴", new string[]
        {
            "안녕하세요~",
            "클레이턴 입니다.",
            "만나서 반갑습니다."
        }));
        
        testDialogue.Add(new Dialogue("리타", new string[]
        {
            "안녕하세요",
            "저는 리타 입니다.",
            "저도 만나서 반갑습니다."
        }));
        
        testDialogue.Add(new Dialogue("클레이턴", "잘 부탁드립니다."));
        testDialogue.Add(new Dialogue("리타", "저도 잘 부탁드립니다."));
        testDialogue.Add(new Dialogue("클레이턴", "감사합니다."));

        return testDialogue;
    }
}