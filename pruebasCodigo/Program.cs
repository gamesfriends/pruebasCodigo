using System;

namespace pruebasCodigo
{
    class Program
    {
        public static int nextIdFolder;

        static void Main(string[] args)
        {
            Disc.folders.Add(new Folder(1, 0, "C:"));
            Disc.currentFolder = Disc.folders[0];
            Disc.folders.Add(new Folder(1, 1, "System"));
            Disc.folders.Add(new Folder(2, 1, "MsDos"));
            Disc.folders.Add(new Folder(3, 1, "Utilities"));
 
            nextIdFolder = nextId(1);

            Disc.currentRoute = @"C:\";
            Disc.previousFolder = Disc.currentFolder;

            string[] sentence = new String[3];
            sentence[0] = "welcome";

            while (!sentence[0].Equals("exit"))
            {
                Console.Write(Disc.currentRoute);
                
                string comando = Console.ReadLine();

                if (comando.Equals("cd .."))
                {
                    newRouteBack();
                } else
                {
                    sentence = comando.Split(" ");
                    if (sentence.Length == 1) actions(sentence[0], "", "");
                    if (sentence.Length == 2) actions(sentence[0], sentence[1], "");
                    if (sentence.Length == 3) actions(sentence[0], sentence[1], sentence[2]);
                }
            }
        }

        public static void actions(string command, string one, string two)
        {
            if (command.Equals("clear")) Console.Clear();
            if (command.Equals("cd")) newRoute(one);
            if (command.Equals("cd ..")) newRouteBack();
            if (command.Equals("create_folder")) newFolder(one);
            if (command.Equals("ls")) dirList();
            if (command.Equals("destroy")) removeFolder(one);
        }

        private static void removeFolder(string enteredFolder)
        {
            int index = 0;
            foreach (Folder item in Disc.folders)
            {
                if (item.name.Equals(enteredFolder)) break;
                index++;
            }
            Disc.folders.RemoveAt(index);
        }

        public static void newRoute(string enteredFolder)
        {
            Disc.previousFolder = Disc.currentFolder;
            foreach (Folder item in Disc.folders)
                if (item.name.Equals(enteredFolder)) Disc.currentFolder = item;
            Disc.currentRoute = Disc.currentRoute + enteredFolder + @"\";
        }

        public static void newRouteBack()
        {
            if (!Disc.currentFolder.name.Equals("C:"))
            {
                int posText = Disc.currentRoute.IndexOf(Disc.currentFolder.name);
                Disc.currentRoute = Disc.currentRoute.Substring(0, posText);
                if (Disc.currentRoute.Equals(@"C:\")) Disc.previousFolder = Disc.folders[0];
                Disc.currentFolder = Disc.previousFolder;
            }
        }

        public static void newFolder(string nameNewFolder)
        {
            Disc.folders.Add(new Folder(nextIdFolder, Disc.currentFolder.id, nameNewFolder));
            nextIdFolder = nextId(1);
        }

        public static void dirList()
        {
            Console.WriteLine("");
            Console.WriteLine("Directorio actual: " + Disc.currentRoute);
            Console.WriteLine("");
            string currentFolder = Disc.currentFolder.name.Substring(0, Disc.currentFolder.name.Length - 1);
            foreach (Folder item in Disc.folders) 
                if (!item.name.Equals(Disc.currentFolder.name) && item.idDependency == Disc.currentFolder.id) 
                    Console.WriteLine(item.name.PadRight(30, ' ') + "<dir>" + "   " + item.id + "   " + item.idDependency);
            Console.WriteLine("");
        }

        public static int nextId(int idCurrent)
        {
            int beginning = 1;
            int idRead = 0;
            foreach (Folder item in Disc.folders)
            {
                if (item.idDependency == idCurrent)
                {
                    if (beginning == 1) idRead = item.id;
                    else if (item.id > idRead) idRead = item.id;
                }
            }
            return idRead + 1;
        }
    }

}
