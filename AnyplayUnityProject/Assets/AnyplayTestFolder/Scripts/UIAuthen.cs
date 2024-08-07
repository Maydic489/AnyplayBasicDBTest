using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIAuthen : MonoBehaviour
{
    [SerializeField] BackendRequest backend;
    [SerializeField] GameObject loginPanel;
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] Button loginButton;
    [SerializeField] Button signupPanelButton;
    [SerializeField] GameObject signupPanel;
    [SerializeField] TMP_InputField signupUsernameInput;
    [SerializeField] TMP_InputField signupPasswordInput;
    [SerializeField] TMP_InputField confirmPasswordInput;
    [SerializeField] Button signupButton;

    [SerializeField] GameObject popUpMessagePanel;
    [SerializeField] TMP_Text popUpText;

    private void Start()
    {
        if(BackendRequest.Instance != null)
        {
            backend = BackendRequest.Instance;
            backend.OnWebResult.AddListener(ShowPopUpMessage);
            backend.OnSighUpSuccess.AddListener(ShowLoginPanel);
        }
    }

    public void Login()
    {
        if (backend == null)
            return;

        if(usernameInput.text != "" && passwordInput.text != "")
        {
            backend.RequestLogin(usernameInput.text, passwordInput.text);
        }
        else
        {
            ShowPopUpMessage("The username or password is empty");
        }
    }

    public void SignUp()
    {
        if (backend == null)
            return;

        if(signupUsernameInput.text != "" && signupPasswordInput.text != "" && confirmPasswordInput.text != "")
        {
            if(signupPasswordInput.text == confirmPasswordInput.text)
            {
                backend.RequestSignUp(signupUsernameInput.text, signupPasswordInput.text);
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

    public void ShowPopUpMessage(string message)
    {
        popUpMessagePanel.SetActive(true);
        popUpText.text = message;
    }
}
