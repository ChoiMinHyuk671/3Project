using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public enum PlayerTrunState
{
	Wait = 0,
	IsTrun = 1,
	EndTrun = 2
}
public class RoomPlayer : NetworkBehaviour
{
	// onValueChanged.AddListener(x =>
	// 	{
	// 		ServerInfo.sessionName = x;
	// 	});
    public static readonly List<RoomPlayer> Players = new List<RoomPlayer>();
    public static RoomPlayer local;
	[Networked] public PlayerTrunState playerTrunState{get; set;}
    private ChangeDetector _changeDetector;
	public PlayerController m_PlayerController;
    [Networked] public NetworkString<_16> playerName { get; set; }
    public override void Spawned()
	{
		base.Spawned();
		
		_changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

		if (Object.HasInputAuthority)
		{
			local = this;
			RPC_SetPlayerStats(ServerInfo.sessionName);
		}

		Players.Add(this);

		DontDestroyOnLoad(gameObject);
	}

    public override void Render()
	{
		foreach (var change in _changeDetector.DetectChanges(this))
		{
			switch (change)
			{
				case nameof(playerName):
					break;
			}
		}
	}

	[Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    private void RPC_SetPlayerStats(NetworkString<_16> _playerName)
	{
		playerName = _playerName;
	}

}
