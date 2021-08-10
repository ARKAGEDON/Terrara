using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MirrorBasics {

    public class UILobby : MonoBehaviour {

        public static UILobby instance;

        [Header ("Host Join")]
        [SerializeField] InputField joinMatchInput;
        [SerializeField] List<Selectable> lobbySelectables = new List<Selectable> ();
        [SerializeField] Canvas lobbyCanvas;
        [SerializeField] Canvas searchCanvas;
        bool searching = false;

        [Header ("Lobby")]
        [SerializeField] Transform UIPlayerParent;
        [SerializeField] GameObject UIPlayerPrefab;
        [SerializeField] Text matchIDText;
        [SerializeField] GameObject[] beginGamesButtons;

        GameObject localPlayerLobbyUI;

        void Start () {
            instance = this;
        }

        public void HostPublic () {
            lobbySelectables.ForEach (x => x.interactable = false);

            Player.localPlayer.HostGame (true);
        }

        public void HostPrivate () {
            lobbySelectables.ForEach (x => x.interactable = false);

            Player.localPlayer.HostGame (false);
        }

        public void HostSuccess (bool success, string matchID) {
            if (success) {
                lobbyCanvas.enabled = true;

                if (localPlayerLobbyUI != null) Destroy (localPlayerLobbyUI);
                localPlayerLobbyUI = SpawnPlayerUIPrefab (Player.localPlayer);
                matchIDText.text = matchID;
                HidePlayButtons();
                beginGamesButtons[0].SetActive (true);
            } else {
                lobbySelectables.ForEach (x => x.interactable = true);
            }
        }

        public void Join () {
            lobbySelectables.ForEach (x => x.interactable = false);

            Player.localPlayer.JoinGame (joinMatchInput.text.ToUpper ());
        }

        public void JoinSuccess (bool success, string matchID) {
            if (success) {
                lobbyCanvas.enabled = true;

                if (localPlayerLobbyUI != null) Destroy (localPlayerLobbyUI);
                localPlayerLobbyUI = SpawnPlayerUIPrefab (Player.localPlayer);
                matchIDText.text = matchID;
            } else {
                lobbySelectables.ForEach (x => x.interactable = true);
            }
        }

        public void DisconnectGame () {
            if (localPlayerLobbyUI != null) Destroy (localPlayerLobbyUI);
            Player.localPlayer.DisconnectGame ();

            lobbyCanvas.enabled = false;
            lobbySelectables.ForEach (x => x.interactable = true);
            HidePlayButtons();
        }

        public GameObject SpawnPlayerUIPrefab (Player player) {
            GameObject newUIPlayer = Instantiate (UIPlayerPrefab, UIPlayerParent);
            newUIPlayer.GetComponent<UIPlayer> ().SetPlayer (player);
            newUIPlayer.transform.SetSiblingIndex (player.playerIndex - 1);

            return newUIPlayer;
        }

        public void BeginGame (string map) {
            Player.localPlayer.BeginGame (map);
        }

        public void SearchGame () {
            StartCoroutine (Searching ());
        }

        public void CancelSearchGame () {
            searching = false;
        }

        public void SearchGameSuccess (bool success, string matchID) {
            if (success) {
                searchCanvas.enabled = false;
                searching = false;
                JoinSuccess (success, matchID);
            }
        }

        IEnumerator Searching () {
            searchCanvas.enabled = true;
            searching = true;

            float searchInterval = 1;
            float currentTime = 1;

            while (searching) {
                if (currentTime > 0) {
                    currentTime -= Time.deltaTime;
                } else {
                    currentTime = searchInterval;
                    Player.localPlayer.SearchGame ();
                }
                yield return null;
            }
            searchCanvas.enabled = false;
        }


        /// <summary>
        /// Fonction pour cacher toutes les campagnes
        /// </summary>
        private void HidePlayButtons()
        {
            for (var i = 0; i < beginGamesButtons.Length; i++)
            {
                beginGamesButtons[i].SetActive(false);
            }
        }

        /// <summary>
        /// Fonction pour changer de campagne avec la flèche de droite
        /// </summary>
        /// <param name="_currentCampaignId">Id de la campagne, commence par 0 comme dans les listes</param>
        public void NextCampaign(int _currentCampaignId)
        {
            if (_currentCampaignId == beginGamesButtons.Length - 1 ) //On vérifie que c'est pas la dernière campagne de la liste
            {
                HidePlayButtons();
                beginGamesButtons[0].SetActive(true);
                return;
            }
            HidePlayButtons(); //Sinon on active la campagne suivante
            beginGamesButtons[_currentCampaignId + 1].SetActive(true); 
        }

        /// <summary>
        /// Fonction pour changer de campagne avec la flèche gauche
        /// </summary>
        /// <param name="_currentCampaignId">Id de la campagne, commence par 0 comme dans les listes</param>
        public void PreviousCampaign(int _currentCampaignId)
        {
            if (_currentCampaignId == 0) //On vérifie si c'est pas la première campagne
            {
                HidePlayButtons();
                beginGamesButtons[beginGamesButtons.Length -1].SetActive(true);
                return;
            }
            HidePlayButtons();
            beginGamesButtons[_currentCampaignId - 1].SetActive(true);
        }
    }
}