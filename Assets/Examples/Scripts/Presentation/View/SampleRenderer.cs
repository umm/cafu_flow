using UnityEngine;
using UnityEngine.UI;

namespace CAFU.Flow.Presentation.Presenter
{
    public class SampleRenderer : MonoBehaviour
    {
        public Text Text;

        public void Render(string message)
        {
            this.Text.text = message;
        }
    }
}