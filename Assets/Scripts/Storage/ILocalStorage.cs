namespace Yaw.Storage
{
    /// <summary>
    /// Salva dados no armazenamento local
    /// </summary>
    public interface ILocalStorage
    {
        //TODO melhor seria com Tasks. Sincrono por falta de tempo.

        /// <summary>
        /// Busca um valor com a chave
        /// </summary>
        public T Get<T>(string key);

        /// <summary>
        /// Salva um valor com a chave
        /// </summary>
        public void Set<T>(string key, T value);

        /// <summary>
        /// Remove qualquer valor com a chave
        /// </summary>
        public void Delete(string key);
    }
}