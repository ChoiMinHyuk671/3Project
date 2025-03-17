using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerEntity : PlayerComponent //필요한 컴포넌트의 집합체 (블랙보드)
{
	[SerializeField] protected Animator _Animator;
	//[SerializeField] protected InputHandler _InputHandler;
	[SerializeField] protected PlayerController _PlayerController;
	[SerializeField] protected PlayerCamera _PlayerCamera;
	public Animator m_Animator { get => _Animator; protected set => _Animator = value; }
	public PlayerController m_PlayerController { get => _PlayerController; protected set => _PlayerController = value; }
	//public InputHandler m_InputHandler { get => _InputHandler; protected set => _InputHandler = value; }
	public PlayerCamera m_PlayerCamera { get => _PlayerCamera; protected set => _PlayerCamera = value; }
	public static readonly List<PlayerEntity> players = new List<PlayerEntity>();
	//public ChangeDetector _changeDetector;

    private void Awake()
	{
		m_Animator = GetComponent<Animator>();
		m_PlayerController = GetComponent<PlayerController>();
		// m_InputHandler = GetComponent<InputHandler>();
		m_PlayerCamera = GetComponent<PlayerCamera>();

		var components = GetComponentsInChildren<PlayerComponent>();

		//PlayerEntity를 넣어주면서 이를 활용하여 컨트롤러, 카메라 등에 접근 권한을 부여
		foreach (var component in components) component.Initialize(this);
	}

	public override void Spawned()
	{
		base.Spawned();
		
		//_changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
		
		players.Add(this);
	}
}
