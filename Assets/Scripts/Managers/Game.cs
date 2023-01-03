using System;
using System.Linq;
using Data;
using Playground;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
	public class Game : MonoBehaviour
	{
		[SerializeField]
		private GameData _data;
		
		private static Game _instance;
		
		private static Game Instance
		{
			get
			{
				if (_instance == null)
					Scenes.LoadInit();

				if (_instance == null)
					_instance = CreateDump();

				return _instance;
			}
		}

		public static void RegisterBattleGround(BattleGround battleGround)
		{
			Instance.InternalRegisterBattleGround(battleGround);
		}

		private void Awake()
		{
			if(_data == null)
				return;
			_instance = this;
			DontDestroyOnLoad(_instance);
			Setup();
			LoadNextScene();
		}

		private void LoadNextScene()
		{
			Scenes.LoadSceneAfterInit();
		}

		private void Setup()
		{
			Screen.SetResolution(640, 480, true);
			_data.ActionMap.FindAction("ExitToMenu").performed += ExitToMenu;
			_data.ActionMap.Enable();
		}

		private void InternalRegisterBattleGround(BattleGround battleGround)
		{
			if(_data == null)
				return;
			battleGround.Setup(_data);
		}
		
		
		private void ExitToMenu(InputAction.CallbackContext obj)
		{
			Scenes.LoadScene(Scenes.ScenesNames.Menu);
		}

		private static Game CreateDump()
		{
			var game = new GameObject().AddComponent<Game>();
			return game;
		}

		private void OnDestroy()
		{
			if(_data!=null)
				_data.ActionMap.FindAction("ExitToMenu").performed -= ExitToMenu;
		}
	}
}