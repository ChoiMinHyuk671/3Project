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
	public LayerMask target; // 감지할 레이어 설정 (인스펙터에서 설정 가능)
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

	public override void FixedUpdateNetwork()
	{
		// if(Runner.IsServer)
		// 	return;
		if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        	RaycastHit hit;
			 if (Physics.Raycast(ray, out hit, 100f, target))
        	{
			Debug.Log($"[{hit.collider.gameObject.name}]이(가) 선택한 레이어에 감지됨");
           	hit.collider.GetComponent<IDamageable>().RPC_TakeDamage(10f);
        	}
		}
	}
    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)] //호출 권한은 All, 타켓은 서버로  Hp의 변경을 위한 함수
    public void RPC_TakeDamage(float damage)
    {
        hp -= damage;
    }
}
