using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private Light _mainLight;
    [SerializeField] private T_EnemySpawner _enemySpawner;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private T_LevelManager _levelManager;
    [SerializeField] private T_LevelUI _levelUI;
    [SerializeField] private T_Loading _loading;
    [SerializeField] private Camera _camera;
    [SerializeField] private T_Player _player;

    private async void Start()
    {
        BindObjects();

        using (var loadingScreenDispoable = new ShowLoadingScreenDisposable(_loading))
        {
            loadingScreenDispoable.SetLoadingPercent(0);
            await InitializeObjects();
            loadingScreenDispoable.SetLoadingPercent(0.33f);
            await CreateObjects();
            loadingScreenDispoable.SetLoadingPercent(0.66f);
            await PrepareGame();
            loadingScreenDispoable.SetLoadingPercent(1f);
        }

        BeginGame();
    }

    private async UniTask InitializeObjects()
    {
        // 서드 파티 함수들을 Amazon.Initialize();
        // Player InputSystem. Enable

        await WaitForSecondsAsync(2);
    }

    private async UniTask CreateObjects()
    {
        _player = Instantiate(_player);
        _levelUI = Instantiate(_levelUI);
        //_enemySpawner. 몬스터를 n마리 생성하라
        await WaitForSecondsAsync(2);
    }
     
    private async UniTask PrepareGame()
    {
        //player의 시작 위치 세팅 SetPlayer
        //player의 시작 무기 세팅 SetWeapon
        //player   Set~~

        //_enemySpawner. 플레이어 주변으로 이동시킨다.

        await WaitForSecondsAsync(2);
    }

    private void BindObjects()
    {
        _mainLight = Instantiate(_mainLight);
        _enemySpawner = Instantiate(_enemySpawner);
        _eventSystem = Instantiate(_eventSystem);
        _levelManager = Instantiate(_levelManager);      
        _loading = Instantiate(_loading);
        _camera = Instantiate(_camera);
       
    }
    
    private void BeginGame()
    {
        // 숨겨놓은 몬스터들을 다시 보이게해라
    }


    private async UniTask WaitForSecondsAsync(float seconds)
    {
        await UniTask.Delay((int)(seconds * 1000));
    }
}
