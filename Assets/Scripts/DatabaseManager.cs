using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// csv파일을 파싱하고 DialogueManager에 제공
public class DatabaseManager : MonoBehaviour
{
    [Tooltip("파싱 할 파일 이름")]
    [SerializeField] private string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;

    void Awake()
    {
        Dialogue[] dialogues = Parse(csv_FileName);
        for(int i = 0; i < dialogues.Length; i++)
        {
            dialogueDic.Add(i + 1, dialogues[i]);
        }
        isFinish = true;
    }

    // start부터 end까지의 대화를 배열로 반환
    public Dialogue[] GetDialogue(int _startNum, int _endNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue> ();

        for(int i =0;i<= _endNum - _startNum; i++)
        {
            dialogueList.Add(dialogueDic[_startNum + i]);
        }

        return dialogueList.ToArray();
    }

    // csv 파일 하나를 통채로 읽어옴
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        // 엔터를 기준으로 줄을 나눔
        string[] data = csvData.text.Split(new char[] { '\n' });


        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue();

            dialogue.name = row[1];

            List<string> contextList = new List<string>();

            do
            {
                contextList.Add(row[2]);

                if (++i < data.Length)
                    row = data[i].Split(new char[] { ',' });
                else
                    break;

            } while (row[1].ToString() == "");

            dialogue.contexts = contextList.ToArray();

            dialogueList.Add(dialogue);
        }

        return dialogueList.ToArray();
    }
}
