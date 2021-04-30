using Ch9.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Ch12.Scripts
{
    public class StartupController : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;

        private void Awake()
        {
            Messenger<int, int>.AddListener(StartupEvent.ManagersProgress, OnManagersProgress);
            Messenger.AddListener(StartupEvent.ManagersStarded, OnManagersStarted);
        }

        private void OnDestroy()
        {
            Messenger<int, int>.RemoveListener(StartupEvent.ManagersProgress, OnManagersProgress);
            Messenger.RemoveListener(StartupEvent.ManagersStarded, OnManagersStarted);
        }

        private void OnManagersProgress(int numReady, int numModules)
        {
            var progress = (float) numReady / numModules;
            progressBar.value = progress;
        }
        
        private void OnManagersStarted()
        {
            Managers.Misson.GoToNext();
        }
    }
}
