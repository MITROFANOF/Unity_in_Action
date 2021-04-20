using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ch5.Scripts
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private MemoryCard originalCard;
        [SerializeField] private Sprite[] images;
        [SerializeField] private TextMesh scoreLabel;
        
        private const int GridRows = 2;
        private const int GridCols = 4;
        private const float OffsetX = 2f;
        private const float OffsetY = 2.5f;

        private MemoryCard _firstRevealed;
        private MemoryCard _secondRevealed;
        private int _score;

        public bool CanReveal => _secondRevealed == null;

        public void CardRevealed(MemoryCard card)
        {
            if (_firstRevealed == null)
            {
                _firstRevealed = card;
            }
            else
            {
                _secondRevealed = card;
                StartCoroutine(CheckMatch());
            }
        }

        private IEnumerator CheckMatch()
        {
            if (_firstRevealed.ID == _secondRevealed.ID)
            {
                _score++;
                scoreLabel.text = "Score: " + _score;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                _firstRevealed.Unreveal();
                _secondRevealed.Unreveal();
            }

            _firstRevealed = null;
            _secondRevealed = null;
        }

        public void Restart()
        {
            SceneManager.LoadScene("Ch5");
        }

        private void Start()
        {
            var startPos = originalCard.transform.position;

            int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};
            numbers = ShuffleArray(numbers);
            
            for (var i = 0; i < GridCols; i++)
            {
                for (var j = 0; j < GridRows; j++)
                {
                    MemoryCard card;
                    if (i == 0 && j == 0)
                    {
                        card = originalCard;
                    }
                    else
                    {
                        card = Instantiate(originalCard);
                    }

                    var index = j * GridCols + i;
                    var id = numbers[index];
                    card.SetCard(id, images[id]);

                    var posX = (OffsetX * i) + startPos.x;
                    var posY = -(OffsetY * j) + startPos.y;
                    card.transform.position = new Vector3(posX, posY, startPos.z);
                }
            }
            
            
        }

        private static int[] ShuffleArray(int[] numbers)
        {
            var newArray = numbers.Clone() as int[];
            
            for (var i = 0; i < newArray?.Length; i++)
            {
                var tmp = newArray[i];
                var r = Random.Range(i, newArray.Length);
                newArray[i] = newArray[r];
                newArray[r] = tmp;
            }

            return newArray;
        }
    }
}
