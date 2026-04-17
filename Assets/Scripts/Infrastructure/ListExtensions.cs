using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    private static Random _random = new Random();

    public static T GetRandomElement<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
            throw new InvalidOperationException("Невозможно получить случайный элемент из пустого списка.");

        return list[_random.Next(list.Count)];
    }
    
    public static List<T> GetRandomElements<T>(this List<T> list, int count)
    {
        if (list == null || list.Count == 0)
            throw new InvalidOperationException("Невозможно выбрать элементы из пустого списка.");

        count = Math.Min(count, list.Count);

        return list.OrderBy(_ => _random.Next()).Take(count).ToList();
    }
}