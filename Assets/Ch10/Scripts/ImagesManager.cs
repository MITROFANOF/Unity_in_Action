using System;
using Ch9.Scripts;
using UnityEngine;

namespace Ch10.Scripts
{
    public class ImagesManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }

        private Texture2D _webImage;
        
        public void Startup()
        {
            Debug.Log("Images manager starting...");

            Status = ManagerStatus.Started;
        }

        public void GetWebImage(Action<Texture2D> callback)
        {
            if (_webImage == null)
            {
                StartCoroutine(NetworkService.DownloadImage(image =>
                {
                    _webImage = image;
                    callback(_webImage);
                }));
            }
            else
            {
                callback(_webImage);
            }
        }
    }
}