using System.Reflection;
using System.Text.Json;

namespace Repository.Logic.Repos
{
    public abstract class Repository<TModel>
        where TModel : Models.ModelObject, Contracts.IIdentifyable, ICloneable
    {
        #region Fields
        private List<TModel> modelList = new();
        #endregion Fields

        #region Properties
        protected virtual string FileName { get; private set; } = $"{typeof(TModel).Name}.json";
        #endregion Properties

        protected Repository()
        {
            Load();
        }
        protected Repository(string filePath)
        {
            FileName = filePath;
            Load();
        }

        #region Create
        public abstract TModel Create();
        public virtual Task<TModel> CreateAsync() => Task.Run(() => Create());
        #endregion Create

        #region Get
        public virtual TModel? GetById(int id) => modelList.FirstOrDefault(m => m.Id == id)?.Clone() as TModel;
        public virtual Task<TModel?> GetByIdAsync(int id) => Task.Run(() => GetById(id)); 
        public virtual TModel[] GetAll()
        {
            return modelList.Where(m => m is TModel)
                            .Select(m => (m.Clone() as TModel)!)
                            .ToArray();
        }
        public virtual Task<TModel[]> GetAllAsync() => Task.Run(() => GetAll());
        #endregion Get

        #region Add
        public virtual void Add(TModel model)
        {
            if (modelList.Any())
            {
                model.Id = modelList.Max(m => m.Id) + 1;
            }
            else
            {
                model.Id = 1;
            }
            modelList.Add(model);
        }
        public virtual Task AddAsync(TModel model)
        {
            return Task.Run(() => Add(model));
        }
        #endregion Add

        #region Update
        public virtual bool Update(TModel model)
        {
            var listModel = modelList.FirstOrDefault(m => m.Id == model.Id);

            if (listModel != null)
            {
                modelList.Remove(listModel);
                modelList.Add(model);
            }
            return listModel != null;
        }
        public virtual Task<bool> UpdateAsync(TModel model)
        {
            return Task.Run(() => Update(model));
        }
        #endregion Update

        #region Delete
        public virtual void Delete(int id)
        {
            var listModel = modelList.FirstOrDefault(m => m.Id == id);

            if (listModel != null)
            {
                modelList.Remove(listModel);
            }
        }
        public virtual Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));
        }
        #endregion Delete

        #region SaveChanges
        public virtual void SaveChanges()
        {
            var jsonData = JsonSerializer.Serialize<TModel[]>(modelList.ToArray());

            File.WriteAllText(FileName, jsonData);
        }
        public virtual Task SaveChangesAsync()
        {
            return Task.Run(() => SaveChanges());
        }
        #endregion SaveChanges

        internal virtual void Load()
        {
            modelList.Clear();
            if (File.Exists(FileName))
            {
                var jsonData = File.ReadAllText(FileName);
                var models = JsonSerializer.Deserialize<TModel[]>(jsonData);

                if (models != null)
                {
                    modelList.AddRange(models);
                }
            }
        }
    }
}
