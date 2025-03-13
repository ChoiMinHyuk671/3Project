using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class JoinRoomScreenUI : ScreenUICore, INetworkRunnerCallbacks
{
    public NetworkRunner _runner;
    public Transform sessionListParent;  // 세션 목록 UI 부모 오브젝트
    public GameObject sessionItemPrefab; // 세션 UI 프리팹
    private float timer;
    private int sessionPoolSize;

    private Queue<GameObject> sessionPool = new Queue<GameObject>(); // 오브젝트 풀
    private List<GameObject> activeSessions = new List<GameObject>(); // 활성화된 세션 목록

    private void Awake()
    {
        for (int i = 0; i < sessionPoolSize; i++)
        {
            GameObject sessionButton = Instantiate(sessionItemPrefab, transform).transform.sessionListParent = sessionListParent.transform;
            sessionButton.SetActive(false);
            pool.Enqueue(sessionButton);
        }
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        UpdateSessionUI(sessionList);
    }

    private void UpdateSessionUI(List<SessionInfo> sessionList)
    {
        
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
}