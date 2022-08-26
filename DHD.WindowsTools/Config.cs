using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DHD.WindowsTools
{
    internal class Config
    {

        private static XDocument? _doc = null;
        private static XElement? _root = null;
        private static String _configFile = System.IO.Path.GetDirectoryName(Environment.ProcessPath) + "\\Config.xml";

        private static void LoadConfig()
        {
            _doc = XDocument.Load(_configFile);
            _root = _doc.Root;
        }

        internal static List<String>? MoveToList
        {
            get
            {
                if (_root == null) return null;

                List<String> result = new List<string>();

#pragma warning disable CS8602 // 解引用可能出现空引用。
                var x = _root.Element("MoveTo").Element("Folders").Elements();
                foreach (var xx in x)
                {
                    result.Add(xx.Value);
                }
#pragma warning restore CS8602 // 解引用可能出现空引用。

                return result;
            }
        }

        internal static void AddToMoveToList(String path)
        {
            if (_root != null) { 
                XElement? moveToElement = _root.Element("MoveTo");
                if (moveToElement == null)
                {
                    moveToElement = new XElement("MoveTo");
                }

                XElement? foldersElement = moveToElement.Element("Folders");
                if (foldersElement == null)
                {
                    foldersElement = new XElement("Folders");
                }

                XElement? target = foldersElement.Elements().FirstOrDefault(x => x.Value == path);
                if (target == null)
                {
                    target = new XElement("Folder");
                    target.Value = path;
                    foldersElement.Add(target);
                }

                Save();
            }            
        }

        internal static void RemoveFromMoveToList(String path)
        {
            if (_root != null)
            {
                XElement? moveToElement = _root.Element("MoveTo");
                if (moveToElement == null)
                {
                    moveToElement = new XElement("MoveTo");
                }

                XElement? foldersElement = moveToElement.Element("Folders");
                if (foldersElement == null)
                {
                    foldersElement = new XElement("Folders");
                }

                XElement? target = foldersElement.Elements().FirstOrDefault(x => x.Value == path);
                if (target != null)
                {
                    target.Remove();
                }

                Save();
            }
        }

        internal static void Load()
        {
            LoadConfig();
        }

        internal static void Save()
        {
            _doc.Save(_configFile);
        }
    }
}
