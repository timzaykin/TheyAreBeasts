using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB { 
    public class GameManager : Singleton<GameManager> {

        public delegate void StartGame();
        public event StartGame onStart;

        public GameState CurrentGameState = GameState.Preparation;

        public int score = 0;

        public enum GameState
        {
            Preparation,
            Started,
            Paused,
            Ended
        }

        private void OnEnable()
        {
            onStart += ChangeToStartGameState;
        }

        private void Start()
        {
            onStart();
            
        }

        private void ChangeToStartGameState() {
            CurrentGameState = GameState.Started;
        }

        private void OnDisable()
        {
            onStart -= ChangeToStartGameState;
        }
    }
}
