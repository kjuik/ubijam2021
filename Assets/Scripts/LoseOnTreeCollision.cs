using UnityEngine;

namespace Assets.Scripts
{
    public class LoseOnTreeCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Tree>())
            {
                GameManager.Instance.Lose();
            }
        }
    }
}