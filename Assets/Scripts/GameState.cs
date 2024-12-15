using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wanderbytes
{
    public class GameState : MonoBehaviour
    {
        public static GameState Instance;

        public Dictionary<string, bool> mineStatus = new Dictionary<string, bool>();
        public List<string> completed = new List<string>();

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
            mineStatus.Add("Mine Dragon", false);
            mineStatus.Add("Mine Babi Ngepet", false);
            mineStatus.Add("Mine Demon", false);
            mineStatus.Add("Mine Boss", false);
        }

        public bool IsMineCompleted(string mineName)
        {
            bool status = mineStatus[mineName];
            Debug.Log($"Cek status mine '{mineName}': {status}");
            return status;
        }

        public void CompleteMine(string mineName)
        {
            if (mineStatus.ContainsKey(mineName))
            {
                if(!mineStatus[mineName])
                {
                    mineStatus[mineName] = true;
                    completed.Add(mineName);
                    Debug.Log($"Mine {mineName} telah selesai!");
                }
            }
        }
    }
}
