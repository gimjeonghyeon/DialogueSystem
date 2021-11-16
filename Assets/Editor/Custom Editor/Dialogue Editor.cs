using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueEditor : EditorWindow
{
    [MenuItem("CustomTools/Dialogue Editor")]
    public static void Init()
    {
        var win = GetWindow (typeof (DialogueEditor));
        win.minSize = win.maxSize = new Vector2(520f, 700f);
    }
    
    [SerializeField] private List<DialogueData> dialogueDatas = new List<DialogueData>();
    
    private string[] options = {"Type1", "Type2", "Type3",};
    private string[] speackers = {"???", "Clayton", "Rita", "Pokota"};string myString = "Hello World";
    
    private Vector2 scrollPosition = Vector2.zero;

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 500f, 645f));
        DrawAddDialogueButton();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
        for (int i = 0; i < dialogueDatas.Count; i++)
        {
            DrawEnteredDialogue(i);
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(10, 660f, 500f, 40f));
        DrawSaveButton();
        GUILayout.EndArea();

    }

    private void DrawAddDialogueButton()
    {
        if (GUILayout.Button("ADD", GUILayout.Height(40f)))
        {
            dialogueDatas.Add(new DialogueData());
        }
    }

    
    private void DrawEnteredDialogue(int i)
    {
        GUILayout.BeginHorizontal();
        
        GUILayout.Label(i.ToString(), GUILayout.Width(20f), GUILayout.Height(40f));
        
        GUILayout.BeginVertical();
        GUILayoutOption[] settingOptions = {GUILayout.Width(80f)};
        dialogueDatas[i].option = EditorGUILayout.Popup(dialogueDatas[i].option, options, settingOptions); 
        dialogueDatas[i].speaker =  EditorGUILayout.Popup(dialogueDatas[i].speaker, speackers, settingOptions); 
        GUILayout.EndVertical();
        
        dialogueDatas[i].text = GUILayout.TextArea(dialogueDatas[i].text, GUILayout.Width(330f), GUILayout.ExpandWidth(false), GUILayout.Height(40f));
        
        if (GUILayout.Button("DEL", GUILayout.Width(40f), GUILayout.ExpandWidth(false), GUILayout.Height(40f)))
        {
            dialogueDatas.RemoveAt(i);   
        }
        
        GUILayout.EndHorizontal();
    }

    private void DrawSaveButton()
    {
        if (GUILayout.Button("SAVE", GUILayout.Height(40f)))
        {
            string json = JsonUtility.ToJson(dialogueDatas);
            Debug.Log(json);
        }
    }
}

// 리스트를 클래스로 만들어서 다시 json으로 변환
// 이번 작업은 계속 이어가고, 추후에 개선을 진행하는 식으로

[Serializable]
public class DialogueData
{
    public int option;
    public int speaker;
    public string text;
}