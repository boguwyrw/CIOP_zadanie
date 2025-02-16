using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

namespace ciop.task
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        [SerializeField] private GameObject restartPanel;

        [SerializeField] private TMP_Text playerChancesText;
        [SerializeField] private TMP_Text measurementResultsText;

        private int playerChances = 3;
        private int showResultsTime = 3;

        private bool isGameEnded = false;

        public bool IsGameEnded { get { return isGameEnded; } }

        private void Start()
        {
            restartPanel.SetActive(false);
            measurementResultsText.text = "";
            ShowPlayerChances();
        }

        private void ShowPlayerChances()
        {
            playerChancesText.text = playerChances.ToString();
        }

        private IEnumerator CoClearResult()
        {
            yield return new WaitForSeconds(showResultsTime);
            measurementResultsText.text = "";
        }

        public void PlayerLoseChance()
        {
            playerChances--;
            ShowPlayerChances();
            if (playerChances == 0)
            {
                isGameEnded = true;
                restartPanel.SetActive(true);
            }
        }

        public void ShowMeasurementResults(string result)
        {
            measurementResultsText.text = result;
            StartCoroutine(CoClearResult());
        }

        public void RestartGame()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex);
        }

        public bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}