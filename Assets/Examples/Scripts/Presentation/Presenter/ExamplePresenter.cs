using CAFU.Core.Presentation.Presenter;
using CAFU.Flow.Domain.UseCase;
using CAFU.Flow.Example.Data.Entity;
using CAFU.Flow.Example.Domain.Model;
using CAFU.Flow.Example.Domain.Translator;
using CAFU.Flow.Presentation.Presenter;

namespace CAFU.Flow.Example.Presentation.Presenter
{
    public class ExamplePresenter : IMultipleModelFlowPresenter
    {
        public class Factory : DefaultPresenterFactory<ExamplePresenter>
        {
            protected override void Initialize(ExamplePresenter instance)
            {
                base.Initialize(instance);
                instance.MultipleModelFlowUseCase = new MultipleModelFlowUseCase.Factory().Create();
                instance.Sample1ModelMapUseCase = new ModelMapUseCase<Sample1Model, Sample1Entity, Sample1EntityList, Sample1Translator>.Factory().Create();
                instance.Sample2ModelMapUseCase = new ModelMapUseCase<Sample2Model, Sample2Entity, Sample2EntityList, Sample2Translator>.Factory().Create();
            }
        }

        public IMultipleModelFlowUseCase MultipleModelFlowUseCase { get; private set; }
        public IModelMapUseCase<Sample1Model> Sample1ModelMapUseCase { get; set; }
        public IModelMapUseCase<Sample2Model> Sample2ModelMapUseCase { get; set; }

        public void Load()
        {
            this.MultipleModelFlowUseCase.Load();
            this.Sample1ModelMapUseCase.Load();
            this.Sample2ModelMapUseCase.Load();
            this.MultipleModelFlowUseCase.LoadModel(this.Sample1ModelMapUseCase.Id2ModelMap);
            this.MultipleModelFlowUseCase.LoadModel(this.Sample2ModelMapUseCase.Id2ModelMap);
        }
    }
}