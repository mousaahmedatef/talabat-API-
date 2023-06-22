using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BL.Interfaces;
using Talabat.DAL.Data;
using Talabat.DAL.Entities;

namespace Talabat.BL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region comments
        //زمان عشان نوحد البيزنس ف كلاس واحد لكن كان فيه مشكله برضو UnitOfWork احنا استخدمنا ال
        //الى انا محتاجو repoبتاعتى وبعدين عشان استخدمو باخد منو اوبجيكت وانادى ال repositories وهيا انى كنت بحط فيه كل ال
        //الى انا محتاجها والى مش محتاجها repositoriesبس كدا انا لما هاخد اوبجيكت منو انا كدا هيبقا معايا كل ال
        //الى محتاجها repositories الحته دى ونخليه معاه بس ال handel فقالك لا احنا لازم ن
        #endregion
        private readonly StoreContext _context;
       
        #region comments
        //dictionaryومستخدمناس ال key=>value عشان انا هخزن بيانات كدا في صوره  Hashtable احنا استخدمنا هنا
        //string,object بتاعو ممكن يكون اى حاجه زي key ال Hashtableلكن ال int بتاعو لازم يكون key عشان ال
        #endregion
        private Hashtable _repositories;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            #region comments
            //affect الى حلصها rowsالى هوا عدد ال int دى هترجع 
            #endregion
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            #region comments
            //حجزوها EF اوال db الى ال resourcesلل free دى فكرتها عشان اعمل 
            #endregion
            _context.Dispose();
        }

        public IGenericRepo<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            #region comments
            //الخاص بيه repository وهيا ترجعلنا ال entityف الفانكشن دى احنا عايزين نبعتلها ال
            //دا entityبقا بتاع ال repositoryبتاعتو هتبقا ال valueوال repositoryالى انا هعملو ال entity بتاعو هيبقا نوع ال keyأل Hashtable ف انا اول حاجه عملت فوق
            //بتاعو الى متخزن عندى بالفعل repository رجعلو ال Hashtableدا موجود قبل كدا ف ال repository الى بيتطلب ليه  entity وهقارن لو ال
            //بدل م تروح تكريت واحد جديد كل شويه
            #endregion
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepo<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            return (IGenericRepo<TEntity>)_repositories[type];
        }
    }
}
