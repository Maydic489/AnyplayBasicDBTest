using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [Header("Player Info")]
    public LocalUserData localData;
    BackendRequest backend;

    public static LobbyManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        backend = BackendRequest.Instance;
        EventsSetup();
    }

    void EventsSetup()
    {
        backend.OnLoginSuccess.AddListener(SetLocalUserData);
        backend.OnDiamondsUpdated.AddListener(UpdateDiamond);
        backend.OnHeartsUpdated.AddListener(UpdateHeart);
    }

    void SetLocalUserData(UserData userData)
    {
        localData.userData = userData;
    }

    void UpdateDiamond(int diamonds)
    {
        localData.userData.diamonds = diamonds;
    }

    void UpdateHeart(int hearts)
    {
        localData.userData.hearts = hearts;
    }
}
