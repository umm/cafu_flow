using UniRx;

namespace CAFU.Flow.Domain.UseCase
{
    public interface IModelFlowUseCase<TModel> : IFlowUseCase
    {
        IObservable<TModel> ModelFlowAsObservable { get; }
    }
}