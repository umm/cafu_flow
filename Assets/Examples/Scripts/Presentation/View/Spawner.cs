using CAFU.Core.Presentation.View;
using CAFU.Flow.Example.Domain.Model;
using CAFU.Flow.Example.Presentation.Presenter;
using CAFU.Flow.Presentation.Presenter;
using UniRx;
using UnityEngine.EventSystems;

namespace CAFU.Flow.Example.Presentation.View
{
    public class Spawner : UIBehaviour, IView
    {
        public SampleRenderer Prefab;
        public bool IsSample1 = true;
        public string FlowKey = "flow1";

        protected override void Start()
        {
            base.Start();

            MessageBroker.Default.Receive<string>().Where(it => it == "Start")
                .Subscribe(_ => this.Initialize())
                .AddTo(this);
        }

        void Initialize()
        {
            this.GetPresenter<ExamplePresenter>().StartMultipleFlow(this.FlowKey);

            if (this.IsSample1)
            {
                this.GetPresenter<IMultipleModelFlowPresenter>()
                    .GetModelMultipleFlow<Sample1Model>(this.FlowKey)
                    .Subscribe(it => this.Spawn(it.Name))
                    .AddTo(this);
            }
            else
            {
                this.GetPresenter<IMultipleModelFlowPresenter>()
                    .GetModelMultipleFlow<Sample2Model>(this.FlowKey)
                    .Subscribe(it => this.Spawn(it.Name))
                    .AddTo(this);
            }
        }

        void Spawn(string name)
        {
            var obj = Instantiate(this.Prefab, this.transform, false);
            obj.Render(name);
        }
    }
}