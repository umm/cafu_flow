using System;
using CAFU.Flow.Domain.UseCase;
using IPresenter = CAFU.Core.Presentation.Presenter.IPresenter;

namespace CAFU.Flow.Presentation.Presenter
{
    public interface IModelFlowPresenter<TModel> : IPresenter
    {
        IModelFlowUseCase<TModel> ModelFlowUseCase { get; }
    }

    public static class IModelFlowPresenterExtension
    {
        public static void LoadFlow<TModel>(this IModelFlowPresenter<TModel> presenter)
        {
            presenter.ModelFlowUseCase.Load();
        }

        public static void StartFlow<TModel>(this IModelFlowPresenter<TModel> presenter)
        {
            presenter.ModelFlowUseCase.Start();
        }

        public static void StopFlow<TModel>(this IModelFlowPresenter<TModel> presenter)
        {
            presenter.ModelFlowUseCase.Stop();
        }

        public static void ResumeFlow<TModel>(this IModelFlowPresenter<TModel> presenter)
        {
            presenter.ModelFlowUseCase.Resume();
        }

        public static void PauseFlow<TModel>(this IModelFlowPresenter<TModel> presenter)
        {
            presenter.ModelFlowUseCase.Pause();
        }

        public static IObservable<TModel> GetModelFlowAsObservable<TModel>(this IModelFlowPresenter<TModel> presenter)
        {
            return presenter.ModelFlowUseCase.ModelFlowAsObservable;
        }
    }
}