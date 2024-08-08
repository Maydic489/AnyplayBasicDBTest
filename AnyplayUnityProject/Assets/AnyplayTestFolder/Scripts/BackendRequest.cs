using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class BackendRequest : MonoBehaviour
{
    public UnityEvent<bool, string> OnWebResult;
    public UnityEvent OnSighUpSuccess;
    public UnityEvent<UserData> OnLoginSuccess;
    public UnityEvent<int> OnDiamondsUpdated;
    public UnityEvent<int> OnHeartsUpdated;

    string loginURL = "https://test-piggy.codedefeat.com/worktest/dev01/gameBackend/LoginUser.php";
    string signupURL = "https://test-piggy.codedefeat.com/worktest/dev01/gameBackend/SignupUser.php";
    string getDiamondsURL = "https://test-piggy.codedefeat.com/worktest/dev01/gameBackend/Diamond.php";
    string getHeartURL = "https://test-piggy.codedefeat.com/worktest/dev01/gameBackend/Heart.php";

    public static BackendRequest Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if(OnWebResult == null)
        {
            OnWebResult = new UnityEvent<bool, string>();
        }
        if(OnSighUpSuccess == null)
        {
            OnSighUpSuccess = new UnityEvent();
        }
    }

    private void Start()
    {
        //StartCoroutine(Login("test01", "1150"));
    }

    public void Login(string username, string password)
    {
        StartCoroutine(RequestLogin(username, password));
    }

    IEnumerator RequestLogin(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(loginURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                this.OnWebResult?.Invoke(false, www.error);
            }
            else
            {
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

                this.OnWebResult?.Invoke(response.success, response.message);

                if(response.success)
                    this.OnLoginSuccess?.Invoke(response.data);
            }
        }
    }

    public void SignUp(string username, string password)
    {
        StartCoroutine(RequestSignUp(username, password));
    }

    IEnumerator RequestSignUp(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("signUpUser", username);
        form.AddField("signUpPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(signupURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                this.OnWebResult?.Invoke(false, www.error);
            }
            else
            {
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                Debug.Log(www.downloadHandler.text);
                this.OnWebResult?.Invoke(response.success, response.message);

                if(response.success)
                    this.OnSighUpSuccess?.Invoke();
            }
        }
    }

    public void GetMoreDiamonds(int userID, int amount)
    {
        StartCoroutine(RequestGetMoreDiamonds(userID, amount));
    }

    IEnumerator RequestGetMoreDiamonds(int userID,int amount)
    {
        WWWForm form = new WWWForm();
        form.AddField("updateUserID", userID);
        form.AddField("diamondAmout", amount);

        using (UnityWebRequest www = UnityWebRequest.Post(getDiamondsURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                this.OnWebResult?.Invoke(false, www.error);
            }
            else
            {
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

                this.OnWebResult?.Invoke(true, response.message);

                if(response.success)
                    this.OnDiamondsUpdated?.Invoke(response.diamonds);
            }
        }
    }

    public void ChangeHeartValue(int userID, int amount)
    {
        StartCoroutine(RequestChangeHeartValue(userID, amount));
    }

    IEnumerator RequestChangeHeartValue(int userID, int amount)
    {
        WWWForm form = new WWWForm();
        form.AddField("updateUserID", userID);
        form.AddField("heartAmout", amount);

        using (UnityWebRequest www = UnityWebRequest.Post(getHeartURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                this.OnWebResult?.Invoke(false, www.error);
            }
            else
            {
                Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

                this.OnWebResult?.Invoke(true, response.message);

                if(response.success)
                    this.OnHeartsUpdated?.Invoke(response.hearts);
            }
        }
    }
}

[System.Serializable]
public class Response
{
    public bool success;
    public string message;
    public UserData data;
    public int diamonds;
    public int hearts;
}