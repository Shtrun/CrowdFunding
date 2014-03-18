using System;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Security;
using System.Security.Principal;
using Breeze.WebApi;
using WebMatrix.WebData;
using System.Collections.Generic;

namespace CrowdFunding.Models
{
    public class UsersRepository : EFContextProvider<UsersContext>
    {
        public UsersRepository(IPrincipal user)
        {
            UserId = user.Identity.Name;
            Context.Configuration.LazyLoadingEnabled = false;
        }

        public string UserId { get; private set; }

        public DbQuery<UserProfile> UserProfiles
        {
            get
            {
                var userProfiles = (DbQuery<UserProfile>)Context.UserProfiles;
                return userProfiles;
            }
        }

        public DbQuery<Role> Roles
        {
            get
            {
                return (DbQuery<Role>)Context.Roles;
            }
        }

        public List<UsersInRoles> UsersInRoles
        {
            get
            {
                var roles = (SimpleRoleProvider)System.Web.Security.Roles.Provider;
                var xx = this.Roles.Include("UserProfiles");
                var po = new List<UsersInRoles>();
                foreach (var x in xx)
                {
                    foreach (var y in x.UserProfiles)
                    {
                        po.Add(new UsersInRoles() { UserId = y.UserId, Role = x.RoleName });
                    }
                }
                return po;
            }
        }

        public DbQuery<Membership> Memberships
        {
            get
            {
                return (DbQuery<Membership>)Context.Memberships;
            }
        }

        public DbQuery<OAuthMembership> OAuthMemberships
        {
            get
            {
                return (DbQuery<OAuthMembership>)Context.OAuthMemberships;
            }
        }
        #region Save processing

        //// Todo: delegate to helper classes when it gets more complicated

        //protected override bool BeforeSaveEntity(EntityInfo entityInfo)
        //{
        //    var entity = entityInfo.Entity;
        //    if (entity is TodoList)
        //    {
        //        return BeforeSaveTodoList(entity as TodoList, entityInfo);
        //    }
        //    if (entity is TodoItem)
        //    {
        //        return BeforeSaveTodoItem(entity as TodoItem, entityInfo);
        //    }
        //    throw new InvalidOperationException("Cannot save entity of unknown type");
        //}


        //private bool BeforeSaveTodoList(TodoList todoList, EntityInfo info)
        //{
        //    if (info.EntityState == EntityState.Added)
        //    {
        //        todoList.UserId = UserId;
        //        return true;
        //    }
        //    return UserId == todoList.UserId || throwCannotSaveEntityForThisUser();
        //}

        //private bool BeforeSaveTodoItem(TodoItem todoItem, EntityInfo info)
        //{
        //    var todoList = ValidationContext.TodoLists.Find(todoItem.TodoListId);
        //    return (null == todoList)
        //               ? throwCannotFindParentTodoList()
        //               : UserId == todoList.UserId || throwCannotSaveEntityForThisUser();
        //}

        //// "this.Context" is reserved for Breeze save only!
        //// A second, lazily instantiated DbContext will be used
        //// for db access during custom save validation. 
        //// See this stackoverflow question and reply for an explanation:
        //// http://stackoverflow.com/questions/14517945/using-this-context-inside-beforesaveentity
        //private TodoItemContext ValidationContext
        //{
        //    get { return _validationContext ?? (_validationContext = new TodoItemContext()); }
        //}
        //private TodoItemContext _validationContext;

        //private bool throwCannotSaveEntityForThisUser()
        //{
        //    throw new SecurityException("Unauthorized user");
        //}

        //private bool throwCannotFindParentTodoList()
        //{
        //    throw new InvalidOperationException("Invalid TodoItem");
        //}

        #endregion

    }
}