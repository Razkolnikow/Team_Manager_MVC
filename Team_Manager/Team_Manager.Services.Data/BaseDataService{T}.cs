using System;
using System.Linq;
using AutoMapper;
using Team_Manager.Data.Common;
using Team_Manager.Data.Common.Models;
using Team_Manager.Data.Models;
using Team_Manager.Services.Data.Contracts;
using Team_Manager.Services.Data.ViewModels;

namespace Team_Manager.Services.Data
{
    public abstract class BaseDataService<T> : IBaseDataService<T>
       where T : class, IDeletableEntity, IAuditInfo
    {
        protected BaseDataService(IDbRepository<T> dataSet)
        {
            if (dataSet == null)
            {
                throw new ArgumentNullException();
            }

            this.Data = dataSet;
        }

        protected IDbRepository<T> Data { get; set; }

        public virtual void Add(T item)
        {
            this.Data.Add(item);
            this.Data.Save();
        }

        public virtual void Delete(object id)
        {
            var entity = this.Data.GetById(id);
            if (entity == null)
            {
                throw new InvalidOperationException("No entity with provided id found.");
            }

            this.Data.Delete(entity);
            this.Data.Save();
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.Data.All();
        }

        public virtual T GetById(object id)
        {
            return this.Data.GetById(id);
        }

        public virtual void Save()
        {
            this.Data.Save();
        }

        public void Dispose()
        {
            this.Data.Dispose();
        }

        protected TeamMemberViewModel MapTeamMemberViewModelToUser(ApplicationUser user)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, TeamMemberViewModel>();

            });
            var mapper = config.CreateMapper();
            return mapper.Map<TeamMemberViewModel>(user);
        }
    }
}
