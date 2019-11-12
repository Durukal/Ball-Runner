using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ColectCoin : MonoBehaviour
    {
        public int ScoreValue;
        public GUIText ScoreText;
        private int _score;

        [UsedImplicitly]
        private void Start()
        {
            _score = 0;
            UpdateScore();
        }

        [UsedImplicitly]
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                other.gameObject.SetActive(false);
            }
            Destroy(other.gameObject);

            BallTimer.DeathTime += BallTimer.CoinTimeBonus;
            AddScore(ScoreValue);

            if(other.gameObject.CompareTag("Obstacles"))
            {
                SceneManager.LoadScene("Ball Runner", LoadSceneMode.Single);
            }
        }

        public void AddScore(int newScoreValue)
        {
            _score+=newScoreValue;
            UpdateScore();
        }

        private void UpdateScore()
        {
            ScoreText.text = "Score:" + _score;
        }
    }
}
