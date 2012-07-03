using System;
using System.Collections.Generic;

namespace SimplyTestData
{
    public interface ICustomizationsContainer
    {
        void AddCustomizationsForType<T>(params Action<T>[] customizations);

        void ClearAll();

        void ClearCustomizationsForType<T>();

        IEnumerable<Action<T>> GetCustomizationsApplicableForType<T>();
    }

    //public class SimpleCustomizationsContainer : ICustomizationsContainer
    //{
    //    Dictionary<Type,List<Action<>>> 
    //    public void AddCustomizationsForType<T>(params Action<T>[] customizations)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void ClearAll()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void ClearCustomizationsForType<T>()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<Action<T>> GetCustomizationsApplicableForType<T>()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
