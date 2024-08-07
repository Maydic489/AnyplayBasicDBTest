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
            //TODO: message popup instead
            Debug.Log("The username or password is empty");
        }
    }
}
