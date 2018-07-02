using CAFU.Core.Domain.Model;
using CAFU.Flow.Utility;

namespace CAFU.Flow.Example.Domain.Model
{
    public class Sample2Model : IModel, IPrimaryId
    {
        public int PrimaryId { get; set; }
        public string Name { get; set; }
    }
}
