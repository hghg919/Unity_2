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
        // ���� ��Ƽ �Լ����� Amazon.Initialize();
        // Player InputSystem. Enable

        await WaitForSecondsAsync(2);
    }

    private async UniTask CreateObjects()
    {
        _player = Instantiate(_player);
        _levelUI = Instantiate(_levelUI);
        //_enemySpawner. ���͸� n���� �����϶�
        await WaitForSecondsAsync(2);
    }
     
    private async UniTask PrepareGame()
    {
        //player�� ���� ��ġ ���� SetPlayer
        //player�� ���� ���� ���� SetWeapon
        //player   Set~~

        //_enemySpawner. �÷��̾� �ֺ����� �̵���Ų��.

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
        // ���ܳ��� ���͵��� �ٽ� ���̰��ض�
    }


    private async UniTask WaitForSecondsAsync(float seconds)
    {
        await UniTask.Delay((int)(seconds * 1000));
    }
}
