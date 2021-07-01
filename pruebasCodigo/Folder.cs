using System;
using System.Collections.Generic;
using System.Text;

namespace pruebasCodigo
{
    public class Folder
    {
        public int id;
        public int idDependency;
        public string name;

        public Folder(int id, int idDependency, string name)
        {
            this.id = id;
            this.idDependency = idDependency;
            this.name = name;
        }
    }
}
