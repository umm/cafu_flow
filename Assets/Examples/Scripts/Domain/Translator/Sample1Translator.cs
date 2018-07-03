using CAFU.Core.Domain.Translator;
using CAFU.Flow.Example.Data.Entity;
using CAFU.Flow.Example.Domain.Model;

namespace CAFU.Flow.Example.Domain.Translator
{
    public class Sample1Translator : IModelTranslator<Sample1Entity, Sample1Model>
    {
        public Sample1Model Translate(Sample1Entity entity)
        {
            return new Sample1Model {PrimaryId = entity.Id, Name = entity.Name};
        }
    }
}