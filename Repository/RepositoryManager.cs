using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Booklist.Model;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;

namespace Booklist.Repository
{
    public class Singleton<T> where T : class, new()
    {
        protected static readonly Lazy<T> instance = new Lazy<T>(() => new T());
        public static T GetInstance()
        {
            return instance.Value;
        }
    }

    public class RepositoryManager : Singleton<RepositoryManager>
    {
        public List<Repository> RepositoryList = new List<Repository>();
        public BookReposoitory BookReposoitory = new BookReposoitory();
        public ComicRepository ComicRepository = new ComicRepository();
        public MusicRepository MusicRepository = new MusicRepository();
        public GamesRepository GamesRepository = new GamesRepository();
        public FilmMediaRepository FilmMediaRepository = new FilmMediaRepository();
        public Repository CurrentRepository;
        public RepositoryManager()
        {
            RepositoryList.Add(BookReposoitory);
            RepositoryList.Add(ComicRepository);
            RepositoryList.Add(MusicRepository);
            RepositoryList.Add(GamesRepository);
            RepositoryList.Add(FilmMediaRepository);
            CurrentRepository = RepositoryList[0];
        }

        public List<string> GetRepositoryNames()
        {
            List<string> names = new List<string>();
            for (int i = 0; i < RepositoryList.Count; i++)
            {
                names.Add(RepositoryList[i].ToString());
            }
            return names;
        }

        public void SetCurrentRepository(string repositoryName)
        {
            for (int i = 0; i < RepositoryList.Count; i++)
            {
                if (RepositoryList[i].ToString().Equals(repositoryName))
                {
                    CurrentRepository = RepositoryList[i];
                    break;
                }
            }
        }

        public void LoadRepositories()
        {
            BookReposoitory.GetMedia();
            ComicRepository.GetMedia();
            MusicRepository.GetMedia();
            GamesRepository.GetMedia();
            FilmMediaRepository.GetMedia();
        }

        public async void LoadRepositoriesAsync()
        {
            List<Task> parallelTasks = new List<Task>();
            parallelTasks.Add(BookReposoitory.GetMediaAsync());
            parallelTasks.Add(ComicRepository.GetMediaAsync());
            parallelTasks.Add(MusicRepository.GetMediaAsync());
            parallelTasks.Add(GamesRepository.GetMediaAsync());
            parallelTasks.Add(FilmMediaRepository.GetMediaAsync());
            await Task.WhenAll(parallelTasks);
        }
    }
}
