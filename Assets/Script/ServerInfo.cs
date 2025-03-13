using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServerInfo
{
    public static string sessionName {
        get => PlayerPrefs.GetString("SessionName", "");
        set => PlayerPrefs.SetString("SessionName", value);
    }
    public static int maxPlayers {
        get => PlayerPrefs.GetInt("MaxPlayers", 4);
        set => PlayerPrefs.SetInt("MaxPlayers", Mathf.Clamp(value, 1, 4));
    }
    public static string playerName {
        get => PlayerPrefs.GetString("PlayerName", string.Empty);
        set => PlayerPrefs.SetString("PlayerName", value);
    }
}
