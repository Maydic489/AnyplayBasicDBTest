using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BackendRequest : MonoBehaviour
{
    string webURL = "https://test-piggy.codedefeat.com/worktest/dev01/gameBackend/login.php";

    private void Start()
    {
        //StartCoroutine(Login("test01", "1150"));
    }

    public void RequestLogin(string username, string password)
    {
        StartCoroutine(Login(username, password));
    }

    IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(webURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
