﻿using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;

namespace GameCreator.Editor.Common
{
    internal class TypePage
    {
        public string Title { get; }
        public List<TypeNode> Content { get; }

        // CONSTRUCTOR: ---------------------------------------------------------------------------
        
        public TypePage(Tree<Type> tree, bool isSearch)
        {
            this.Title = tree.id;
            this.Content = new List<TypeNode>();
            
            foreach (KeyValuePair<string, Tree<Type>> child in tree.Children)
            {
                if (child.Value.Data == null)
                {
                    this.Content.Add(new TypeNodeFolder(child.Key, child.Value));
                }
                else
                {
                    this.Content.Add(new TypeNodeValue(child.Value.Data));
                }
            }
            
            if (!isSearch) this.Content.Sort(CompareTypeNodes);
        }
        
        // COMPARER: ------------------------------------------------------------------------------

        private static int CompareTypeNodes(TypeNode a, TypeNode b)
        {
            return a switch
            {
                TypeNodeValue _ when b is TypeNodeFolder => -1,
                TypeNodeFolder _ when b is TypeNodeValue => 1,
                _ => string.Compare(
                    a.Name, 
                    b.Name, 
                    StringComparison.Ordinal
                )
            };
        }
    }   
}
