using Core.Hero;
using Core.Hud;
using Services;

namespace Core.EndGame
{
    public class EndGameChecker
    {
        private const string ProgressName = "PlayerProgress";

        private readonly EndGameWindow _endGameWindow;
        private readonly GameScore _gameScore;
        private readonly PlayerProgress _playerProgress;
        private readonly JsonSaver _jsonSaver;

        public EndGameChecker(EndGameWindow endGameWindow, HeroDeath heroDeath, GameScore gameScore,  PlayerProgress playerProgress, JsonSaver jsonSaver)
        {
            _endGameWindow = endGameWindow;
            _gameScore = gameScore;
            _playerProgress = playerProgress;
            _jsonSaver = jsonSaver;

            heroDeath.HeroDied += EndGame;
        }

        private void EndGame()
        {
            if (_gameScore.Score > _playerProgress.BestResult)
                SaveProgress(_gameScore.Score);

            ShowEndGameWindow();
        }

        private void SaveProgress(int newScore)
        {
            _playerProgress.BestResult = newScore;
            _jsonSaver.Save(ProgressName, _playerProgress);
        }

        private void ShowEndGameWindow() =>
            _endGameWindow.Show();
    }
}
