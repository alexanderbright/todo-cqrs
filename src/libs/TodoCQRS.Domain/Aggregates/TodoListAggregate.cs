using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TodoCQRS.Domain.Events;
using TodoCQRS.Domain.Events.TodoItems;
using TodoCQRS.Domain.Models;
using TodoCQRS.Infrastructture.Persistance.Models;
using TodoCQRS.Infrastructure.Framework;

namespace TodoCQRS.Domain.Aggregates
{
  public class TodoListAggregate : Entity<int>, IAggregateRoot
  {
    private readonly List<TodoItem> _items;

    public IReadOnlyCollection<TodoItem> Items => _items.AsReadOnly();

    public Guid ListId { get; protected internal set; }

    public string Name { get; protected internal set; }

    protected TodoListAggregate()
    {
      _items = new List<TodoItem>();
    }

    protected void RaiseEvent<T>(T e) where T: DomainEvent
    {
      BusProvider.Bus.RaiseEvent<T>(e); //TODO: Should collect events
    }

    public void ChangeItemStatus(Guid itemId, bool isCompleted)
    {
      var item = FindItem(itemId);
      if (item == null)
        return;
      item.IsCompleted = isCompleted;
      RaiseEvent(new TodoItemStatusChangedEvent(ListId, itemId, isCompleted));
    }

    public void ChangeItemText(Guid itemId, string text)
    {
      var item = FindItem(itemId);
      if (item == null)
        return;
      item.Text = text;
      RaiseEvent(new TodoItemTextChangedEvent(ListId, itemId, text));
    }

    public void AddItem(Guid itemId, string text, bool isCompleted)
    {
      var item = new TodoItem()
      {
        ItemId = itemId,
        Text = text ?? "<no_name>",
        IsCompleted = isCompleted
      };
      _items.Add(item);
      RaiseEvent(new TodoItemCreatedEvent(ListId, Name, itemId, text, isCompleted));
    }

    public void DeleteItem(Guid itemId)
    {
      var item = FindItem(itemId);
      if (item == null)
        return;

      _items.Remove(item);
      RaiseEvent(new TodoItemDeletedEvent(ListId, itemId));
    }

    private TodoItem FindItem(Guid id)
    {
      var item = _items.FirstOrDefault(i => i.ItemId == id);
      if (item == null)
      {
        RaiseEvent(new ItemNotFoundEvent(ListId, id));
      }
      return item;
    }

    public static class Factory
    {
      public static TodoListAggregate Create(Guid listId, string name)
      {
        var list = new TodoListAggregate()
        {
          ListId = listId,
          Name = name ?? "<no_name>"
        };
        list.RaiseEvent(new TodoListCreatedEvent(listId, name));
        return list;
      }
    }

    public static class Map
    {
      public static TodoListAggregate From(TodoListDto dto, IEnumerable<TodoListItemDto> dtos)
      {
        var model = new TodoListAggregate()
        {
          Key = dto.Id,
          ListId = dto.ListId,
          Name = dto.Name,
        };
        model._items.AddRange(dtos.Select(TodoItem.Map.From));
        return model;
      }
    }
  }
}