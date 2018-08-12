using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace LD42
{
    public class GameOverUI : MonoBehaviour
    {
        public TextMeshProUGUI TotalScoreText;
        public TextMeshProUGUI DeliveredText;
        public TextMeshProUGUI FailedText;
        public TextMeshProUGUI BombExplodedText;
        public TextMeshProUGUI BombDeliveredText;
        public TextMeshProUGUI UnwantedText;
        public TextMeshProUGUI NotInspectedText;

        private void Awake()
        {
            this.UpdateUI();
        }

        private void OnEnable()
        {
            this.UpdateUI();
        }

        private void UpdateUI()
        {
            GameManager gm = GameObject.Find("Manager").GetComponent<GameManager>();
            this.TotalScoreText.text = gm.Score.ToString();
            this.DeliveredText.text = gm.Stats["Delivered"].ToString();
            this.FailedText.text = gm.Stats["Failed"].ToString();
            this.BombExplodedText.text = gm.Stats["BombExploded"].ToString();
            this.BombDeliveredText.text = gm.Stats["BombDelivered"].ToString();
            this.UnwantedText.text = gm.Stats["Unwanted"].ToString();
            this.NotInspectedText.text = gm.Stats["NotInspected"].ToString();
        }
    }
}