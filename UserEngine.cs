using System;
using System.Collections.Generic;
using System.Linq;

public class UserEngine
{
    private readonly DataContext _dataContext;

    public UserEngine(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void RegisterAuthor(string name)
    {
        _dataContext.AddAuthor(new Author(name));
    }

    public void RegisterReader(string name)
    {
        _dataContext.AddReader(new Reader(name));
    }

    public Author GetAuthor(string name)
    {
        return _dataContext.Authors.FirstOrDefault(a => a.Name == name);
    }

    public Reader GetReader(string name)
    {
        return _dataContext.Readers.FirstOrDefault(r => r.Name == name);
    }
}

