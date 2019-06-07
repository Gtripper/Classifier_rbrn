using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classifier.Nodes;

namespace Classifier
{
    public interface INode
    {
        string Code { get; }
        string Type { get; }
        string Kind { get; }

        string SimpleDescription { get; }
        string Description { get; }

        string GetSearchResult(string input);
        bool Equals(INode node);
    }

    /// <summary>
    /// 
    /// </summary>
    public class Node : INode
    {
        public string Code { get; }
        public string Type { get; }
        public string Kind { get; }
        public string SimpleDescription { get; }
        public string Description { get; }        
        private INodeRegExp regExp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_code"></param>
        /// <param name="_type"></param>
        /// <param name="_kind"></param>
        /// <param name="_simpleDescription"></param>
        /// <param name="_description"></param>
        /// <param name="_reg"></param>
        public Node(string _code, string _type, string _kind, string _simpleDescription, string _description, INodeRegExp _reg)
        {
            Code = _code;
            Type = _type;
            Kind = _kind;
            Description = _description;
            SimpleDescription = _simpleDescription;
            regExp = _reg;
        }

        public string GetSearchResult(string input)
        {
            return regExp.GetSearchResult(input);
        }

        public bool Equals(INode node)
        {
            return Code.Equals(node.Code);
        }
    }

}
