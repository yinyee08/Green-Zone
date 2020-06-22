using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ScoreData
{
    public string alias;
    public string rank;

}
[System.Serializable]
public class Message
{
    public ScoreData[] data;
}

[System.Serializable]
public class Leaderboard
{
    public Message message;
    public int status;
}

public class LeaderBoard : MonoBehaviour
{
    List<ScoreDataList> scorelist = new List<ScoreDataList>();

    public GameObject boardPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (!string.IsNullOrEmpty(GetTeamAlias.curlevel))
        {
            StartCoroutine(GetLeaderBoard(GetTeamAlias.curlevel));
        }
        else
        {
            StartCoroutine(GetLeaderBoard("easy"));
        }
    }

    IEnumerator GetPlayerRecord(string alias, string badge, int total)
    {

        using (UnityWebRequest www = UnityWebRequest.Get("http://api.tenenet.net/getPlayer?token=7bfe7669d12cb3f64681d71d4bb54a22&alias=" + alias))
        {
            yield return www.SendWebRequest();
            var data = www.downloadHandler.text;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Data team = JsonUtility.FromJson<Data>(data);
                for (int j = 0; j < team.message.score.Length; j++)
                {
                    if (team.message.score[j].metric_id == badge)
                    {
                        scorelist.Add(new ScoreDataList(team.message.first_name, team.message.score[j].value));
                        if (scorelist.Count == total)
                        {
                            SortOurList(boardPanel);
                        }
                    }
                }

            }

        }
    }

    IEnumerator GetLeaderBoard(string level)
    {

        string boardtype = "";
        string curscore = "";

        if (level == "easy")
        {
            boardtype = "lbl1_score";
            curscore = "easy_score";
        }
        else if (level == "medium")
        {
            boardtype = "lbl2_score";
            curscore = "medium_score";
        }
        else if (level == "hard")
        {
            boardtype = "lbl3_score";
            curscore = "hard_score";
        }

        using (UnityWebRequest www = UnityWebRequest.Get("http://api.tenenet.net/getLeaderboard?token=7bfe7669d12cb3f64681d71d4bb54a22&id=" + boardtype))
        {
            yield return www.SendWebRequest();
            var data = www.downloadHandler.text;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Leaderboard record = JsonUtility.FromJson<Leaderboard>(data);
                for (int i = 0; i < record.message.data.Length; i++)
                {

                    StartCoroutine(GetPlayerRecord(record.message.data[i].alias, curscore, record.message.data.Length));

                }

            }
        }
    }

    private void SortOurList(GameObject panel)
    {
        scorelist.Sort(SortFunc);
        for (int i = 0; i < scorelist.Count; i++)
        {
            if (i < 9)
            {
                panel.transform.GetChild(i).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = scorelist[i].teamname;
                panel.transform.GetChild(i).gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (scorelist[i].teamscore).ToString();

            }
        }

        if (scorelist.Count <= 3)
        {

            for (int i = 3; i < 9; i++)
            {
                panel.transform.GetChild(i).gameObject.SetActive(false);
            }

        }

    }

    private int SortFunc(ScoreDataList a, ScoreDataList b)
    {
        if (a.teamscore > b.teamscore)
        {
            return -1;
        }
        else if (a.teamscore < b.teamscore)
        {
            return 1;
        }
        return 0;
    }

    public void setEasyLeaderBoard()
    {
        scorelist.Clear();
        StartCoroutine(GetLeaderBoard("easy"));
    }

    public void setMediumLeaderboard()
    {
        scorelist.Clear();
        StartCoroutine(GetLeaderBoard("medium"));
    }

    public void setHardLeaderboard()
    {
        scorelist.Clear();
        StartCoroutine(GetLeaderBoard("hard"));
    }

    public void gotoHomeScene()
    {
        SceneManager.LoadScene("Room1");
    }
}