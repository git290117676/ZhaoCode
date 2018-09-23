﻿using System;

namespace CSharpKeywords
{
    class Program
    {
        static void Main(string[] args)
        {
            MethodParameters methodParameters = new MethodParameters();

            #region Method Parameters

            #region ref
            Console.WriteLine("----------ref start----------");

            int number = 1;
            MethodParameters.RefMethod(ref number);
            Console.WriteLine(number);    // result:2

            MethodParameters.ModifyProductByReference();
            // result:
            // Original values in Main. Name:laptops,ID:10001
            // Back in Main.Name:laptops,ID: 10002

            methodParameters.ListBooks();
            // output:
            // 三体,by 刘慈欣
            // 三体II: 黑暗森林,by 刘慈欣
            // 三体III: 死神永生,by 刘慈欣
            ref var book = ref methodParameters.GetBookByTitle("三体");
            if (book != null)
            {
                book = new Book { Title = "The Three-Body Problem", Author = "Liu Cixin" };
            }
            methodParameters.ListBooks();
            // output:
            // The Three-Body Problem,by Liu Cixin
            // 三体II: 黑暗森林,by 刘慈欣
            // 三体III: 死神永生,by 刘慈欣

            Console.WriteLine("----------ref end----------\n");
            #endregion

            #region out
            Console.WriteLine("----------out start----------");

            int initializeInMethod;
            methodParameters.OutArgExample(out initializeInMethod);
            Console.WriteLine(initializeInMethod);    // 1

            int argNumber;
            string argMessage, argDefault;
            methodParameters.OutMethod(out argNumber, out argMessage, out argDefault);
            Console.WriteLine(argNumber);    // 100
            Console.WriteLine(argMessage);    // I've been returned
            Console.WriteLine(argDefault == null);    // True

            string numberAsString = "1640";
            int number1;
            if (int.TryParse(numberAsString, out number1))
                Console.WriteLine($"Converted '{numberAsString}' to {number1}");
            else
                Console.WriteLine($"Unable to convert '{numberAsString}'");
            // output:
            // Converted '1640' to 1640
            if (int.TryParse(numberAsString, out int number2))
                Console.WriteLine($"Converted '{numberAsString}' to {number2}");
            else
                Console.WriteLine($"Unable to convert '{numberAsString}'");

            Console.WriteLine("----------out end----------\n");
            #endregion

            #endregion
        }
    }
    #region Method Parameters
    /// <summary>
    /// visit:https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/method-parameters 
    /// </summary>
    class MethodParameters
    {
        #region ref
        public static void RefMethod(ref int refArgument)
        {
            refArgument += 1;
        }

        public static void ModifyProductByReference()
        {
            // Declare an instance of product and display its initial values.
            Product item = new Product("laptops", 10001);
            Console.WriteLine($"Original values in Main. Name:{item.ItemName},ID:{item.ItemID}");

            // Pass the prodcut instance to ChangeByReference.
            ChangeByRefrence(ref item);
            Console.WriteLine($"Back in Main. Name:{item.ItemName},ID:{item.ItemID}");
        }
        private static void ChangeByRefrence(ref Product itemRef)
        {
            itemRef = new Product("laptops", 10001);
            itemRef.ItemID = 10002;
        }

        private Book[] books = { new Book { Title = "三体", Author = "刘慈欣" }, new Book { Title = "三体II:黑暗森林", Author = "刘慈欣" }, new Book { Title = "三体III:死神永生", Author = "刘慈欣" } };
        private Book nobook = null;

        public ref Book GetBookByTitle(string title)
        {
            for (int ctr = 0; ctr < books.Length; ctr++)
            {
                if (title == books[ctr].Title)
                {
                    return ref books[ctr];
                }
            }
            return ref nobook;
        }
        public void ListBooks()
        {
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title},by {book.Author}");
            }
            Console.WriteLine();
        }
        #endregion

        #region out
        public void OutArgExample(out int number)
        {
            number = 1;
        }

        public void OutMethod(out int answer, out string message, out string stillNull)
        {
            answer = 100;
            message = "I've been returned";
            stillNull = null;
        }
        #endregion
    }
    #endregion

    class Product
    {
        public Product(string name, int newID)
        {
            ItemName = name;
            ItemID = newID;
        }
        public string ItemName { get; set; }
        public int ItemID { get; set; }
    }

    class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
    }

}
