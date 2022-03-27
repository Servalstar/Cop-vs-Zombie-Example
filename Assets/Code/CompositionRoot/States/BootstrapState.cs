using CompositionRoot.SceneLoad;
using Services;
using Services.AssetManagement;
using Services.Input;
using System.Threading.Tasks;
using UnityEngine;

namespace CompositionRoot.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly Locator _locator;
        private readonly SceneLoader _sceneLoader;
        private AssetProvider _assetProvider;
        private GameObject _uiRoot;

        public BootstrapState(GameStateMachine stateMachine, Locator locator, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _locator = locator;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            RegisterAssetProvider();
            RegisterCommonServices();

            await CreateBaseUI();

            _stateMachine.Enter<MenuState>();
        }

        private void RegisterAssetProvider()
        {
            _assetProvider = new AssetProvider();
            _locator.RegisterSingle(_assetProvider);
            _assetProvider.Initialize();
        }

        private async Task CreateBaseUI()
        {
            _uiRoot = await CreateUIRoot();
            await CreateLoadingScreen();
        }

        private void RegisterCommonServices()
        {
            _locator.RegisterSingle(_stateMachine);
            _locator.RegisterSingle(new JsonSaver());
            _locator.RegisterSingle(_sceneLoader);
            _locator.RegisterSingle(LoadPlayerProgress());
            _locator.RegisterSingle(GetInput());
        }

        private PlayerProgress LoadPlayerProgress()
        {
            var saver = _locator.GetSingle<JsonSaver>();
            var progress = saver.Load<PlayerProgress>("PlayerProgress");

            if (progress == null)
            {
                progress = new PlayerProgress();
                saver.Save("PlayerProgress", progress);
            }

            return progress;
        }

        private async Task<GameObject> CreateUIRoot() =>
            await _assetProvider.Instantiate(AssetAddress.UIRoot);

        private async Task CreateLoadingScreen() =>
            await _assetProvider.Instantiate(AssetAddress.LoadingScreen, _uiRoot);

        private IInputService GetInput() =>
            Application.isEditor ? (IInputService)new StandaloneInputService() : new MobileInputService();
    }
}