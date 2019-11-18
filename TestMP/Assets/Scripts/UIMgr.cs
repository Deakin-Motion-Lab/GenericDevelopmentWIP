using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace CrossPlatformVR
{
    /// <summary>
    /// This class manages the updating of information presented on the UI present in the VR room scene
    /// </summary>
    public class UIMgr : MonoBehaviourPunCallbacks
    {
        // Attributes
        private int playerCount;
        public Text totalPlayers;
        public Text playerList;

        void Start()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            StringBuilder sb = new StringBuilder();
            playerCount = 0;

            // Update player list
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsMasterClient)
                {
                    sb.AppendLine(player.ActorNumber.ToString() + " (Master)");
                }
                else
                {
                    sb.AppendLine(player.ActorNumber.ToString());
                }
            }

            playerCount = PhotonNetwork.PlayerList.Length;

            // Update UI
            totalPlayers.text = playerCount.ToString();
            playerList.text = sb.ToString();
        }

        #region Photon Callbacks
        // Update Scene UI when networked players enter / leave rooms
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            UpdateUI();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdateUI();
        }

        #endregion
    }
}
