using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerController : PlayerComponent, IStats, IDamageable
{
    [Networked] public RoomPlayer roomPlayer {get; set;}
    [Networked] public float hp {get; set;}
    [Networked] public float maxHp {get; set;}
    public event Action OnDamage;
    private ChangeDetector _changeDetector {get; set;}
    public override void Spawned()
	{
		base.Spawned();
		_changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
	}
    public override void Render()
	{
		foreach (var change in _changeDetector.DetectChanges(this))
		{
			switch (change)
			{
				case nameof(hp):
                case nameof(maxHp):
					break;
			}
		}
	}
    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)] //호출 권한은 All, 타켓은 서버로  Hp의 변경을 위한 함수
    public void RPC_TakeDamage(float damage)
    {
        hp -= damage;
    }
}
