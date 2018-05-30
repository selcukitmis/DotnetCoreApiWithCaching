using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EasyCaching.Core;
using Newtonsoft.Json;

public class TodoService
{
    private readonly IEasyCachingProvider _provider;
    public TodoService(IEasyCachingProvider provider)
    {
        this._provider = provider;
    }

    public List<Todo> GetAll()
    {
        try
        {
            var cacheValue = _provider.Get<string>("TODO");
            var output = new List<Todo>(); ;
            if (cacheValue.HasValue)
            {
                output = JsonConvert.DeserializeObject<List<Todo>>(cacheValue.Value);
            }
            else
            {
                // Get data from DB and Add to Cache
                output.Add(new Todo { Id = 1, Title = "Title 1" });
                output.Add(new Todo { Id = 2, Title = "Title 2" });
                string json = JsonConvert.SerializeObject(output);
                SetItemToCache(json, "TODO");

            }
            return output.OrderByDescending(p => p.Id).ToList();
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public Todo GetById(int id)
    {
        try
        {
            var datas = GetAll();
            if (datas != null)
            {
                var data = datas.Find(p => p.Id.Equals(id));
                return data;
            }
            return null;

        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public List<Todo> Add(Todo todo)
    {
        var todos = GetAll();
        if (!todos.Any(s => s.Id == todo.Id))
        {
            todos.Add(todo);
            string json = JsonConvert.SerializeObject(todos);
            SetItemToCache(json, "TODO");
            return todos;
        }
        throw new ArgumentException("This id already used!");

    }

    public List<Todo> Update(Todo todo)
    {
        try
        {
            var todos = GetAll();
            var find = todos.Find(s => s.Id == todo.Id);
            if (find != null)
            {
                todos.Remove(find);
                todos.Add(todo);
                string json = JsonConvert.SerializeObject(todos);
                SetItemToCache(json, "TODO");
                return todos;
            }
            throw new ArgumentException("This record can't find!");
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public List<Todo> Delete(int id)
    {
        try
        {
            var todos = GetAll();
            var find = todos.Find(s => s.Id == id);
            if (find != null)
            {
                todos.Remove(find);
                string json = JsonConvert.SerializeObject(todos);
                SetItemToCache(json, "TODO");
                return todos;
            }
            throw new ArgumentException("This record can't find!");

        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }


    private void SetItemToCache(string json, string key)
    {
        _provider.Remove(key);
        _provider.Set(key, json, TimeSpan.FromHours(24));
    }
}