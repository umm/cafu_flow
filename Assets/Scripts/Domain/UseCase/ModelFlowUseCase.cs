using CAFU.Core.Domain.Model;
using CAFU.Core.Domain.Translator;
using CAFU.Core.Domain.UseCase;
using CAFU.Flow.Utility;
using CAFU.Generics.Data.Entity;
using CAFU.Generics.Domain.Repository;
using UniRx;
using System.Collections.Generic;
using System.Linq;

namespace CAFU.Flow.Domain.UseCase
{
    // Generics多すぎでゴリ押しひどい... でも毎回Model, Entity, Translatorのパターン書くのが嫌やったんや...
    public class ModelFlowUseCase<TModel, TEntity, TEntityList, TModelTranslator>
        : FlowUseCase, IModelFlowUseCase<TModel>
        where TModel : IModel, IPrimaryId
        where TModelTranslator : IModelTranslator<TEntity, TModel>, new()
        where TEntity : IGenericEntity
        where TEntityList : ScriptableObjectGenericEntityList<TEntity>
    {
        public new class
            Factory : DefaultUseCaseFactory<ModelFlowUseCase<TModel, TEntity, TEntityList, TModelTranslator>>
        {
            protected override void Initialize(
                ModelFlowUseCase<TModel, TEntity, TEntityList, TModelTranslator> instance)
            {
                base.Initialize(instance);
                instance.Initialize();
            }
        }

        public IObservable<TModel> ModelFlowAsObservable =>
            this.IdFlowAsObservable
                .Select(id => this.Id2ModelMap[id]);

        private IDictionary<int, TModel> Id2ModelMap { get; set; }

        private IGenericRepository<TEntityList> GenericRepository { get; set; }

        private TModelTranslator Translator { get; set; }

        public override void Load()
        {
            base.Load();

            this.Id2ModelMap = this.GenericRepository.GetEntity().List
                .Select(it => this.Translator.Translate(it))
                .ToDictionary(it => it.PrimaryId);
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.GenericRepository = new GenericRepository<TEntityList>();
            this.Translator = new TModelTranslator();
        }
    }
}