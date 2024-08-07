using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] BackendRequest backend;
    [SerializeField] GameObject loginPanel;
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] Button loginButton;
    [SerializeField] Button signupPanelButton;

    [Header("SignUP")]
    [SerializeField] GameObject signupPanel;
    [SerializeField] TMP_InputField signupUsernameInput;
    [SerializeField] TMP_InputField signupPasswordInput;
    [SerializeField] TMP_InputField confirmPasswordInput;
    [SerializeField] Button signupButton;

    [Header("Popup message")]
    [SerializeField] GameObject popUpMessagePanel;
    [SerializeField] TMP_Text popUpText;

    [Header("Lobby")]
    [SerializeField] GameObject lobbyGroup;

    [Header("Player Info")]
    [SerializeField] LocalUserData localData;

    private void Start()
    {
        if(BackendRequest.Instance != null)
        {
            backend = BackendRequest.Instance;
            backend.OnWebResult.AddListener(ShowPopUpMessage);
            backend.OnSighUpSuccess.AddListener(ShowLoginPanel);
            backend.OnWebResult.AddListener(ShowLobby);
        }

        //string json = "{\"id\":1,\"username\":\"test01\",\"password\":\"1234\",\"user_id\":1,\"diamonds\":0,\"hearts\":0}";
        //PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(json);

        //Debug.Log("Player ID: " + playerInfo.id);
        //Debug.Log("Username: " + playerInfo.username);
        //Debug.Log("Password: " + playerInfo.password);
        //Debug.Log("User ID: " + playerInfo.user_id);
        //Debug.Log("Diamonds: " + playerInfo.diamonds);
        //Debug.Log("Hearts: " + playerInfo.hearts);
    }

    public void OnClickLogin()
    {
        if (backend == null)
            return;

        if(usernameInput.text != "" && passwordInput.text != "")
        {
            backend.Login(usernameInput.text, passwordInput.text);
        }
        else
        {
            ShowPopUpMessage("The username or password is empty");
        }
    }

    public void OnClickSignUp()
    {
        if (backend == null)
            return;

        if(signupUsernameInput.text != "" && signupPasswordInput.text != "" && confirmPasswordInput.text != "")
        {
            if(signupPasswordInput.text == confirmPasswordInput.text)
            {
                backend.SignUp(signupUsernameInput.text, signupPasswordInput.text);
            }
            else
            {
                ShowPopUpMessage("Password and Confirm Password are not the same");
            }
        }
    }

    public void ShowLoginPanel()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);

        usernameInput.text = "";
        passwordInput.text = "";
    }

    public void ShowSignUpPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);

        signupUsernameInput.text = "";
        signupPasswordInput.text = "";
        confirmPasswordInput.text = "";
    }

    void ShowLobby(string userData)
    {
        lobbyGroup.SetActive(true);
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);

        userData = userData.Remove(0, 18);
        Debug.Log(userData);
        localData.userData = JsonUtility.FromJson<UserData>(userData);
    }

    public void ShowPopUpMessage(string message)
    {
        popUpMessagePanel.SetActive(true);
        popUpText.text = message;
    }
}
