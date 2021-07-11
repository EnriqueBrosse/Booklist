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

        public Repository CurrentRepository;
        public RepositoryManager()
        {
            RepositoryList.Add(BookReposoitory);
            RepositoryList.Add(ComicRepository);
            CurrentRepository = RepositoryList[1];
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

        public async void LoadRepositories()
        {
            List<Task> parallelTasks = new List<Task>();
            parallelTasks.Add(BookReposoitory.GetMediaAsync());
            parallelTasks.Add(ComicRepository.GetMediaAsync());
            await Task.WhenAll(parallelTasks);
        }
    }
}
