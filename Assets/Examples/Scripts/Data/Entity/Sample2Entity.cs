using System;
using CAFU.Generics.Data.Entity;

namespace CAFU.Flow.Example.Data.Entity
{
    [Serializable]
    public class Sample2EntityList : ScriptableObjectGenericEntityList<Sample2Entity>
    {
    }

    [Serializable]
    public class Sample2Entity : IGenericEntity
    {
        public int Id;
        public string Name;
    }
}