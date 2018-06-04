using System;
using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Domain.UseCase;
using CAFU.Flow.Data.Entity;
using CAFU.Flow.Domain.Model;
using CAFU.Flow.Domain.Translator;
using CAFU.Generics.Domain.Repository;
using UnityModule;
using UniRx;

namespace CAFU.Flow.Domain.UseCase
{
    public interface IFlowUseCase : IUseCase
    {
        /// <summary>
        /// Loads FlowModels from FlowEntityList
        /// </summary>
        void Load();

        /// <summary>
        /// Start flow
        /// </summary>
        void Start();

        /// <summary>
        /// Stop flow
        /// </summary>
        void Stop();

        /// <summary>
        /// Resume flow
        /// </summary>
        void Resume();

        /// <summary>
        /// Pause flow
        /// </summary>
        void Pause();

        /// <summary>
        /// Flow of Ids.
        /// </summary>
        IObservable<int> IdFlowAsObservable { get; }
    }

    public class FlowUseCase : IFlowUseCase
    {
        public class Factory : DefaultUseCaseFactory<FlowUseCase>
        {
            protected override void Initialize(FlowUseCase instance)
            {
                base.Initialize(instance);
                instance.Initialize();
            }
        }

        public IObservable<int> IdFlowAsObservable => this.IdSubject;

        private IGenericRepository<FlowEntityList> GenericRepository { get; set; }

        private FlowModelTranslator Translator { get; set; }

        protected IList<FlowModel> ModelList { get; set; }

        protected IStopWatch StopWatch { get; set; }

        private ISubject<int> IdSubject { get; set; } = new Subject<int>();

        private IDisposable Subscription { get; set; }

        public virtual void Load()
        {
            var entityList = this.GenericRepository.GetEntity();
            this.ModelList = entityList.List.Select(it => this.Translator.Translate(it)).ToList();
        }

        public void Start()
        {
            this.Stop();

            this.StopWatch.Start();
            this.Subscription = this.StopWatch.TimeAsObservable.Subscribe(time =>
            {
                foreach (var model in this.ModelList)
                {
                    if (!model.IsFinished(time))
                    {
                        model.GenerateItems(time);

                        foreach (var id in model.EmitItems(time))
                        {
                            this.IdSubject.OnNext(id);
                        }
                    }
                }
            });
        }

        public void Stop()
        {
            this.StopWatch.Stop();
            this.Subscription?.Dispose();
        }

        public void Resume()
        {
            this.StopWatch.Resume();
        }

        public void Pause()
        {
            this.StopWatch.Pause();
        }

        protected virtual void Initialize()
        {
            this.GenericRepository = new GenericRepository<FlowEntityList>.Factory().Create();
            this.Translator = new FlowModelTranslator();
            this.StopWatch = new StopWatch();
        }
    }
}