﻿using System.Xml.Serialization;
using XmlAttributesDemo.Models;
using System.IO;
using System.Xml;
using System.Text;
using XmlAttributesDemo.XmlAttributesDemo.XmlAttributesDemo.Models;
using System;

namespace XmlAttributesDemo
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            //LibraryDto[] libraries = GetLibraries();

            //serialization
            //StringBuilder sb = new StringBuilder();
            //XmlSerializer serializer = new XmlSerializer(typeof(LibraryDto), new XmlRootAttribute("libraries"));
            //serializer.Serialize(new StringWriter(sb), libraries, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty} ));

            //deserialization
            //string xmlString = File.ReadAllText("XmlPath");
            //StringBuilder sb = new StringBuilder();
            //XmlSerializer serializer = new XmlSerializer(typeof(LibraryDto), new XmlRootAttribute("libraries"));  
            //serializer.Deserialize(new StringReader(xmlString));

            //deserialization 
            //users.xml does not exist here - it is only for example
            //string xmlString = File.ReadAllText("users.xml");
            //StringBuilder sb = new StringBuilder();
            //XmlSerializer serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            //UserDto[] deserializedUsers = (UserDto[])serializer.Deserialize(new StringReader(xmlString));
            //foreach (UserDto user in deserializedUsers)
            //{
            //    Console.WriteLine(user.LastName);
            //}

            //serialization
            XmlSerializer serializer = new XmlSerializer(typeof(LibraryDto[]), new XmlRootAttribute("Libraries"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            LibraryDto[] libraries = GetLibraries();
            using (TextWriter writer = new StreamWriter("../Library.xml"))
            {
                serializer.Serialize(writer, libraries, namespaces);
            }
        }

        private static LibraryDto[] GetLibraries()
        {
            LibraryDto firstLibrary = new LibraryDto
            {
                LibraryName = "Jo Bowl",
                Sections = new SectionDto()
                {
                    Name = "Horror",
                    Books = new BookDto[]
                    {
                        new BookDto() {
                            Name = "It",
                            Author = "Stephen King",
                            Description = "Here you can put description about the book"
                        },
                        new BookDto() {
                            Name = "Frankenstein",
                            Author = "Mary Shelley",
                            Description = "Here you can put description about the book"
                        }
                    }
                },
                CardPrice = 20.30m
            };

            LibraryDto secondLibrary = new LibraryDto
            {
                LibraryName = "Kevin Sanchez",
                Sections = new SectionDto()
                {
                    Name = "Comedy",
                    Books = new BookDto[]
                    {
                        new BookDto()
                        {
                            Name = "The Diary of a Nobody",
                            Author = "George Grossmith and Weeden Grossmith",
                            Description = "Here you can put description about the book"
                        },
                        new BookDto()
                        {
                            Name = "Queen Lucia",
                            Author = "E F Benson",
                            Description = "Here you can put description about the book"
                        }
                    }
                },
                CardPrice = 43.35m
            };

            return new LibraryDto[] { firstLibrary, secondLibrary };
        }
    }
}
