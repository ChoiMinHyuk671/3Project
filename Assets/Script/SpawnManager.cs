using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpawnManager : NetworkBehaviour
{
    public Transform[] spawnPoints; // 스폰 위치를 저장
    public PlayerEntity playerPrefab;
    public RoomPlayer roomPlayer;
    public static SpawnManager Instance { get; private set; }

    public override void Spawned()
    {
        base.Spawned();

        if (Runner.IsServer)
        {
            foreach (var player in RoomPlayer.Players)
            {
                PlayerSpawn(Runner, player);
            }
        }
    }

    public void PlayerSpawn(NetworkRunner runner, RoomPlayer player)
    {
        var index = RoomPlayer.Players.IndexOf(player);
		var point = spawnPoints[index];

        var entity = runner.Spawn(
            playerPrefab,
            point.position,
            point.rotation,
            player.Object.InputAuthority
        );

        if (entity != null)
        {
            entity.m_PlayerController.roomPlayer = player;
            player.m_PlayerController = entity.m_PlayerController;
        }
    }
}
