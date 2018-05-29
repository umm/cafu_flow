# cafu_flow

## What

* Flow is interval event trigger for any games 

## Requirement

* cafu_core
* cafu_generics
* extra_collection
* unity_module_stopwatch

## Install

```shell
yarn add "umm-projects/cafu_flow#^1.0.0"
```

## Usage

Implement IFlowPresenter on your Presenter

```csharp
public class MyPresenter : IModelFlowPresenter<YourModel>
{
    public class Factory : DefaultPresenterFactory<SoybeanScreeningPresenter>
    {
        protected override void Initialize(SoybeanScreeningPresenter instance)
        {
            base.Initialize(instance);
            instance.ModelFlowUseCase = new ModelFlowUseCase<YourModel, YourEntity, YourEntityList, YourTranslator>.Factory().Create();
        }
    }

    public IModelFlowUseCase<SoybeanModel> ModelFlowUseCase { get; private set; }
}
```

```csharp
presenter.ModelFlowAsObservable.Subscribe(model => /* do any thing on model event triggered */);
presenter.StartFlow(); // start your model flow 
presenter.StopFlow(); // stop your model flow 
```

## License

Copyright (c) 2018 Takuma Maruyama

Released under the MIT license, see [LICENSE.txt](LICENSE.txt)

