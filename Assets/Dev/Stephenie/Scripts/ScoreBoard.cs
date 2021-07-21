using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("Score Board Entry Container");
        entryTemplate = entryContainer.Find("Score Board Entry Template");
        Debug.Log(highscoreEntryList);
        Debug.Log(highscoreEntryTransformList);
        entryTemplate.gameObject.SetActive(false);

        /*highscoreEntryList = new List<HighscoreEntry>()
         {
             new HighscoreEntry { score = 20, name = "Mina" },
             new HighscoreEntry { score = 14, name = "Leslie" },
             new HighscoreEntry { score = 3, name = "Martin" },
             new HighscoreEntry { score = 9, name = "Ronald" },
         };*/

        PlayerPrefs.HasKey("highscoreTable");

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscoreEntryList.Count; j++)
            {
                if(highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
            }
        }
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

        
        Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
            float templateHeight = 34f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "th"; break;

                case 1: rankString = "1st"; break;
                case 2: rankString = "2nd"; break;
                case 3: rankString = "3rd"; break;
            }

        entryTransform.Find("Position Entry One").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("Score Entry One").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("Name Entry One").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
