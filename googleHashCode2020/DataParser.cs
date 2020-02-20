using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace googleHashCode2020
{
    public class DataParser
    {
        public ParseResult ReadData(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = fileName;
            var commonData = new CommonData();
            ParseResult resultData = new ParseResult();
            List<int> orderedScoreList = new List<int>();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var line = 0;
                var libraryId = 0;
                while(!reader.EndOfStream)
                {
                    string result = reader.ReadLine();
                    if(result.Length == 0)
                    {
                        continue;
                    }

                    if(line < 2)
                    {
                        if(line == 0)
                        {
                            var rawData = result.Split(" ");
                            commonData.BooksCount = int.Parse(rawData[0]);
                            commonData.LibririesCount = int.Parse(rawData[1]);
                            commonData.DaysForScanning = int.Parse(rawData[2]);

                            resultData = new ParseResult
                            {
                                commonData = commonData,
                                libraryDatas = new LibraryData[commonData.LibririesCount]
                            };
                        } 
                            else
                        {
                            orderedScoreList = result.Split(" ").Select(x => int.Parse(x)).ToList();
                        }
                    }
                    else
                    {
                        var rawData = result.Split(" ");

                        if (line % 2 == 0)
                        {
                            var libraryData = new LibraryData
                            {
                                LibraryId = libraryId,
                                BooksCount = int.Parse(rawData[0]),
                                DaysForSignUp = int.Parse(rawData[1]),
                                ShipBooksPerDay = int.Parse(rawData[2]),
                                Books = new Book[int.Parse(rawData[0])]
                            };

                            resultData.libraryDatas[libraryId] = libraryData;
                        } else
                        {
                            var libraryData = resultData.libraryDatas[libraryId];

                            for(var i = 0; i < rawData.Length; i++)
                            {
                                var bookId = int.Parse(rawData[i]);
                                libraryData.Books[i] = new Book
                                {
                                    Id = bookId,
                                    Score = orderedScoreList[bookId]
                                };
                            }

                            libraryId++;
                        }
                    }

                    line++;
                }               
            }

            return resultData;
        }
    }

    public struct CommonData
    {
        public int BooksCount;
        public int LibririesCount;
        public int DaysForScanning;
    }

    public struct LibraryData
    {
        public int LibraryId;
        public int BooksCount;
        public int DaysForSignUp;
        public int ShipBooksPerDay;
        public Book[] Books;
    }

    public struct Book
    {
        public int Id;
        public int Score;
    }

    public struct ParseResult
    {
        public CommonData commonData;
        public LibraryData[] libraryDatas;
    }
}
