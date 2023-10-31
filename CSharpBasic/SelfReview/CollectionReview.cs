using System.Collections.Generic;
using System.Collections;
using System;
using SelfReview;

public class CollectionReview
{
    int[] arr = new int[3];
    int a = 3;
    //필드에서는 초기화만 가능하다!
    //int[] arr = new int[]{1,2,3}; 이렇게 초기값을 직접 넣던지
    //배열 선언만 하던지.

    //public void ArrAdd()
    //{
    //    arr[0] = 10;
    //    arr[1] = 20;
    //}
    public void ArrRead()
    {
        arr[0] = 10;
        a = 4;
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write($"{arr[i]}\t");
        }
    }

    List<string> colors = new List<string>();
    public void ListColor()
    {
        colors.Add("Red");
        colors.Add("Green");
        colors.Add("Blue");
    }
    public void ListColorRead()
    {
        for (int i = 0; i < colors.Count; i++)
            Console.WriteLine(colors[i]);
    }


    IDictionary<string, string> data = new Dictionary<string, string>();
    public void Dic()
    {
        data.Add("cs", "C#");
        data.Add("aspx", "ASP.NET");
        data.Remove("aspx");
        data["cshtml"] = "ASP.NET MVC";
    }
    public void DicRead()
    {
        foreach (KeyValuePair<string, string> item in data)
            Console.WriteLine($"{item.Key}은/는 {item.Value}의 확장자입니다");
    }
}


