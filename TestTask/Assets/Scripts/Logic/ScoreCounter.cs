using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Logic
{
    public abstract class ScoreCounter : MonoBehaviour
    {
        protected const string InitialScene = "Initial";

        public IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(InitialScene);
        }
    }
}
