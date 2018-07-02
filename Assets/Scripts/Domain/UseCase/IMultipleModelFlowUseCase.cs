using System;
using System.Collections.Generic;
using CAFU.Core.Domain.Model;

namespace CAFU.Flow.Domain.UseCase
{
    public interface IMultipleModelFlowUseCase : IMultipleFlowUseCase
    {
        IObservable<TModel> GetModelFlowAsObservable<TModel>(string key);
        void LoadModel<TModel>(IDictionary<int, TModel> modelMap);
    }
}