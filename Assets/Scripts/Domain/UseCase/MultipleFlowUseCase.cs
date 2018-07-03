using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Domain.UseCase;
using CAFU.Flow.Data.Entity;
using CAFU.Flow.Domain.Model;
using CAFU.Flow.Domain.Translator;
using CAFU.Generics.Domain.Repository;
using ExtraCollection;
using UnityModule;
using UniRx;

namespace CAFU.Flow.Domain.UseCase
{
    public class MultipleFlowUseCase : IMultipleFlowUseCase
    {
        public class Factory : DefaultUseCaseFactory<MultipleFlowUseCase>
        {
            protected override void Initialize(MultipleFlowUseCase instance)
            {
                base.Initialize(instance);
                instance.Initialize();
            }
        }

        private IGenericRepository<MultipleFlowEntityList> GenericRepository { get; set; }
        private FlowModelTranslator Translator { get; set; }
        protected IDictionary<string, IStopWatch> StopWatchMap { get; set; }
        protected IDictionary<string, IList<FlowModel>> ModelMap { get; set; }
        private IDictionary<string, IDisposable> DisposableMap { get; set; }
        protected IDictionary<string, UniRx.ISubject<int>> SubjectMap { get; set; }

        protected virtual void Initialize()
        {
            this.GenericRepository = new GenericRepository<MultipleFlowEntityList>.Factory().Create();
            this.Translator = new FlowModelTranslator();
            this.StopWatchMap = new Dictionary<string, IStopWatch>();
            this.ModelMap = new Dictionary<string, IList<FlowModel>>();
            this.SubjectMap = new Dictionary<string, ISubject<int>>();
            this.DisposableMap = new Dictionary<string, IDisposable>();
        }

        public virtual void Load()
        {
            var multipleEntityList = this.GenericRepository.GetEntity();
            var entityListMap = multipleEntityList.List.ToDictionary(it => it.Key);
            this.LoadInitialize(entityListMap);
        }

        private void LoadInitialize(IEnumerable<KeyValuePair<string, FlowEntityList>> pairs)
        {
            foreach (var pair in pairs)
            {
                this.ModelMap[pair.Key] = this.Translator.TranslateList(pair.Value).ToList();
                this.StopWatchMap.GetOrSet(pair.Key, () => new StopWatch());
                this.SubjectMap.GetOrSet(pair.Key, () => new Subject<int>());
            }
        }

        public void Start(string key)
        {
            this.Stop(key);
            this.StopWatchMap[key].Start();
            this.DisposableMap[key] = this.StopWatchMap[key].TimeAsObservable
                .Subscribe(time => this.ModelGenerate(key, this.ModelMap[key], time));
        }

        public void Stop(string key)
        {
            this.StopWatchMap[key].Stop();
            this.DisposableMap.GetOrDefault(key)?.Dispose();
        }

        public void Resume(string key)
        {
            this.StopWatchMap[key].Resume();
        }

        public void Pause(string key)
        {
            this.StopWatchMap[key].Pause();
        }

        public IObservable<int> GetIdFlowAsObservable(string key)
        {
            return this.SubjectMap.GetOrSet(key, () => new Subject<int>());
        }

        private void ModelGenerate(string key, IList<FlowModel> models, float time)
        {
            foreach (var model in models)
            {
                if (!model.IsFinished(time))
                {
                    model.GenerateItems(time);

                    foreach (var id in model.EmitItems(time))
                    {
                        this.SubjectMap[key].OnNext(id);
                    }
                }
            }
        }
    }
}