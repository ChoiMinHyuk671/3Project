using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;

    public GameMode _gameMode;

    [SerializeField] RoomPlayer roomPlayerPrefab;

    public void SetCreateLobby() => _gameMode = GameMode.Host;

	public void SetJoinLobby() => _gameMode = GameMode.Client;

    void Awake()
    {
        ServerInfo.maxPlayers = 2; 
        ServerInfo.sessionName = "Test1";
        ServerInfo.sessionName = "Player2";
        JoinOrCreateLobby();
    }

    public void JoinOrCreateLobby()
	{
		GameObject go = new GameObject("Session");
		DontDestroyOnLoad(go);

		_runner = go.AddComponent<NetworkRunner>();

		_runner.ProvideInput = _gameMode != GameMode.Server;
		_runner.AddCallbacks(this);

		_runner.StartGame(new StartGameArgs
		{
			GameMode = _gameMode,
			SessionName = ServerInfo.sessionName,
			SceneManager = SceneController.Instance,
			PlayerCount = ServerInfo.maxPlayers,
			EnableClientSessionCreation = false
		});
	}

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
	{
		if (runner.IsServer)
		{
			var roomPlayer = runner.Spawn(roomPlayerPrefab, Vector3.zero, Quaternion.identity, player);
            SceneController.SceneTransition(SceneType.InGame);
		}
	}
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
}
