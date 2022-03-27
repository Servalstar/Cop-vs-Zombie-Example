using CompositionRoot.SceneLoad;
using Menu;
using Services;
using Services.AssetManagement;
using System.Threading.Tasks;
using UnityEngine;

namespace CompositionRoot.States
{
    public class MenuState : IExitableState
    {
        private readonly Locator _locator;
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private AssetProvider _assetProvider;
        private PlayerProgress _playerProgress;

        public MenuState(Locator locator) =>
            _locator = locator;

        public async void Enter()
        {
            GetDependencies();

            await WarmUpMenuAssets();

            _sceneLoader.Load(SceneNames.Menu, CreateMenu);
        }

        public void Exit() =>
            _assetProvider.Cleanup();

        private void GetDependencies()
        {
            _stateMachine = _locator.GetSingle<GameStateMachine>();
            _sceneLoader = _locator.GetSingle<SceneLoader>();
            _assetProvider = _locator.GetSingle<AssetProvider>();
            _playerProgress = _locator.GetSingle<PlayerProgress>();
        }

        private async Task WarmUpMenuAssets()
        {
            await _assetProvider.Load<GameObject>(AssetAddress.UIRoot);
            await _assetProvider.Load<GameObject>(AssetAddress.MainMenuScreen);
        }

        private async void CreateMenu()
        {
            var uiRoot = await _assetProvider.Instantiate(AssetAddress.UIRoot);
            var screen = await _assetProvider.Instantiate(AssetAddress.MainMenuScreen, uiRoot);

            ConfigureCanvas(uiRoot);

            screen.GetComponentInChildren<BattleButton>().Construct(_stateMachine);
            screen.GetComponentInChildren<MenuScore>().Construct(_playerProgress);
        }

        private void ConfigureCanvas(GameObject uiRoot)
        {
            var canvas = uiRoot.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
        }
    }
}