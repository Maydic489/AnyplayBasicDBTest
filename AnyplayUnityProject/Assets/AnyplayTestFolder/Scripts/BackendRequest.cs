using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class BackendRequest : MonoBehaviour
{
    public UnityEvent<string> OnWebResult;
    public UnityEvent OnSighUpSuccess;

    string loginURL = "https://test-piggy.codedefeat.com/worktest/dev01/gameBackend/login.php";
    string signupURL = "https://test-piggy.codedefeat.com/worktest/dev01/gameBackend/signup.php";

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
            OnWebResult = new UnityEvent<string>();
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

    public void RequestLogin(string username, string password)
    {
        StartCoroutine(Login(username, password));
    }

    IEnumerator Login(string username, string password)
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
                this.OnWebResult?.Invoke(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                this.OnWebResult?.Invoke(www.downloadHandler.text);
            }
        }
    }

    public void RequestSignUp(string username, string password)
    {
        StartCoroutine(SignUp(username, password));
    }

    IEnumerator SignUp(string username, string password)
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
                this.OnWebResult?.Invoke(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                this.OnWebResult?.Invoke(www.downloadHandler.text);
                this.OnSighUpSuccess?.Invoke();
            }
        }
    }
}