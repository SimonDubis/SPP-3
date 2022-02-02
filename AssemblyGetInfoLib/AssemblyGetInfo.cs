using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyGetInfoLib
{
    public class AssemblyGetInfo : IGetInfo
    {
        private Assembly assembly; //сборка

        public Node GetInfoFromFile(string fileName) //берем информацию с файла
        {
            assembly = Assembly.LoadFrom(fileName); //читаем сборку
            Node info = new Node(assembly.FullName); //полное имя сборки
            try
            {
                var types = assembly.GetTypes(); //загружаем все типы
                var attr = Attribute.GetCustomAttribute(assembly, typeof(CompilerGeneratedAttribute));
                foreach (var type in types)
                {
                    GetTypeInfo(SearchNamespaceEntry(info.Nodes, type.ToString()), type); //передаем дерево и тип
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Reflection.ReflectionTypeLoadException)
                {
                    var typeLoadException = ex as ReflectionTypeLoadException;
                    var loaderExceptions = typeLoadException.LoaderExceptions;
                }
            }
            return info;
        }

        private ObservableCollection<Node> SearchNamespaceEntry(ObservableCollection<Node> tree, string typeName)
        {
            int dotIndex = typeName.IndexOf('.');
            if (dotIndex == -1)
            {
                return tree;
            }
            Node match = null; 
            string namespaceName = typeName.Substring(0, dotIndex);
            foreach (var node in tree)
            {
                if (node.Content == namespaceName)
                {
                    match = node;
                    break;
                }
            }
            if (match == null)
            {
                match = new Node(namespaceName);
                tree.Add(match);
            }
            return SearchNamespaceEntry(match.Nodes, typeName.Substring(dotIndex + 1));
        }


        private void GetTypeInfo(ObservableCollection<Node> tree, Type type) //передаем класс и берем информацию о полях свойствах и методоах
        {
            if (!type.IsDefined(typeof(ExtensionAttribute), false) && type.IsVisible)
            {
                Node typeInfo = new Node(type.Name);
                tree.Add(typeInfo);

                GetMembersInfo(typeInfo.Nodes, type.GetFields(), "Fields");
                GetMembersInfo(typeInfo.Nodes, type.GetProperties(), "Properties");
                GetMembersInfo(typeInfo.Nodes, type.GetMethods(), "Methods");
            }
        }

        private void GetMembersInfo(ObservableCollection<Node> tree, object[] members, string header)
        {
            if (members.Length > 0)
            {
                Node membersInfo = new Node(header);
                tree.Add(membersInfo);
                foreach (var member in members)
                {
                    Node memberInfo = new Node(member.ToString());
                    membersInfo.Nodes.Add(memberInfo);
                }
            }
        }

    }
}
