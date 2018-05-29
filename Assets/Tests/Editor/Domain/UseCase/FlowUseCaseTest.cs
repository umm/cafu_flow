using System.Collections.Generic;
using CAFU.Flow.Domain.Model;
using CAFU.Flow.Utility;
using UnityModule;
using ExtraUniRx;
using NUnit.Framework;
using UniRx;

namespace CAFU.Flow.Domain.UseCase
{
    public class FlowUseCaseTest
    {
        class FlowUseCaseSpy : FlowUseCase
        {
            public void SetModelList(IList<FlowModel> models)
            {
                this.ModelList = models;
            }

            public void SetStopWatch(IStopWatch stopWatch)
            {
                this.StopWatch = stopWatch;
            }
        }

        [Test]
        public void StartStopPauseResumeTest()
        {
            var usecase = new FlowUseCaseSpy();
            var observer = new TestObserver<int>();
            var oscillator = new Subject<float>();
            var stopWatch = new StopWatch(oscillator);
            usecase.IdFlowAsObservable.Subscribe(observer);
            usecase.SetStopWatch(stopWatch);
            usecase.SetModelList(new List<FlowModel>()
            {
                new FlowModel(
                    0f, 10f, 1f, 0f, 1,
                    new WeightedArray<int>(new Dictionary<int, int>()
                    {
                        {
                            1, 1
                        }
                    }))
            });

            Assert.AreEqual(0, observer.OnNextCount);

            usecase.Start();
            Assert.AreEqual(0, observer.OnNextCount);

            oscillator.OnNext(1f);
            Assert.AreEqual(1, observer.OnNextCount);
            Assert.AreEqual(1, observer.OnNextValues[0]);

            oscillator.OnNext(1f);
            Assert.AreEqual(2, observer.OnNextCount);
            Assert.AreEqual(1, observer.OnNextValues[1]);

            usecase.Stop();
            oscillator.OnNext(1f);
            oscillator.OnNext(1f);
            oscillator.OnNext(1f);
            Assert.AreEqual(2, observer.OnNextCount);

            usecase.Start();
            Assert.AreEqual(2, observer.OnNextCount);

            usecase.Pause();
            oscillator.OnNext(1f);
            oscillator.OnNext(1f);
            oscillator.OnNext(1f);
            Assert.AreEqual(2, observer.OnNextCount);

            usecase.Resume();
            oscillator.OnNext(9f); // 9sec
            Assert.AreEqual(3, observer.OnNextCount);
            Assert.AreEqual(1, observer.OnNextValues[2]);

            oscillator.OnNext(2f); // 11sec
            Assert.AreEqual(3, observer.OnNextCount);
        }
    }
}