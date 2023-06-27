public class DestroyableBehaviour : UnityEngine.MonoBehaviour
{
  virtual public void Destroy() {
    UnityEngine.Object.Destroy(gameObject);
  }
}