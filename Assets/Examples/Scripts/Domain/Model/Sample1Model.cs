using CAFU.Core.Domain.Model;
using CAFU.Flow.Utility;

namespace CAFU.Flow.Example.Domain.Model
{
    public class Sample1Model : IModel, IPrimaryId
    {
        public int PrimaryId { get; set; }
        public string Name { get; set; }
    }
}