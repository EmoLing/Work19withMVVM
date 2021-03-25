using System.Collections.Generic;

namespace InterfasesLib
{
    public interface IAdd
    {
        void AddClient<T>(List<T> item, string depart, string type);
    }
}