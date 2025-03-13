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
        Debug.Log($"{currentTrun}");
        Debug.Log($"실행2");
        if(RoomPlayer.Players[currentTrun].playerTrunState == PlayerTrunState.EndTrun && RoomPlayer.Players[currentTrun] == player)
        {
            Debug.Log($"System : {player.playerName}가 턴을 종료 하였습니다");
            player.playerTrunState = PlayerTrunState.Wait; //턴을 대기 상태로 변경
        }
        else
        {
            Debug.Log($"System : 턴이 아닌 Player가 턴을 제어하려 했습니다 ({player.playerName})");
            return;
        }

        int nextTurn = RoomPlayer.Players.IndexOf(player) + 1;
        //테이블 회전, 총알 개수 확인
        // 순서 총알 확인 > 턴의 변경 여부 확인(플레이어가 공포탄인 경우는 턴을 그대로 유지이기에 추가 메커니즘 불필요) > 턴 변경

        if(nextTurn >= ServerInfo.maxPlayers)
        {
            nextTurn = 0;
            Debug.Log($"System : 최대 Player를 초과하여 턴을 {nextTurn}으로 변경합니다)");
        }

        RoomPlayer.Players[nextTurn].playerTrunState = PlayerTrunState.IsTrun;
        currentTrun = nextTurn;
        // 변경으로 무기 회전
        
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
