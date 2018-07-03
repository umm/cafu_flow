using CAFU.Core.Domain.Translator;
using CAFU.Flow.Example.Data.Entity;
using CAFU.Flow.Example.Domain.Model;

namespace CAFU.Flow.Example.Domain.Translator
{
    public class Sample2Translator : IModelTranslator<Sample2Entity, Sample2Model>
    {
        public Sample2Model Translate(Sample2Entity entity)
        {
            return new Sample2Model {PrimaryId = entity.Id, Name = entity.Name};
        }
    }
}
