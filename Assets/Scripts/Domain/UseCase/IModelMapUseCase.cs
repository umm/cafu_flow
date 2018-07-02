using System.Collections.Generic;
using CAFU.Core.Domain.Model;
using CAFU.Core.Domain.UseCase;

public interface IModelMapUseCase<TModel> : IUseCase
    where TModel : IModel
{
    void Load();
    IDictionary<int, TModel> Id2ModelMap { get; }
}