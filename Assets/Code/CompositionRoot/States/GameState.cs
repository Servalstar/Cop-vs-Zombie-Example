using CompositionRoot.SceneLoad;
using Services;
using Services.AssetManagement;
using Services.Input;
using System.Threading.Tasks;
using UnityEngine;
using Core.Hero;
using Core.EnemyCreation;
using Core.Common;
using Core.Hud;
using Core.EndGame;

namespace CompositionRoot.States
{
    public class GameState : IExitableState
    {
        private readonly Locator _locator;
        private SceneLoader _sceneLoader;
        private AssetProvider _assetProvider;
        private GameStateMachine _stateMachine;
        private JsonSaver _jsonSaver;
        private PlayerProgress _playerProgress;

        public GameState(Locator locator) =>
            _locator = locator;

        public async void Enter()
        {
            GetDependencies();

            await WarmUpAssets();

            _sceneLoader.Load(SceneNames.Game, InitGameWorld);
        }

        public void Exit() =>
            _assetProvider.Cleanup();

        private void GetDependencies()
        {
            _sceneLoader = _locator.GetSingle<SceneLoader>();
            _assetProvider = _locator.GetSingle<AssetProvider>();
            _stateMachine = _locator.GetSingle<GameStateMachine>();
            _playerProgress = _locator.GetSingle<PlayerProgress>();
            _jsonSaver = _locator.GetSingle<JsonSaver>();
        }

        private async Task WarmUpAssets()
        {
            await _assetProvider.Load<GameObject>(AssetAddress.UIRoot);
            await _assetProvider.Load<GameObject>(AssetAddress.Hud);
            await _assetProvider.Load<GameObject>(AssetAddress.EndGameWindow);
            await _assetProvider.Load<GameObject>(AssetAddress.Hero);
            await _assetProvider.Load<GameObject>(AssetAddress.Enemy);
            await _assetProvider.Load<GameObject>(AssetAddress.EnemyCreator);
            await _assetProvider.Load<SpawnData>(AssetAddress.SpawnData);
        }

        private async void InitGameWorld()
        {
            var uiRoot = await CreateUIRoot();
            var spawnData = await GetSpawnData();
            var hero = await CreateHero(spawnData);
            var pool = await CreateEnemies(hero, spawnData);
            var gameScore = await CreateHud(uiRoot, hero, pool);

            CreateEndGame(uiRoot, hero, gameScore);
            SetCameraTarget(hero);
        }

        private async Task<GameObject> CreateUIRoot()
        {
            var uiRoot = await _assetProvider.Instantiate(AssetAddress.UIRoot);

            return uiRoot;
        }

        private async Task<SpawnData> GetSpawnData()
        {
            var spawnData = await _assetProvider.Load<SpawnData>(AssetAddress.SpawnData);

            return spawnData;
        }

        private async Task<GameObject> CreateHero(SpawnData spawnData)
        {
            var hero = await _assetProvider.Instantiate(AssetAddress.Hero);
            hero.transform.position = spawnData.SpawnHeroPosition;
            hero.GetComponent<HeroMover>().Construct(_locator.GetSingle<IInputService>());
            hero.SetActive(true);

            return hero;
        }

        private async Task<EnemyPool> CreateEnemies(GameObject hero, SpawnData spawnData)
        {
            var enemyPrefab = await _assetProvider.Load<GameObject>(AssetAddress.Enemy);

            var factory = new EnemyFactory(enemyPrefab, hero);
            var pool = new EnemyPool(factory);

            var enemyCreator = await _assetProvider.Instantiate(AssetAddress.EnemyCreator);
            enemyCreator.GetComponent<EnemyCreator>().Construct(pool, spawnData.SpawnEnemiesPosition, spawnData.SpawnDelay);

            return pool;
        }

        private async Task<GameScore> CreateHud(GameObject uiRoot, GameObject hero, EnemyPool pool)
        {
            var hud = await _assetProvider.Instantiate(AssetAddress.Hud, uiRoot);

            var joystick = hud.GetComponentInChildren<Joystick>();
            var input = _locator.GetSingle<IInputService>();
            input.SetJoystick(joystick);

            var heroHealth = hero.GetComponent<IHealth>();
            hud.GetComponentInChildren<HeroHealthBar>().Construct(heroHealth);

            var gameScore = hud.GetComponentInChildren<GameScore>();
            gameScore.Construct(pool);

            return gameScore;
        }

        private async void CreateEndGame(GameObject uiRoot, GameObject hero, GameScore gameScore)
        {
            var endGameWindowObject = await _assetProvider.Instantiate(AssetAddress.EndGameWindow, uiRoot);
            var endGameWindow = endGameWindowObject.GetComponentInChildren<EndGameWindow>();
            endGameWindow.Construct(_stateMachine);

            var heroDeath = hero.GetComponent<HeroDeath>();
            new EndGameChecker(endGameWindow, heroDeath, gameScore, _playerProgress, _jsonSaver);
        }

        private void SetCameraTarget(GameObject hero) =>
            Camera.main.GetComponent<CameraMover>().Follow(hero);
    }
}