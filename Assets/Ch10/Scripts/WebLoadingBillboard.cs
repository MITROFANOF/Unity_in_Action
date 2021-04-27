using Ch9.Scripts;
using UnityEngine;

namespace Ch10.Scripts
{
    public class WebLoadingBillboard : MonoBehaviour
    {
        public void Operate()
        {
            Managers.Images.GetWebImage(OnWebImage);
        }

        private void OnWebImage(Texture2D image)
        {
            GetComponent<Renderer>().material.mainTexture = image;
        }
    }
}
