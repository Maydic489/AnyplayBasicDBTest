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
    [SerializeField] Image heartFill;
    [SerializeField] TMP_Text diamondText;

    [Header("Player Info")]
    [SerializeField] LocalUserData localData;

    private void Start()
    {
        if(BackendRequest.Instance != null)
        {
            backend = BackendRequest.Instance;
            backend.OnWebResult.AddListener(ShowPopUpMessage);
            backend.OnSighUpSuccess.AddListener(ShowLoginPanel);
            backend.OnLoginSuccess.AddListener(SetLocalUserData);
            backend.OnLoginSuccess.AddListener(ShowLobby);
            backend.OnDiamondsUpdated.AddListener(UpdateDiamond);
        }
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

    void SetLocalUserData(UserData userData)
    {
        localData.userData = userData;
    }

    void ShowLobby(UserData userData)
    {
        lobbyGroup.SetActive(true);
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);

        SetLobby();
    }

    void SetLobby()
    {
        diamondText.text = localData.userData.diamonds.ToString();
        heartFill.fillAmount = localData.userData.hearts / 100;
    }

    void UpdateDiamond(int amount)
    {
        diamondText.text = amount.ToString();
    }

    public void OnClickIncreaseDiamond()
    {
        backend.GetMoreDiamonds(localData.userData.id, 100);
    }

    public void ShowPopUpMessage(string message)
    {
        popUpMessagePanel.SetActive(true);
        popUpText.text = message;
    }
}
