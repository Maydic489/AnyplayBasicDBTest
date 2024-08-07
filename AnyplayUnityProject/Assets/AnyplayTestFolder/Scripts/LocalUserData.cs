using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalUserData : MonoBehaviour
{
    public UserData userData;
}

[System.Serializable]
public class UserData
{
    public int id;
    public string username;
    public string password;
    public int diamonds;
    public int hearts;
}
