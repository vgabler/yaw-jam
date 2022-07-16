using System.Collections.Generic;

namespace Yaw.Data
{
    /// <summary>
    /// Gerencia uma lista de objetos do tipo T
    /// </summary>
    public interface IDataProvider<T>
    {
        //TODO async. Melhor ainda, reactive
        List<T> GetAll();
    }

    /// <summary>
    /// Gerencia um único objeto do tipo T
    /// </summary>
    public interface ISingleDataProvider<T>
    {
        //TODO async. Melhor ainda, reactive
        T Data { get; }

        void Set(T value);
    }
}