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
}