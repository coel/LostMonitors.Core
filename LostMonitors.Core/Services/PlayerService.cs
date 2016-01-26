using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LostMonitors.Core.Services
{
    public class Player
    {
        public Player(Type playerType)
        {
            PlayerType = playerType;
            Name = GetInstance().GetFriendlyName();
        }

        public Type PlayerType { get; set; }
        public string Name { get; set; }

        public IPlayer GetInstance()
        {
            var instance = Activator.CreateInstance(PlayerType);
            return instance as IPlayer;
        }
    }

    public static class PlayerService
    {
        private static List<Player> Players; 

        static PlayerService()
        {
            // Going to boldy assume you want to load all assemblies in current directory if not specified
            var assemblyFolder = ConfigurationManager.AppSettings["LostMonitors.AssemblyFolder"] ?? Assembly.GetExecutingAssembly().Location;
            LoadAssemblies(assemblyFolder);

            Players = GetPlayersAppDomain();
        }

        private static void LoadAssemblies(string assemblyFolder)
        {
            var assemblies = new List<Assembly>();
            var path = Path.GetDirectoryName(assemblyFolder);
            
            var dlls = Directory.GetFiles(path, "*.dll");
            foreach (string dll in dlls)
            {
                assemblies.Add(Assembly.LoadFile(dll));
            }
        }

        private static List<Player> GetPlayersAppDomain()
        {
            var type = typeof (IPlayer);
            var players = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            return players.Select(x => new Player(x)).ToList();
        }

        public static List<Player> GetPlayers()
        {
            return Players.ToList();
        }

        public static Player GetPlayer(string name)
        {
            return Players.FirstOrDefault(x => x.Name == name);
        }
    }
}