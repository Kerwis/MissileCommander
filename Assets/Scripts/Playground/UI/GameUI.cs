using System;
using TMPro;
using UnityEngine;

namespace Playground.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _score;

        [SerializeField]
        private TextMeshProUGUI _leftTowerStatus;

        [SerializeField]
        private TextMeshProUGUI _middleTowerStatus;

        [SerializeField]
        private TextMeshProUGUI _rightTowerStatus;
        
        [SerializeField]
        private GameObject _gameOver;

        public TextMeshProUGUI Score => _score;
        public TextMeshProUGUI LeftTowerStatus => _leftTowerStatus;
        public TextMeshProUGUI MiddleTowerStatus => _middleTowerStatus;
        public TextMeshProUGUI RightTowerStatus => _rightTowerStatus;

        public void GameOver()
        {
            _gameOver.SetActive(true);
        }
    }
}
