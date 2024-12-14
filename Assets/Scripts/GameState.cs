using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wanderbytes
{
    public class GameState : MonoBehaviour
    {
        public static GameState Instance;

        public Dictionary<string, bool> mineStatus = new Dictionary<string, bool>();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            mineStatus.Add("Mine Vampire", false);
            mineStatus.Add("Mine 2", false);
            mineStatus.Add("Mine 3", false);
            mineStatus.Add("Mine 4", false);
            mineStatus.Add("Mine 5", false);
        }

        public bool IsMineCompleted(string mineName)
        {
            bool status = mineStatus.ContainsKey(mineName) && mineStatus[mineName];
            Debug.Log($"Cek status mine '{mineName}': {status}");
            return status;
        }

        public void CompleteMine(string mineName)
        {
            if (mineStatus.ContainsKey(mineName))
            {
                mineStatus[mineName] = true;
                Debug.Log($"Mine {mineName} telah selesai!");
            }
        }
    }
}
