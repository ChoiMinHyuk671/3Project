using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
/// <summary>
/// 게임의 턴 단계를 정의하는 열거형.
/// </summary>
public enum TurnType
{
    PreTurnPhase = 0,
    TurnStart = 1,
    ActionPhase = 2,
    TurnEnd = 3
}

public class NetworkTurnManager : NetworkBehaviour
{
    [Networked]public int currentTrun {get; set;}
    [Networked] public TurnType turnType{get; set;}
    public override void Spawned()
    {
        base.Spawned();

        if (Runner.IsServer)
        {
            currentTrun = 0;
            RoomPlayer.Players[currentTrun].playerTrunState = PlayerTrunState.IsTrun;
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_NextTurn(RoomPlayer player) //플레이어 턴 전환
    {
        if(RoomPlayer.Players[currentTrun].playerTrunState != PlayerTrunState.EndTrun)
            return;
        if(RoomPlayer.Players[currentTrun] != player)
            return;

        int nextTurn = RoomPlayer.Players.IndexOf(player) + 1;
        player.playerTrunState = PlayerTrunState.Wait; //턴을 대기 상태로 변경

        if(nextTurn >= ServerInfo.maxPlayers)
        {
            nextTurn = 0;
            Debug.Log($"System : 최대 Player를 초과하여 턴을 {nextTurn}으로 변경합니다)");
        }

        RoomPlayer.Players[nextTurn].playerTrunState = PlayerTrunState.IsTrun;
        currentTrun = nextTurn;
    }

    public void TestNextTrun()
    {
        RPC_NextTurn(RoomPlayer.local);
    }

    void Update()
    {
        Debug.Log($"{currentTrun}");
    }

}
