﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace c3o.SqlCopy
{
    public class SerializationHelper
    {

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
		private static string UTF8ByteArrayToString(byte[] characters)
		{
			return new UTF8Encoding().GetString(characters);
		}

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
		private static byte[] StringToUTF8ByteArray(string xml)
		{
			return new UTF8Encoding().GetBytes(xml);
		}

        public static void Serialize<T>(T obj, string filename)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            XmlSerializer x = new XmlSerializer(typeof(T));
            x.Serialize(stream, obj);
            stream.Close();
        }

		/// <summary>
		/// Serialize an object into an XML string
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string Serialize<T>(T obj)
		{
			try
			{
				string xmlString = null;
				MemoryStream memoryStream = new MemoryStream();
				XmlSerializer xs = new XmlSerializer(typeof(T));
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				xs.Serialize(xmlTextWriter, obj);
				memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
				xmlString = UTF8ByteArrayToString(memoryStream.ToArray()); return xmlString;
			}
			catch
			{
				return string.Empty;
			}
		}


		/// <summary>
		/// Method to reconstruct an Object from XML string
		/// </summary>
		/// <param name="pXmlizedString"></param>
		/// <returns></returns>
		public static Object Deserialize(String pXmlizedString, Type type)
		{
			XmlSerializer xs = new XmlSerializer(type);
			MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
			return xs.Deserialize(memoryStream);
		}


        public static Object Deserialize(Type type, string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(type);            
            Object obj = xs.Deserialize(stream);
            stream.Close();
            return obj;
        }


        public static T Deserialize<T>(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            Object obj = xs.Deserialize(stream);
            stream.Close();
            return (T) obj;
        }

        
        /// <summary>
        /// Reconstruct an object from an XML string
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(xml));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return (T)xs.Deserialize(memoryStream);
        }

        public static T Deserialize<T>(System.IO.Stream stream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            //MemoryStream memoryStream = new MemoryStream(stream);
            //XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8);
            return (T)xs.Deserialize(stream);
        }
   }
}