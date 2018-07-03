using CAFU.Core.Presentation.View;
using CAFU.Flow.Example.Presentation.Presenter;
using UniRx;

namespace CAFU.Flow.Example.Presentation.View
{
    public class Controller : Controller<Controller, ExamplePresenter, ExamplePresenter.Factory>
    {
        protected override void Start()
        {
            base.Start();
            this.GetPresenter<ExamplePresenter>().Load();
            MessageBroker.Default.Publish("Start");
        }
    }
}